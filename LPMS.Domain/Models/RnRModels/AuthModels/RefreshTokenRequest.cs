namespace LPMS.Domain.Models.RnRModels.AuthModels
{
    public class RefreshTokenRequest
    {
        public required string AuthToken { get; set; }
        public required string RefreshToken { get; set; }
    }
}