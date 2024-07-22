using System.Globalization;
using System.Reflection.Metadata.Ecma335;

namespace LPMS.Infrastructure.Services
{
    public class TestService
    {
        public List<string> Test(CultureInfo ci)
        {
            /*var model = new SystemUser()
            {
                Email = "stankovski.n@hotmail.com"
            };

            var result = model.Validate(ci);

            return result.GetErrors();*/

            return new List<string>();
        }
    }
}
