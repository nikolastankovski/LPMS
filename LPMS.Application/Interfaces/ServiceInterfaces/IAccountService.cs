using FluentResults;
using LPMS.Domain.Models.RnRModels.AccountModels;
using LPMS.Domain.Models.RnRModels.LoginModels;
using System.Globalization;

namespace LPMS.Application.Interfaces.ServiceInterfaces
{
    public interface IAccountService
    {
        Task<Result<LoginResponse>> LoginAsync(LoginRequest request, CultureInfo culture);
        Task<Result<CreateUserResponse>> CreateApplicationUserAsync(CreateUserRequest request, CultureInfo culture);
    }
}
