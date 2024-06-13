using FluentValidation;
using LanguageExt.Common;
using LPMS.Application.Validators;
using LPMS.Domain.Models.RnRModels.Login;
using System.Globalization;
using System.Reflection.Metadata.Ecma335;

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

        public async Task<string> LoginAsync(LoginRequest request, CultureInfo ci)
        {
            var user = await _identityUserRepository.GetUserByEmailAsync(request.Email);

            if (user == null)
                return string.Empty;

            var login = await _identityUserRepository.IsCorrectPassword(user, request.Password);

            if (!login)
                return string.Empty;

            var genTokenResult = await _tokenService.GenerateAuthTokenAsync(user.Id);

            return genTokenResult.Match(
                s => s.Token,
                e => e.Message
            );
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
