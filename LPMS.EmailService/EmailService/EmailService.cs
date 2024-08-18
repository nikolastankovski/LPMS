using FluentEmail.Core;
using FluentEmail.Core.Models;
using LPMS.Domain.Models.Entities;

namespace LPMS.EmailService.EmailService;

public class EmailService : IEmailService
{
    private readonly IFluentEmail _email;

    public EmailService(IFluentEmail email)
    {
        _email = email;
    }

    public async Task<SendResponse> SendEmailAsync(EmailSetUp es)
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

        return await _email.SendAsync();
    }
}