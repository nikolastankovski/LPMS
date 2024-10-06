using LPMS.Domain.Models.RnRModels.UserManagementModels;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;

namespace LPMS.API.Controllers
{
    [Route("api/{culture}/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AuthTokenResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(BadRequestModel))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(InternalServerErrorModel))]
        public async Task<IResult> Login(string culture, LoginRequest request)
        {
            var getTokenResult = await _authService.GetAuthTokenAsync(request, CultureInfo.GetCultureInfo(culture));

            return getTokenResult.IsSuccess ? getTokenResult.ToOkResponse() : getTokenResult.ToBadRequest();
        }

        [HttpPost("refresh-token")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AuthTokenResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(BadRequestModel))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(InternalServerErrorModel))]
        public async Task<IResult> RefreshToken(string culture, RefreshTokenRequest request)
        {
            var refreshAuthToken = await _authService.RefreshAuthTokenAsync(request, CultureInfo.GetCultureInfo(culture));

            return refreshAuthToken.IsSuccess ? refreshAuthToken.ToOkResponse() : refreshAuthToken.ToBadRequest();
        }
    }
}