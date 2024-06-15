using LPMS.Application.Models.RnRModels.Login;
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
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(LoginResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(BadRequestModel))]
        public async Task<IResult> Login(string culture, LoginRequest request)
        {
            var login = await _accountService.LoginAsync(request, CultureInfo.GetCultureInfo(culture));

            return login.IsSuccess ? login.ToOkResponse() : login.ToBadRequest();
        }

    }
}