using LPMS.Domain.Interfaces.ServiceInterfaces;
using LPMS.Domain.Models.ConfigModels;
using LPMS.Infrastructure.Data;
using LPMS.Infrastructure.DbContexts;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace LPMS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpPost(nameof(GetToken))]

        public async Task<string> GetToken(string email, string password)
        {
            return await _accountService.LoginAsync(email, password);
        }

    }
}
