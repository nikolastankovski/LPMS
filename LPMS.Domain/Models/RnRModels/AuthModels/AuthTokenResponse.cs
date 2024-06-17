namespace LPMS.Domain.Models.RnRModels.AuthModels
{
    public class AuthTokenResponse
    {
        public string Token { get; set; } = string.Empty;
        public DateTime? Expires { get; set; }
    }
}
