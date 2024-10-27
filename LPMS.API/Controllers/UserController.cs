using Microsoft.AspNetCore.Mvc;
using System.Globalization;
using LPMS.Domain.Models.RnRModels.UserManagementModels;
using LPMS.Domain.Models.RnRModels;

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
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApplicationUser))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(BadRequestModel))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(InternalServerErrorModel))]
        public async Task<IResult> Get(string culture, Guid id)
        {
            var getUser = await _userService.GetAppUserAsyncById(id, CultureInfo.GetCultureInfo(culture));

            return getUser.IsSuccess ? getUser.ToOkResponse() : getUser.ToBadRequest();
        }

        [CustomAuthorize(Roles = [UserRoles.SystemAdministrator, UserRoles.Administrator])]
        [HttpPost("create")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CreatedResponse<Guid>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(BadRequestModel))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(InternalServerErrorModel))]
        public async Task<IResult> Create(string culture, CreateModifyUserRequest request)
        {
            var createUser = await _userService.CreateAppUserAsync(request, CultureInfo.GetCultureInfo(culture));

            return createUser.IsSuccess ? createUser.ToOkResponse() : createUser.ToBadRequest();
        }

        [CustomAuthorize(Roles = [UserRoles.SystemAdministrator, UserRoles.Administrator])]
        [HttpPut("modify/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(BadRequestModel))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(InternalServerErrorModel))]
        public async Task<IResult> Modify(string culture, Guid id, CreateModifyUserRequest request)
        {
            var modifyUser = await _userService.ModifyAppUserAsync(id, request, CultureInfo.GetCultureInfo(culture));

            return modifyUser.IsSuccess ? modifyUser.ToOkResponse() : modifyUser.ToBadRequest();
        }

        [CustomAuthorize(Roles = [UserRoles.SystemAdministrator, UserRoles.Administrator])]
        [HttpDelete("delete/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(BadRequestModel))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(InternalServerErrorModel))]
        public async Task<IResult> Delete(string culture, Guid id)
        {
            var deleteUser = await _userService.DeleteAppUserAsync(id, CultureInfo.GetCultureInfo(culture));

            return deleteUser.IsSuccess ? deleteUser.ToOkResponse() : deleteUser.ToBadRequest();
        }
    }
}