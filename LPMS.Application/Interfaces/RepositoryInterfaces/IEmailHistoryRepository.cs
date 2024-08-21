using Microsoft.EntityFrameworkCore.Metadata;

namespace LPMS.Application.Interfaces.RepositoryInterfaces
{
    public interface IEmailHistoryRepository 
    {
        Task<EmailHistory> CreateAsync(EmailHistory entity);
        Task UpdateAsync(EmailHistory entity);
    }
}
