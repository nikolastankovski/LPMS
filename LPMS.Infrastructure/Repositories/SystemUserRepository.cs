﻿using Microsoft.AspNetCore.Identity;

namespace LPMS.Infrastructure.Repositories
{
    public class SystemUserRepository : ISystemUserRepository
    {
        private readonly UserManager<SystemUser> _userManager;
        private readonly RoleManager<SystemRole> _roleManager;

        public SystemUserRepository(UserManager<SystemUser> userManager, RoleManager<SystemRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<SystemUser> CreateAsync(SystemUser systemUser)
        {
            var createResult = await _userManager.CreateAsync(systemUser, Helper.GeneratePassword());

            if (!createResult.Succeeded)
                throw new Exception(createResult.Errors.Select(x => x.Description).FirstOrDefault());

            return await GetUserByEmailAsync(systemUser.Email);
        }

        public async Task<SystemUser> UpdateAsync(SystemUser systemUser)
        {
            var updateResult = await _userManager.UpdateAsync(systemUser);

            if (!updateResult.Succeeded)
                throw new Exception(updateResult.Errors.Select(x => x.Description).FirstOrDefault());

            return systemUser;
        }

        public async Task AddToRoleAsync(SystemUser systemUser, string role)
        {
            await _userManager.AddToRoleAsync(systemUser, role);
        }

        public async Task UpdateUserRoleAsync(SystemUser systemUser, string role)
        {
            await _userManager.RemoveFromRoleAsync(systemUser, role);
            await _userManager.AddToRoleAsync(systemUser, role);
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
            var sysUser = await GetUserByIdAsync(userId);

            if(sysUser == null)
                return false;

            return await _userManager.IsInRoleAsync(sysUser, role);
        }

        public async Task<bool> IsUserInRolesAsync(string userId, List<string> roles)
        {
            var sysUser = await GetUserByIdAsync(userId);

            if (sysUser == null) 
                return false;

            foreach(var role in roles)
            {
                if (await _userManager.IsInRoleAsync(sysUser, role) == false)
                    return false;
            }

            return true;
        }

        public async Task<bool> IsCorrectPasswordAsync(SystemUser user, string password)
        {
            return await _userManager.CheckPasswordAsync(user, password);
        }

        public async Task<bool> IsEmailUsedAsync(string? email)
        {
            if(string.IsNullOrEmpty(email))
                return false;

            var sysUser = await GetUserByEmailAsync(email);

            if(sysUser == null)
                return false;

            return true;
        }

        public async Task DeleteAsync(Guid id)
        {
            var sysUser = await GetUserByIdAsync(id);

            if(sysUser is not null)
                await _userManager.DeleteAsync(sysUser);
        }
    }
}
