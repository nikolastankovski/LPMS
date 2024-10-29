using Microsoft.AspNetCore.Mvc;
using System.Globalization;
using LPMS.Domain.Models.RnRModels.AuthModels;

namespace LPMS.API.Controllers
{
    [Route("api/{culture}/auth")]
    [ApiController]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(BadRequestModel))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(InternalServerErrorModel))]
    public class AuthController(IAuthService authService) : ControllerBase
    {
        [HttpPost("login")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AuthTokenResponse))]
        public async Task<IResult> Login(string culture, LoginRequest request)
        {
            var getTokenResult = await authService.GetAuthTokenAsync(request, CultureInfo.GetCultureInfo(culture));

            return getTokenResult.IsSuccess ? getTokenResult.ToOkResponse() : getTokenResult.ToBadRequest();
        }

        [HttpPost("refresh-token")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AuthTokenResponse))]
        public async Task<IResult> RefreshToken(string culture, RefreshTokenRequest request)
        {
            var refreshAuthToken = await authService.RefreshAuthTokenAsync(request, CultureInfo.GetCultureInfo(culture));

            return refreshAuthToken.IsSuccess ? refreshAuthToken.ToOkResponse() : refreshAuthToken.ToBadRequest();
        }
    }
}