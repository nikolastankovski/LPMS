using System.Globalization;
using LPMS.Domain.Models.RnRModels.City;
using Microsoft.AspNetCore.Mvc;

namespace LPMS.API.Controllers;

[Route("api/{culture}/cities")]
[ApiController]
[CustomAuthorize(Roles = [UserRoles.SystemAdministrator, UserRoles.Administrator])]
[ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(BadRequestModel))]
[ProducesResponseType(StatusCodes.Status401Unauthorized)]
[ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(InternalServerErrorModel))]
public class CitiesController(ICityService cityService) : ControllerBase
{
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CityResponse))]
    public async Task<IResult> Get(string culture, int id)
    {
        return Results.Ok(await cityService.GetByIdAsync(id, CultureInfo.GetCultureInfo(culture)));
    }
        
    [HttpPost("create")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CreatedResponse<int>))]
    public async Task<IResult> Create(string culture, CityRequest request)
    {
        var createResult = await cityService.CreateAsync(request, CultureInfo.GetCultureInfo(culture));
        return createResult.IsSuccess ? createResult.ToCreatedResponse(HttpContext.Request.Path) : createResult.ToBadRequest();
    }
        
    [HttpPut("modify/{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IResult> Modify(string culture, int id, CityRequest request)
    {
        var modifyResult = await cityService.ModifyAsync(id, request, CultureInfo.GetCultureInfo(culture));
        return modifyResult.IsSuccess ? modifyResult.ToOkResponse() : modifyResult.ToBadRequest();
    }
        
    [HttpDelete("delete/{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IResult> Delete(string culture, int id)
    {
        var deleteResult = await cityService.DeleteAsync(id, CultureInfo.GetCultureInfo(culture));
        return deleteResult.IsSuccess ? deleteResult.ToOkResponse() : deleteResult.ToBadRequest();
    }
}