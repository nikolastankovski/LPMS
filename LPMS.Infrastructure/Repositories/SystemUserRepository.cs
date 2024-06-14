using Microsoft.AspNetCore.Identity;

namespace LPMS.Infrastructure.Repositories
{
    public class SystemUserRepository : ISystemUserRepository
    {
        private readonly LPMSDbContext _context;
        private readonly UserManager<SystemUser> _userManager;
        private readonly RoleManager<SystemRole> _roleManager;

        public SystemUserRepository(LPMSDbContext context, UserManager<SystemUser> userManager, RoleManager<SystemRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public List<SystemUser> GetAllUsers()
        {
            return _userManager.Users.ToList();
        }

        public async Task<List<SystemUser>> GetAllUsersAsync()
        {
            return await _userManager.Users.ToListAsync();
        }

        public List<SystemRole> GetAllRoles()
        {
            return _roleManager.Roles.ToList();
        }

        public async Task<List<SystemRole>> GetAllRolesAsync()
        {
            return await _roleManager.Roles.ToListAsync();
        }

        public async Task<List<SystemRole>> GetUserRolesAsync(string userId)
        {
            var user = await GetUserByIdAsync(userId);

            if (user == null) return new List<SystemRole>();

            var roles = await _userManager.GetRolesAsync(user);

            if(!roles.Any()) return new List<SystemRole>();

            var userRoles = await _roleManager.Roles.Where(x => roles.Contains(x.Name)).ToListAsync();

            return userRoles;
        }

        public async Task<List<SystemRole>> GetUserRolesAsync(SystemUser user)
        {
            if (user == null) return new List<SystemRole>();

            var roles = await _userManager.GetRolesAsync(user);

            if (!roles.Any()) return new List<SystemRole>();

            var userRoles = await _roleManager.Roles.Where(x => roles.Contains(x.Name)).ToListAsync();

            return userRoles;
        }

        public async Task<SystemUser?> GetUserByIdAsync(string id)
        {
            return await _userManager.FindByIdAsync(id);
        }

        public async Task<SystemUser?> GetUserByIdAsync(Guid id)
        {
            return await _userManager.FindByIdAsync(id.ToString());
        }

        public async Task<SystemUser?> GetUserByEmailAsync(string email)
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

        public async Task<bool> IsCorrectPassword(SystemUser user, string password)
        {
            return await _userManager.CheckPasswordAsync(user, password);
        }
    }
}
