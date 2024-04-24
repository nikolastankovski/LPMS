using LPMS.Infrastructure.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace LPMS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserIdentityDbContext _userDbContext;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;

        public AccountController(UserIdentityDbContext userDbContext, UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _userDbContext = userDbContext;
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
