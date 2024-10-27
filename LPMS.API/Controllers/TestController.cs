using FluentEmail.Core.Models;
using LPMS.Application.Interfaces.RepositoryInterfaces;
using LPMS.EmailService.EmailService;
using LPMS.EmailService.EmailTemplates;
using LPMS.Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;

namespace LPMS.API.Controllers
{
    [Route("api/{culture}/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private readonly TestService _test;
        private readonly IEmailService _emailService;
        private readonly IReferenceRepository _referenceRepo;

        public TestController(TestService test, IEmailService emailService, IReferenceRepository referenceRepo)
        {
            _test = test;
            _emailService = emailService;
            _referenceRepo = referenceRepo;
        }

        [HttpGet(nameof(Test))]
        public async Task<IActionResult> Test(string culture)
        {
            var test = await _referenceRepo.GetAllAsync();

            /*var test = EmailTemplates.Account_ForgotPassword.IndexOf("\\EmailTemplates\\");

            var emailSetUp = new EmailSetUp()
            {
                To = new Address("stankovski.n@hotmail.com", "Nikola Stankovski"),
                Subject = "Test",
                EmailTemplate = EmailTemplates.Account_ForgotPassword,
                Culture = CultureInfo.GetCultureInfo(culture),
                Tokens = new { Name = "Nikola" }
            };

            var test123 = await _emailService.SendEmailAsync(emailSetUp);*/

            return Ok(true);

            /* string[] test1 = ["DPT1", "Test2"];
             List<string> test2 = new List<string>() { "DPT1", "DPT2" };

             var test = test2.Intersect(test1).Any();


             return Ok(_test.Test(CultureInfo.GetCultureInfo(culture)));
            */
        }
    }
}
