using FluentEmail.Core.Models;
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

        public TestController(TestService test, IEmailService emailService)
        {
            _test = test;
            _emailService = emailService;
        }

        [HttpGet(nameof(Test))]
        public async Task<IActionResult> Test(string culture)
        {

            var test = EmailTemplates.Account_ForgotPassword.IndexOf("\\EmailTemplates\\");

            var emailSetUp = new EmailSetUp()
            {
                To = new Address("stankovski.n@hotmail.com", "Nikola Stankovski"),
                Subject = "Test",
                EmailTemplate = EmailTemplates.Account_ForgotPassword,
                Culture = CultureInfo.GetCultureInfo(culture),
                Tokens = new { Name = "Nikola" }
            };

            var test123 = await _emailService.SendEmailAsync(emailSetUp);

            return Ok(test123);

            /* string[] test1 = ["DPT1", "Test2"];
             List<string> test2 = new List<string>() { "DPT1", "DPT2" };

             var test = test2.Intersect(test1).Any();


             return Ok(_test.Test(CultureInfo.GetCultureInfo(culture)));
            */
        }
    }
}
