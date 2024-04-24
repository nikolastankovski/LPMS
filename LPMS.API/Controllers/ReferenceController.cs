using LPMS.Domain.Interfaces.RepositoryInterfaces;
using LPMS.Infrastructure.Services.SharedServices;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace LPMS.API.Controllers
{
    [Route("api/[controller]")]
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
        public Reference? GetById(int referenceId)
        {
            var test = Request.HttpContext.Connection;
            Logger.Log("test");
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
