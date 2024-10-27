using LPMS.Domain.Models.RnRModels.UserManagementModels;

namespace LPMS.Application.Interfaces.RepositoryInterfaces
{
    public interface IAccountRepository : IBaseRepository<Account, Guid>
    {
        Task<ApplicationUser?> GetApplicationUserAsync(string email);
        Task<ApplicationUser?> GetApplicationUserAsync(Guid accountId);
    }
}
