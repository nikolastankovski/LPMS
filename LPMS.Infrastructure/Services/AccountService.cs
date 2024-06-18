using FluentValidation;
using LPMS.Application.Validators;
using System.Globalization;
using FluentResults;
using System.Transactions;

namespace LPMS.Infrastructure.Services
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _accountRepository;
        private readonly ISystemUserRepository _identityUserRepository;
        private readonly IAccountxDepartmentxDivisionRepository _accxDeptxDivRepository;
        private readonly IAuthService _tokenService;

        public AccountService(IAccountRepository accountRepository, ISystemUserRepository identityUserRepository, IAccountxDepartmentxDivisionRepository accxDeptxDivRepository, IAuthService tokenService)
        {
            _accountRepository = accountRepository;
            _identityUserRepository = identityUserRepository;
            _accxDeptxDivRepository = accxDeptxDivRepository;
            _tokenService = tokenService;
        }

        public async Task<Result<LoginResponse>> LoginAsync(LoginRequest request, CultureInfo culture)
        {
            try
            {
                var sysUser = await _identityUserRepository.GetUserByEmailAsync(request.Email);

                if (sysUser == null)
                    return Result.Fail(culture.GetResource(nameof(Resources.User_not_found)));

                var login = await _identityUserRepository.IsCorrectPasswordAsync(sysUser, request.Password);

                if (!login)
                    return Result.Fail(culture.GetResource(nameof(Resources.Incorrect_credentials)));

                var genTokenResult = await _tokenService.GenerateAuthTokenAsync(sysUser.Id, culture);

                if (genTokenResult.IsFailed)
                    return Result.Fail(genTokenResult.Errors);

                return Result.Ok(new LoginResponse() { Token = genTokenResult.Value.Token, Expires = genTokenResult.Value.Expires });
            }
            catch (Exception e)
            {
                Log.Error(exception: e, $"Exception occured: {e.Message}");

                return Result.Fail(culture.GetResource(nameof(Resources.Unexpected_Error)));
            }
        }

        public async Task<Result<CreateUserResponse>> CreateApplicationUserAsync(CreateUserRequest request, CultureInfo culture)
        {
            using (var transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                try
                {
                    var sysUser = request.MapToSystemUser();

                    sysUser = await _identityUserRepository.CreateAsync(sysUser);

                    await _identityUserRepository.AddToRoleAsync(sysUser, request.Role);

                    var account = request.MapToAccount(sysUser.Id);

                    var accValidation = account.Validate(culture);

                    if (!accValidation.IsValid)
                        return Result.Fail(accValidation.GetErrors());

                    await _accountRepository.CreateAsync(account);

                    var accxDeptxDiv = request.MapToAccountxDepartmentxDivision(account.AccountID);

                    await _accxDeptxDivRepository.CreateAsync(accxDeptxDiv);

                    transaction.Complete();

                    return Result.Ok(new CreateUserResponse());
                }
                catch (Exception e)
                {
                    transaction.Dispose();

                    Log.Error(exception: e, $"Exception occured: {e.Message}");

                    return Result.Fail(culture.GetResource(nameof(Resources.Unexpected_Error)));
                }
            }
        }
    }
}
