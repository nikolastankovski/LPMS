using FluentValidation;
using System.Globalization;
using FluentResults;
using System.Transactions;
using LPMS.Domain.Models.RnRModels.UserManagementModels;

namespace LPMS.Infrastructure.Services
{
    public class UserService : IUserService
    {
        private readonly IAccountRepository _accountRepository;
        private readonly ISystemUserRepository _systemUserRepository;
        private readonly IAuthService _tokenService;

        public UserService(IAccountRepository accountRepository, ISystemUserRepository systemUserRepository, IAuthService tokenService)
        {
            _accountRepository = accountRepository;
            _systemUserRepository = systemUserRepository;
            _tokenService = tokenService;
        }

        public async Task<Result<CreateUserResponse>> CreateApplicationUserAsync(CreateUserRequest request, CultureInfo culture)
        {
            using (var transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                try
                {
                    #region System User
                    SystemUser sysUser = request.MapToSystemUser();

                    var validationResult = sysUser.Validate(culture, _systemUserRepository);
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

                    transaction.Complete();

                    return Result.Ok(new CreateUserResponse());
                }
                catch (Exception e)
                {
                    transaction.Dispose();

                    Log.Error(exception: e, messageTemplate: e.ToMessageTemplate());

                    return Result.Fail(culture.GetResource(nameof(Resources.Unexpected_Error)));
                }
            }
        }

        public async Task<Result<ModifyUserResponse>> ModifyApplicationUser(ModifyUserRequest request, CultureInfo culture)
        {
            using (var transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                try
                {
                    Account? account = await _accountRepository.GetByIdAsync(request.AccountID);

                    if (account == null)
                        return Result.Fail(culture.GetResource(nameof(Resources.User_Doesnt_Exist)));

                    SystemUser? sysUser = await _systemUserRepository.GetUserByIdAsync(account.SystemUserId);
                    sysUser.PhoneNumber = request.PhoneNumber;

                    var validationResult = sysUser.Validate(culture, _systemUserRepository);
                    if (!validationResult.IsValid)
                        return Result.Fail(validationResult.GetErrors());

                    await _systemUserRepository.UpdateUserRoleAsync(sysUser, request.Role);

                    account.Name = request.Name;

                    validationResult = account.Validate(culture);
                    if (!validationResult.IsValid)
                        return Result.Fail(validationResult.GetErrors());

                    await _accountRepository.UpdateAsync(account);

                }
                catch (Exception e)
                {
                    transaction.Dispose();

                    Log.Error(exception: e, messageTemplate: e.ToMessageTemplate());

                    return Result.Fail(culture.GetResource(nameof(Resources.Unexpected_Error)));
                }
            }

            return Result.Fail(culture.GetResource(nameof(Resources.Unexpected_Error)));
        }
    }
}
