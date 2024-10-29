using System.Globalization;
using LPMS.Application.Interfaces.RepositoryInterfaces;
using LPMS.Domain.Models.RnRModels;
using LPMS.Domain.Models.RnRModels.Country;
using Microsoft.AspNetCore.Mvc;

namespace LPMS.API.Controllers
{
    [Route("api/{culture}/countries")]
    [ApiController]
    [CustomAuthorize(Roles = [UserRoles.SystemAdministrator, UserRoles.Administrator])]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(BadRequestModel))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(InternalServerErrorModel))]
    public class CountriesController(ICountryService countryService) : ControllerBase
    {
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CountryResponse))]
        public async Task<IResult> Get(string culture, int id)
        {
            var getCountryResult = await countryService.GetByIdAsync(id, CultureInfo.GetCultureInfo(culture));
            return getCountryResult.IsSuccess ? getCountryResult.ToOkResponse() : getCountryResult.ToBadRequest();
        }
        
        [HttpPost("create")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CreatedResponse<int>))]
        public async Task<IResult> Create(string culture, CountryRequest request)
        {
            var createResult = await countryService.CreateAsync(request, CultureInfo.GetCultureInfo(culture));
            return createResult.IsSuccess ? createResult.ToOkResponse() : createResult.ToBadRequest();
        }
        
        [HttpPut("modify/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CreatedResponse<int>))]
        public async Task<IResult> Modify(string culture, int id, CountryRequest request)
        {
            var modifyResult = await countryService.ModifyAsync(id, request, CultureInfo.GetCultureInfo(culture));
            return modifyResult.IsSuccess ? modifyResult.ToOkResponse() : modifyResult.ToBadRequest();
        }
        
        [HttpDelete("delete/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CreatedResponse<int>))]
        public async Task<IResult> Delete(string culture, int id)
        {
            var deleteResult = await countryService.DeleteAsync(id, CultureInfo.GetCultureInfo(culture));
            return deleteResult.IsSuccess ? deleteResult.ToOkResponse() : deleteResult.ToBadRequest();
        }
    }
}