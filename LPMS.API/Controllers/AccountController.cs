using LPMS.Domain.Interfaces.ServiceInterfaces;
using Microsoft.AspNetCore.Mvc;

namespace LPMS.API.Controllers
{
    [Route("api/{culture}/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpPost(nameof(GetToken))]
        public async Task<string> GetToken(string culture, string email, string password)
        {
            _accountService.Test();
            return await _accountService.LoginAsync(email, password);
        }

    }
}
