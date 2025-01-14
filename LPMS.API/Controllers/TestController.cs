using FluentEmail.Core.Models;
using LPMS.Application.Interfaces.RepositoryInterfaces;
using LPMS.EmailService.EmailService;
using LPMS.EmailService.EmailTemplates;
using LPMS.Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;
using ClassLibrary1;

namespace LPMS.API.Controllers
{
    [Route("api/{culture}/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private readonly TestService _test;
        //private readonly IEmailService _emailService;
        private readonly IReferenceRepository _referenceRepo;

        public TestController(TestService test, IReferenceRepository referenceRepo)
        {
            _test = test;
            _referenceRepo = referenceRepo;
        }

        [HttpGet(nameof(Test))]
        public async Task<IActionResult> Test(string culture)
        {
            DocumentGenerator.Generate();

            return Ok();
        }
    }
}
