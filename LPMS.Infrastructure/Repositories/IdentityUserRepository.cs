using Microsoft.AspNetCore.Identity;

namespace LPMS.Infrastructure.Repositories
{
    public class IdentityUserRepository
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

        public async Task<List<ApplicationRole>> GetUserRoles(ApplicationUser user)
        {
            var roles = await _roleManager.Roles.ToListAsync();
            return roles.ToList();
        }

        public async Task<ApplicationUser?> GetUserById(string id)
        {
            return await _userManager.FindByIdAsync(id);
        }

        public async Task<ApplicationUser?> GetUserByEmail(string email)
        {
            return await _userManager.FindByEmailAsync(email);
        }
    }
}
