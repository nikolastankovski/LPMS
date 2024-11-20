using System.Globalization;
using System.Transactions;
using LPMS.Domain.Models.RnRModels.UserModels;
using LPMS.EmailService.EmailService;

namespace LPMS.Infrastructure.Services
{
    public class UserService(
        IAccountRepository accountRepository,
        ISystemUserRepository systemUserRepository)
        : IUserService
    {
        public async Task<Result<ApplicationUser>> GetAppUserAsyncById(Guid id, CultureInfo culture)
        {
            try
            {
                ApplicationUser? appUser = await accountRepository.GetApplicationUserAsync(id);

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

                    var validationResult = await sysUser.ValidateAsync(culture, systemUserRepository);
                    if (!validationResult.IsValid)
                        return Result.Fail(validationResult.GetErrors());

                    sysUser = await systemUserRepository.CreateAsync(sysUser);

                    await systemUserRepository.AddToRoleAsync(sysUser, request.Role);
                    #endregion

                    #region Account
                    Account account = request.MapToAccount(sysUser.Id);

                    validationResult = account.Validate(culture);

                    if (!validationResult.IsValid)
                        return Result.Fail(validationResult.GetErrors());

                    await accountRepository.CreateAsync(account);
                    #endregion

                    /*var emailSetUp = new EmailSetUp()
                    {
                        To = new Address(sysUser.Email, account.Name),
                        Subject = culture.GetResource(nameof(Resources.Email_Account_NewUserSubject)),
                        EmailTemplate = EmailTemplates.Account_ConfirmEmail,
                        Culture = culture,
                        Tokens = account
                    };

                    await emailService.SendEmailAsync(emailSetUp);*/

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
                    Account? account = await accountRepository.GetByIdAsync(id);

                    if (account == null)
                        return Result.Fail(culture.GetResource(nameof(Resources.User_Doesnt_Exist)));

                    SystemUser? sysUser = await systemUserRepository.GetUserByIdAsync(account.SystemUserId);
                    sysUser.PhoneNumber = request.PhoneNumber;

                    var validationResult = await sysUser.ValidateAsync(culture, systemUserRepository);
                    if (!validationResult.IsValid)
                        return Result.Fail(validationResult.GetErrors());

                    await systemUserRepository.UpdateUserRoleAsync(sysUser, request.Role);

                    account.Name = request.Name;

                    validationResult = account.Validate(culture);
                    if (!validationResult.IsValid)
                        return Result.Fail(validationResult.GetErrors());

                    await accountRepository.ModifyAsync(account);

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
                    var account = await accountRepository.GetByIdAsync(id);

                    await accountRepository.DeleteAsync(id);

                    await systemUserRepository.DeleteAsync(account.SystemUserId);

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
