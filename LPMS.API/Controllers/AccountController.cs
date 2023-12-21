using LPMS.Domain.DbModels.UserManagementModels;
using LPMS.Infrastructure.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace LPMS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManagementDbContext _userDbContext;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;

        public AccountController(UserManagementDbContext userDbContext, UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager)
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
                PhoneNumberConfirmed = true,
                CreatedOn = DateTime.Now
            };

            await _userManager.CreateAsync(user);

            await _userManager.AddToRoleAsync(user, "Administrator");

            return true;
        }
    }
}
