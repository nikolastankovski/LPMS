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
            return Ok(_test.Test(CultureInfo.GetCultureInfo(culture)));
        }
    }
}
