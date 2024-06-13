using LPMS.Domain.Interfaces.ServiceInterfaces;
using LPMS.Domain.Models.RnRModels.Login;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;

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

        [HttpPost(nameof(Login))]
        public async Task<string> Login(string culture, LoginRequest request)
        {
            return await _accountService.LoginAsync(request, CultureInfo.GetCultureInfo(culture));
        }

    }
}
