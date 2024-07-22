using FluentEmail.MailKitSmtp;

namespace EmailService
{
    public class EmailConfig
    {
        public const string SectionName = "EmailConfig";

        public string From { get; init; } = string.Empty;
        public string DisplayName { get; init; } = string.Empty;
        public SmtpClientOptions SmtpClientOptions { get; init; } = null!;
    }
}
