using FluentEmail.Core.Models;

namespace LPMS.EmailService.EmailService;

public interface IEmailService
{
    Task<SendResponse> SendEmailAsync(EmailSetUp emailSetUp);
}