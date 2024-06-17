namespace LPMS.Domain.Models.RnRModels.LoginModels
{
    public record LoginResponse
    {
        public string Token { get; set; } = string.Empty;
        public DateTime? Expires { get; set; }
        public string RefreshToken { get; set; } = string.Empty;

    }
}
