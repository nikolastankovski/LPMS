using LanguageExt.Common;
using LPMS.Domain.Models.RnRModels.Auth;

namespace LPMS.Domain.Interfaces.ServiceInterfaces
{
    public interface IAuthService
    {
        Task<Result<AuthTokenResponse>> GenerateAuthTokenAsync(Guid userId);
        /*Task<Result<AuthTokenResponse>> GenerateTokenAsync(string email);*/
    }
}
