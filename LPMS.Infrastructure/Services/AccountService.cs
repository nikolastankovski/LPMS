using FluentValidation;
using LPMS.Application.Validators;
using System.Globalization;

namespace LPMS.Infrastructure.Services
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _accountRepository;
        private readonly ISystemUserRepository _identityUserRepository;
        private readonly ITokenService _tokenService;

        public AccountService(IAccountRepository accountRepository, ISystemUserRepository identityUserRepository, ITokenService tokenService)
        {
            _accountRepository = accountRepository;
            _identityUserRepository = identityUserRepository;
            _tokenService = tokenService;
        }

        public async Task<string> LoginAsync(string email, string password)
        {
            var user = await _identityUserRepository.GetUserByEmailAsync(email);

            if (user == null)
                return string.Empty;

            var login = await _identityUserRepository.IsCorrectPassword(user, password);

            if (!login)
                return string.Empty;

            var getToken = await _tokenService.GenerateTokenAsync(user.Id);

            return getToken;
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
