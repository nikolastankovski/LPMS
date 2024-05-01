using LPMS.Infrastructure.Nomenclature;
using Microsoft.AspNetCore.Identity;

namespace LPMS.Infrastructure.Repositories
{
    public class IdentityUserRepository : IIdentityUserRepository
    {
        private readonly LPMSDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;

        public IdentityUserRepository(LPMSDbContext context, UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public List<ApplicationUser> GetAllUsers()
        {
            return _userManager.Users.ToList();
        }

        public async Task<List<ApplicationUser>> GetAllUsersAsync()
        {
            return await _userManager.Users.ToListAsync();
        }

        public List<ApplicationRole> GetAllRoles()
        {
            return _roleManager.Roles.ToList();
        }

        public async Task<List<ApplicationRole>> GetAllRolesAsync()
        {
            return await _roleManager.Roles.ToListAsync();
        }

        public async Task<List<ApplicationRole>> GetUserRolesAsync(string userId)
        {
            var user = await GetUserByIdAsync(userId);

            if (user == null) return new List<ApplicationRole>();

            var roles = await _userManager.GetRolesAsync(user);

            if(!roles.Any()) return new List<ApplicationRole>();

            var userRoles = await _roleManager.Roles.Where(x => roles.Contains(x.Name)).ToListAsync();

            return userRoles;
        }

        public async Task<List<ApplicationRole>> GetUserRolesAsync(ApplicationUser user)
        {
            if (user == null) return new List<ApplicationRole>();

            var roles = await _userManager.GetRolesAsync(user);

            if (!roles.Any()) return new List<ApplicationRole>();

            var userRoles = await _roleManager.Roles.Where(x => roles.Contains(x.Name)).ToListAsync();

            return userRoles;
        }

        public async Task<ApplicationUser?> GetUserByIdAsync(string id)
        {
            return await _userManager.FindByIdAsync(id);
        }

        public async Task<ApplicationUser?> GetUserByIdAsync(Guid id)
        {
            return await _userManager.FindByIdAsync(id.ToString());
        }

        public async Task<ApplicationUser?> GetUserByEmailAsync(string email)
        {
            return await _userManager.FindByEmailAsync(email);
        }

        public async Task<bool> IsUserInRoleAsync(string userId, string role) 
        {
            var user = await GetUserByIdAsync(userId);

            return await _userManager.IsInRoleAsync(user, role);
        }

        public async Task<bool> IsUserInRolesAsync(string userId, List<string> roles)
        {
            var user = await GetUserByIdAsync(userId);

            foreach(var role in roles)
            {
                if (await _userManager.IsInRoleAsync(user, role) == false)
                    return false;
            }

            return true;
        }

        public async Task<bool> IsCorrectPassword(ApplicationUser user, string password)
        {
            return await _userManager.CheckPasswordAsync(user, password);
        }
    }
}
