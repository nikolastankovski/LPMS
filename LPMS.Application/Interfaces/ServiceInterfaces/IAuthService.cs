using FluentResults;
using LPMS.Domain.Models.RnRModels.AuthModels;
using System.Globalization;

namespace LPMS.Application.Interfaces.ServiceInterfaces
{
    public interface IAuthService
    {
        Task<Result<AuthTokenResponse>> GenerateAuthTokenAsync(Guid userId, CultureInfo culture);
        /*Task<Result<AuthTokenResponse>> GenerateTokenAsync(string email);*/
    }
}
