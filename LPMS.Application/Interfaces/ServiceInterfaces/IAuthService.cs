using FluentResults;
using LPMS.Domain.Models.RnRModels.UserManagementModels;
using System.Globalization;

namespace LPMS.Application.Interfaces.ServiceInterfaces
{
    public interface IAuthService
    {
        Task<Result<AuthTokenResponse>> GetAuthTokenAsync(LoginRequest request, CultureInfo culture);
        Task<Result<AuthTokenResponse>> RefreshAuthTokenAsync(RefreshTokenRequest request, CultureInfo culture);
    }
}
