using FluentEmail.Core;
using LPMS.Application.ExtensionMethods;
using Serilog;

namespace LPMS.EmailService.EmailService;

public class EmailService : IEmailService
{
    private readonly IFluentEmail _email;

    public EmailService(IFluentEmail email)
    {
        _email = email;
    }

    public async Task<bool> SendEmailAsync(EmailSetUp es)
    {
        try
        {
            _email.Subject(es.Subject);

            if (es.From is not null)
                _email.SetFrom(es.From.EmailAddress, es.From.Name);

            _email.To(es.To.EmailAddress);

            if (es.CC.Any())
                _email.CC(es.CC);

            if (es.BCC.Any())
                _email.BCC(es.BCC);

            _email.UsingCultureTemplateFromFile(es.EmailTemplate, es.Tokens, es.Culture);

            if (es.Attachments.Any())
                _email.Attach(es.Attachments);

            var result = await _email.SendAsync();

            if (!result.Successful)
                Log.Error("Error while sending e-mail! Errors: " + string.Join("; ", result.ErrorMessages));

            return result.Successful;
        }
        catch (Exception e)
        {
            Log.Error(exception: e, messageTemplate: e.ToMessageTemplate());
            return false;
        }
    }
}