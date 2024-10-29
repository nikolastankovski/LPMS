namespace LPMS.Domain.Models.RnRModels.AuthModels
{
    public class AuthTokenResponse
    {
        public required string AccessToken { get; set; }
        public required DateTime Expires { get; set; }
        public required string RefreshToken { get; set; }
    }
}