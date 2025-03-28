﻿using LPMS.Domain.Models.RnRModels.ReferenceModels;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;

namespace LPMS.API.Controllers
{
    [Route("api/{culture}/references")]
    [ApiController]
    public class ReferencesController : ControllerBase
    {
        private readonly IReferenceService _referenceService;

        public ReferencesController(IReferenceService referenceService)
        {
            _referenceService = referenceService;
        }

        [HttpGet(nameof(GetByReferenceTypeCode) + "/{referenceTypeCode}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<ReferenceWReferenceTypeResponse>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(InternalServerErrorModel))]
        public async Task<IResult> GetByReferenceTypeCode(string culture, string referenceTypeCode)
        {
            var references = await _referenceService.GetByReferenceTypeCodeAsync(referenceTypeCode, CultureInfo.GetCultureInfo(culture));
            return Results.Ok(references);
        }
    }
}
