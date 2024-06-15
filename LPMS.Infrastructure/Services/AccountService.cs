using FluentValidation;
using LPMS.Application.Validators;
using LPMS.Application.Models.RnRModels.Login;
using System.Globalization;
using FluentResults;

namespace LPMS.Infrastructure.Services
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _accountRepository;
        private readonly ISystemUserRepository _identityUserRepository;
        private readonly IAuthService _tokenService;

        public AccountService(IAccountRepository accountRepository, ISystemUserRepository identityUserRepository, IAuthService tokenService)
        {
            _accountRepository = accountRepository;
            _identityUserRepository = identityUserRepository;
            _tokenService = tokenService;
        }

        public async Task<Result<LoginResponse>> LoginAsync(LoginRequest request, CultureInfo culture)
        {
            try
            {
                var sysUser = await _identityUserRepository.GetUserByEmailAsync(request.Email);

                if (sysUser == null)
                    return Result.Fail(culture.GetResource(nameof(Resources.User_not_found)));

                var login = await _identityUserRepository.IsCorrectPassword(sysUser, request.Password);

                if (!login)
                    return Result.Fail(culture.GetResource(nameof(Resources.Incorrect_credentials)));

                var genTokenResult = await _tokenService.GenerateAuthTokenAsync(sysUser.Id, culture);

                if(genTokenResult.IsSuccess)
                    return Result.Ok(new LoginResponse() { Token = genTokenResult.Value.Token, Expires = genTokenResult.Value.Expires });   
    
                return Result.Fail(genTokenResult.Errors);
            }
            catch (Exception e)
            {
                if (e is ValidationException)
                    return Result.Fail(e.Message);

                Log.Error(exception: e, $"Exception occured: {e.Message}");

                return Result.Fail(culture.GetResource(nameof(Resources.Unexpected_Error)));
            }
        }

        public Task<string> LoginAsync(string email, string password)
        {
            throw new NotImplementedException();
        }

        public void Test()
        {
            var reference = new Reference()
            {
                Name_EN = "Test",
                Description_EN = "Test",
            };

            ReferenceValidator val = new ReferenceValidator(CultureInfo.GetCultureInfo("en"));

            var valResult = val.Validate(reference);
            var test1 = new ValidationException(valResult.Errors);

            try
            {
                val.ValidateAndThrow(reference);

            }
            catch (Exception e)
            {
                var test = e;
            }
        }
    }
}
