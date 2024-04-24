using LPMS.Infrastructure.Data;
using LPMS.Infrastructure.DbContexts;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace LPMS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly LPMSDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;

        public AccountController(LPMSDbContext context, UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        [HttpPost]
        public async Task<bool> InsertAdminUser()
        {
            ApplicationUser user = new ApplicationUser()
            {
                UserName = "admin",
                Email = "stankovski.n@hotmail.com",
                PhoneNumber = "1234567890",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true
            };

            await _userManager.CreateAsync(user);

            await _userManager.AddToRoleAsync(user, "Administrator");

            return true;
        }
    }
}
