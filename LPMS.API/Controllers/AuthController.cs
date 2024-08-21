using LPMS.Domain.Models.RnRModels.UserManagementModels;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;

namespace LPMS.API.Controllers
{
    [Route("api/{culture}/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpGet(nameof(GetAuthenticationToken))]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AuthenticationTokenResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(BadRequestModel))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(InternalServerErrorModel))]
        public async Task<IResult> GetAuthenticationToken(string culture, AuthenticationTokenRequest request)
        {
            var getTokenResult = await _authService.GetAuthenticationToken(request, CultureInfo.GetCultureInfo(culture));

            return getTokenResult.IsSuccess ? getTokenResult.ToOkResponse() : getTokenResult.ToBadRequest();
        }

        [HttpGet(nameof(RefreshToken))]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AuthenticationTokenResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(BadRequestModel))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(InternalServerErrorModel))]
        public async Task<IResult> RefreshToken(string culture, RefreshTokenRequest request)
        {
            /*var getTokenResult = await _authService.GetAuthenticationToken(request, CultureInfo.GetCultureInfo(culture));*/

            return Results.Ok();
        }

    }
}