using LPMS.Domain.Interfaces.RepositoryInterfaces;
using Microsoft.AspNetCore.Mvc;

namespace LPMS.API.Controllers
{
    [Route("api/{culture}/[controller]")]
    [ApiController]
    public class ReferenceController : ControllerBase
    {
        private readonly IReferenceRepository _referenceRepository;

        public ReferenceController(IReferenceRepository referenceRepository)
        {
            _referenceRepository = referenceRepository;
        }

        // GET: api/<ReferenceController>
        [HttpGet(nameof(GetById) + "/{referenceId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Reference))]
        //[ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(string))]
        public Reference? GetById(string culture, int referenceId)
        {
            return _referenceRepository.GetById(referenceId);
        }

        [HttpGet(nameof(GetByReferenceTypeCode) + "/{referenceTypeCode}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<Reference>))]
        //[ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(string))]
        public List<Reference> GetByReferenceTypeCode(string referenceTypeCode)
        {
            return _referenceRepository.GetByReferenceTypeCode(referenceTypeCode);
        }
    }
}
