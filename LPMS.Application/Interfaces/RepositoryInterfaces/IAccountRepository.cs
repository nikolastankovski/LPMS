using LPMS.Domain.Models.RnRModels.UserManagementModels;

namespace LPMS.Application.Interfaces.RepositoryInterfaces
{
    public interface IAccountRepository : IBaseRepository<Account>
    {
        Task<ApplicationUserResponse?> GetApplicationUserAsync(string email);
        Task<ApplicationUserResponse?> GetApplicationUserAsync(Guid accountId);
    }
}
