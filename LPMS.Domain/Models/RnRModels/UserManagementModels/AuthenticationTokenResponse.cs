namespace LPMS.Domain.Models.RnRModels.UserManagementModels
{
    public class AuthenticationTokenResponse
    {
        public required string AccessToken { get; set; }
        public required DateTime Expires { get; set; }
        public required string RefreshToken { get; set; }
    }
}