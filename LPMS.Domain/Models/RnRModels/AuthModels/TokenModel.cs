namespace LPMS.Domain.Models.RnRModels.AuthModels
{
    public class TokenModel
    {
        public required string Token { get; set; }
        public DateTime ExpiresUTC { get; set; }
    }
}
