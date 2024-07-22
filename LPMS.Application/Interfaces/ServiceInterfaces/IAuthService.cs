using FluentResults;
using LPMS.Domain.Models.RnRModels.UserManagementModels;
using System.Globalization;

namespace LPMS.Application.Interfaces.ServiceInterfaces
{
    public interface IAuthService
    {
        Task<Result<AuthenticationTokenResponse>> GetAuthenticationToken(AuthenticationTokenRequest request, CultureInfo culture);
        /*Task<Result<AuthTokenResponse>> GenerateTokenAsync(string email);*/
    }
}
