using LPMS.Domain.Models.RnRModels.Login;
using System.Globalization;

namespace LPMS.Domain.Interfaces.ServiceInterfaces
{
    public interface IAccountService
    {
        Task<string> LoginAsync(LoginRequest request, CultureInfo ci);
        void Test();
    }
}
