namespace LPMS.Domain.Models.RnRModels.UserManagementModels
{
    public class RefreshTokenRequest
    {
        public required string AuthToken { get; set; }
        public required string RefreshToken { get; set; }
    }
}