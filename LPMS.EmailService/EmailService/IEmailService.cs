using FluentEmail.Core.Models;

namespace LPMS.EmailService.EmailService;

public interface IEmailService
{
    Task<bool> SendEmailAsync(EmailSetUp emailSetUp);
}