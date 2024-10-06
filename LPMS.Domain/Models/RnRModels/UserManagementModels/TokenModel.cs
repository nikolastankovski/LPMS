namespace LPMS.Domain.Models.RnRModels.UserManagementModels
{
    public class TokenModel
    {
        public required string Token { get; set; }
        public DateTime ExpiresUTC { get; set; }
    }
}
