using System.Globalization;

namespace LPMS.Infrastructure.Services
{
    public class TestService
    {
        public List<string> Test(CultureInfo ci)
        {
            var model = new SystemUser()
            {
                Email = "stankovski.n@hotmail.com"
            };

            var result = model.Validate(ci);

            return result.GetErrors();
        }
    }
}
