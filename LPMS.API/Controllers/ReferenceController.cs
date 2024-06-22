using LPMS.Domain.Models.DTO;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;

namespace LPMS.API.Controllers
{
    [Route("api/{culture}/[controller]")]
    [ApiController]
    public class ReferenceController : ControllerBase
    {
        private readonly IReferenceService _referenceService;

        public ReferenceController(IReferenceService referenceService)
        {
            _referenceService = referenceService;
        }

        [HttpGet(nameof(GetByReferenceTypeCode) + "/{referenceTypeCode}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<DTOReference>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(InternalServerErrorModel))]
        public async Task<IResult> GetByReferenceTypeCode(string culture, string referenceTypeCode)
        {
            var references = await _referenceService.GetByReferenceTypeCodeAsync(referenceTypeCode, CultureInfo.GetCultureInfo(culture));
            return Results.Ok(references);
        }
    }
}
