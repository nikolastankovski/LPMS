using LPMS.Infrastructure.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;

namespace LPMS.API.Controllers
{
    [Route("api/{culture}/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private readonly TestService _test;

        public TestController(TestService test)
        {
            _test = test;
        }

        [HttpGet(nameof(Test))]
        public IActionResult Test(string culture)
        {
            string[] test1 = ["DPT1", "Test2"];
            List<string> test2 = new List<string>() { "DPT1", "DPT2" };

            var test = test2.Intersect(test1).Any();


            return Ok(_test.Test(CultureInfo.GetCultureInfo(culture)));
        }
    }
}
