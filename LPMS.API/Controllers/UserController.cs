using Microsoft.AspNetCore.Mvc;
using System.Globalization;
using LPMS.Domain.Models.RnRModels.UserManagementModels;

namespace LPMS.API.Controllers
{
    [Route("api/{culture}/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [CustomAuthorize(Roles = [UserRoles.SystemAdministrator, UserRoles.Administrator])]
        [HttpPost()]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CreateUserResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(BadRequestModel))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(InternalServerErrorModel))]
        public async Task<IResult> Create(string culture, CreateUserRequest request)
        {
            var createUser = await _userService.CreateApplicationUserAsync(request, CultureInfo.GetCultureInfo(culture));

            return createUser.IsSuccess ? createUser.ToOkResponse() : createUser.ToBadRequest();
        }

        [CustomAuthorize(Roles = [UserRoles.SystemAdministrator, UserRoles.Administrator])]
        [HttpPut()]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CreateUserResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(BadRequestModel))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(InternalServerErrorModel))]
        public async Task<IResult> Modify(string culture, CreateUserRequest request)
        {
            var createUser = await _userService.CreateApplicationUserAsync(request, CultureInfo.GetCultureInfo(culture));

            return createUser.IsSuccess ? createUser.ToOkResponse() : createUser.ToBadRequest();
        }

        [CustomAuthorize(Roles = [UserRoles.SystemAdministrator, UserRoles.Administrator])]
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CreateUserResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(BadRequestModel))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(InternalServerErrorModel))]
        public async Task<IResult> Delete(string culture, Guid id)
        {
            //var createUser = await _accountService.CreateApplicationUserAsync(request, CultureInfo.GetCultureInfo(culture));

            return Results.Ok();
        }
    }
}