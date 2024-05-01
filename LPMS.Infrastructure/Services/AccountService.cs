using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LPMS.Infrastructure.Services
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IIdentityUserRepository _identityUserRepository;
        private readonly ITokenService _tokenService;

        public AccountService(IAccountRepository accountRepository, IIdentityUserRepository identityUserRepository, ITokenService tokenService)
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
    }
}
