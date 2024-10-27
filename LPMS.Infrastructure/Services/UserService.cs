using FluentValidation;
using System.Globalization;
using FluentResults;
using System.Transactions;
using LPMS.Domain.Models.RnRModels;
using LPMS.EmailService.EmailService;
using FluentEmail.Core.Models;
using LPMS.EmailService.EmailTemplates;

namespace LPMS.Infrastructure.Services
{
    public class UserService : IUserService
    {
        private readonly IAccountRepository _accountRepository;
        private readonly ISystemUserRepository _systemUserRepository;
        private readonly IEmailService _emailService;

        public UserService(IAccountRepository accountRepository, ISystemUserRepository systemUserRepository, IEmailService emailService)
        {
            _accountRepository = accountRepository;
            _systemUserRepository = systemUserRepository;
            _emailService = emailService;
        }

        public async Task<Result<ApplicationUser>> GetAppUserAsyncById(Guid id, CultureInfo culture)
        {
            try
            {
                ApplicationUser? appUser = await _accountRepository.GetApplicationUserAsync(id);

                if (appUser == null)
                    return Result.Fail(culture.GetResource(nameof(Resources.User_Not_Found)));

                return Result.Ok(appUser);
            }
            catch (Exception e)
            {
                Log.Error(exception: e, messageTemplate: e.ToMessageTemplate());
            }

            return Result.Fail(culture.GetResource(nameof(Resources.Unexpected_Error)));
        }

        public async Task<Result<CreatedResponse<Guid>>> CreateAppUserAsync(CreateModifyUserRequest request, CultureInfo culture)
        {
            using (var transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                try
                {
                    #region System User
                    SystemUser sysUser = request.MapToSystemUser();

                    var validationResult = await sysUser.ValidateAsync(culture, _systemUserRepository);
                    if (!validationResult.IsValid)
                        return Result.Fail(validationResult.GetErrors());

                    sysUser = await _systemUserRepository.CreateAsync(sysUser);

                    await _systemUserRepository.AddToRoleAsync(sysUser, request.Role);
                    #endregion

                    #region Account
                    Account account = request.MapToAccount(sysUser.Id);

                    validationResult = account.Validate(culture);

                    if (!validationResult.IsValid)
                        return Result.Fail(validationResult.GetErrors());

                    await _accountRepository.CreateAsync(account);
                    #endregion

                    //var emailSetUp = new EmailSetUp()
                    //{
                    //    To = new Address(sysUser.Email, account.Name),
                    //    Subject = culture.GetResource(nameof(Resources.Email_Account_NewUserSubject)),
                    //    EmailTemplate = EmailTemplates.Account_ConfirmEmail,
                    //    Culture = culture,
                    //    Tokens = account
                    //};

                    //await _emailService.SendEmailAsync(emailSetUp);

                    transaction.Complete();

                    return Result.Ok(new CreatedResponse<Guid>(true, account.AccountID));
                }
                catch (Exception e)
                {
                    transaction.Dispose();

                    Log.Error(exception: e, messageTemplate: e.ToMessageTemplate());
                }
            }

            return Result.Fail(culture.GetResource(nameof(Resources.Unexpected_Error)));
        }

        public async Task<Result> ModifyAppUserAsync(Guid id, CreateModifyUserRequest request, CultureInfo culture)
        {
            using (var transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                try
                {
                    Account? account = await _accountRepository.GetByIdAsync(id);

                    if (account == null)
                        return Result.Fail(culture.GetResource(nameof(Resources.User_Doesnt_Exist)));

                    SystemUser? sysUser = await _systemUserRepository.GetUserByIdAsync(account.SystemUserId);
                    sysUser.PhoneNumber = request.PhoneNumber;

                    var validationResult = await sysUser.ValidateAsync(culture, _systemUserRepository);
                    if (!validationResult.IsValid)
                        return Result.Fail(validationResult.GetErrors());

                    await _systemUserRepository.UpdateUserRoleAsync(sysUser, request.Role);

                    account.Name = request.Name;

                    validationResult = account.Validate(culture);
                    if (!validationResult.IsValid)
                        return Result.Fail(validationResult.GetErrors());

                    await _accountRepository.UpdateAsync(account);

                    return Result.Ok();
                }
                catch (Exception e)
                {
                    transaction.Dispose();

                    Log.Error(exception: e, messageTemplate: e.ToMessageTemplate());
                }
            }

            return Result.Fail(culture.GetResource(nameof(Resources.Unexpected_Error)));
        }

        public async Task<Result> DeleteAppUserAsync(Guid id, CultureInfo culture)
        {
            using (var transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                try
                {
                    var account = await _accountRepository.GetByIdAsync(id);

                    await _accountRepository.DeleteAsync(id);

                    await _systemUserRepository.DeleteAsync(account.SystemUserId);

                    transaction.Complete();

                    return Result.Ok();
                }
                catch (Exception e)
                {
                    transaction.Dispose();

                    Log.Error(exception: e, messageTemplate: e.ToMessageTemplate());
                }
            }

            return Result.Fail(culture.GetResource(nameof(Resources.Unexpected_Error)));
        }
    }
}
