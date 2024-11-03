using LPMS.Domain.Models.RnRModels.UserModels;

namespace LPMS.Application.Interfaces.RepositoryInterfaces;

public interface IAccountRepository : IBaseRepository<Account, Guid>
{
    Task<ApplicationUser?> GetApplicationUserAsync(string email);
    Task<ApplicationUser?> GetApplicationUserAsync(Guid accountId);
}