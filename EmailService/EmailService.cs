using FluentEmail.Core;
using FluentEmail.Core.Models;

namespace EmailService
{
    public class EmailService : IEmailService
    {
        public async Task<SendResponse> SendEmailAsync(EmailSetUp es)
        {
            var email = new Email();

            email.Subject(es.Subject);

            if (es.From is not null)
                email.SetFrom(es.From.EmailAddress, es.From.Name);

            email.To(es.To.EmailAddress);

            if (es.CC.Any())
                email.CC(es.CC);

            if (es.BCC.Any())
                email.BCC(es.BCC);

            email.UsingCultureTemplateFromFile(es.EmailTemplate, es.Tokens, es.Culture);

            if (es.Attachments.Any())
                email.Attach(es.Attachments);

            return await email.SendAsync();
        }
    }
}