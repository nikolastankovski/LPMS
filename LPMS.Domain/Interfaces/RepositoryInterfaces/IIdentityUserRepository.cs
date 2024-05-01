using LPMS.Domain.Models.Entities.IdentityEntities;

namespace LPMS.Domain.Interfaces.RepositoryInterfaces
{
    public interface IIdentityUserRepository
    {
        List<ApplicationUser> GetAllUsers();
        Task<List<ApplicationUser>> GetAllUsersAsync();
        Task<List<ApplicationRole>> GetAllRolesAsync();
        Task<List<ApplicationRole>> GetUserRolesAsync(string userId);
        Task<List<ApplicationRole>> GetUserRolesAsync(ApplicationUser user);
        Task<ApplicationUser?> GetUserByIdAsync(string id);
        Task<ApplicationUser?> GetUserByIdAsync(Guid id);
        Task<ApplicationUser?> GetUserByEmailAsync(string email);
        Task<bool> IsUserInRoleAsync(string userId, string role);
        Task<bool> IsUserInRolesAsync(string userId, List<string> roles);
        Task<bool> IsCorrectPassword(ApplicationUser user, string password);
    }
}