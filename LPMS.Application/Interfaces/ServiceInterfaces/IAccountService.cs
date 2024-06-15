using FluentResults;
using LPMS.Application.Models.RnRModels.Login;
using System.Globalization;

namespace LPMS.Application.Interfaces.ServiceInterfaces
{
    public interface IAccountService
    {
        Task<Result<LoginResponse>> LoginAsync(LoginRequest request, CultureInfo culture);
        void Test();
    }
}
