using Microsoft.AspNetCore.Mvc;
using System.Globalization;
using LPMS.Domain.Models.RnRModels.UserModels;

namespace LPMS.API.Controllers
{
    [Route("api/{culture}/users")]
    [ApiController]
    [CustomAuthorize(Roles = [UserRoles.SystemAdministrator, UserRoles.Administrator])]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(BadRequestModel))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(InternalServerErrorModel))]
    public class UsersController(IUserService userService) : ControllerBase
    {
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApplicationUser))]
        public async Task<IResult> Get(string culture, Guid id)
        {
            var getUser = await userService.GetAppUserAsyncById(id, CultureInfo.GetCultureInfo(culture));

            return getUser.IsSuccess ? getUser.ToOkResponse() : getUser.ToBadRequest();
        }

        [HttpPost("create")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CreatedResponse<Guid>))]
        public async Task<IResult> Create(string culture, CreateModifyUserRequest request)
        {
            var createUser = await userService.CreateAppUserAsync(request, CultureInfo.GetCultureInfo(culture));

            return createUser.IsSuccess ? createUser.ToOkResponse() : createUser.ToBadRequest();
        }

        [HttpPut("modify/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IResult> Modify(string culture, Guid id, CreateModifyUserRequest request)
        {
            var modifyUser = await userService.ModifyAppUserAsync(id, request, CultureInfo.GetCultureInfo(culture));

            return modifyUser.IsSuccess ? modifyUser.ToOkResponse() : modifyUser.ToBadRequest();
        }

        [HttpDelete("delete/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IResult> Delete(string culture, Guid id)
        {
            var deleteUser = await userService.DeleteAppUserAsync(id, CultureInfo.GetCultureInfo(culture));

            return deleteUser.IsSuccess ? deleteUser.ToOkResponse() : deleteUser.ToBadRequest();
        }
    }
}