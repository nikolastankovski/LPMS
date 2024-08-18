using FluentEmail.Core;
using FluentEmail.MailKitSmtp;
using FluentEmail.Razor;

namespace LPMS.EmailService.EmailService;

public static class DependencyInjection
{
    public static WebApplicationBuilder AddEmailService(this WebApplicationBuilder builder)
    {
        builder.Services
            .AddOptions<EmailConfig>()
            .Bind(builder.Configuration.GetSection(EmailConfig.SectionName))
            .ValidateOnStart();

        EmailConfig emailConfig = builder.Configuration.GetSection(EmailConfig.SectionName).Get<EmailConfig>() ?? new EmailConfig();

        builder.Services
                .AddFluentEmail(emailConfig.From, emailConfig.DisplayName)
                .AddRazorRenderer()
                .AddMailKitSender(emailConfig.SmtpClientOptions);

        Email.DefaultRenderer = new RazorRenderer();
        Email.DefaultSender = new MailKitSender(emailConfig.SmtpClientOptions);

        builder.Services.AddScoped<IEmailService, EmailService>();

        return builder;
    }
}