using FluentEmail.Core.Models;

namespace EmailService
{
    public interface IEmailService
    {
        Task<SendResponse> SendEmailAsync(EmailSetUp emailSetUp);
    }
}
