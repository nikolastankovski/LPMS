namespace LPMS.Application.Models.ConfigModels
{
    public class JWTConfig
    {
        public const string SectionName = "JWTConfig";
        public bool ValidateIssuerSigningKey { get; init; }
        public string IssuerSigningKey { get; init; } = string.Empty;
        public bool ValidateIssuer { get; init; }
        public string ValidIssuer { get; init; } = string.Empty;
        public bool ValidateAudience { get; init; }
        public string ValidAudience { get; init; } = string.Empty;
        public bool RequireExpirationTime { get; init; }
        public bool ValidateLifetime { get; init; }
        public int ExpirationTimeInMin { get; init; }
    }
}
