﻿namespace LPMS.Application.Interfaces.RepositoryInterfaces
{
    public interface ISystemUserRepository
    {
        Task<SystemUser> CreateAsync(SystemUser systemUser);
        Task AddToRoleAsync(SystemUser systemUser, string role);
        List<SystemUser> GetAllUsers();
        Task<List<SystemUser>> GetAllUsersAsync();
        Task<List<SystemRole>> GetAllRolesAsync();
        Task<List<SystemRole>> GetUserRolesAsync(string userId);
        Task<List<SystemRole>> GetUserRolesAsync(SystemUser user);
        Task<SystemUser?> GetUserByIdAsync(string id);
        Task<SystemUser?> GetUserByIdAsync(Guid id);
        Task<SystemUser?> GetUserByEmailAsync(string email);
        Task<bool> IsUserInRoleAsync(string userId, string role);
        Task<bool> IsUserInRolesAsync(string userId, List<string> roles);
        Task<bool> IsCorrectPassword(SystemUser user, string password);
    }
}