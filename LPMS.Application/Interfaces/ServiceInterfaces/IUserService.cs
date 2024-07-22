using FluentResults;
using LPMS.Domain.Models.RnRModels.UserManagementModels;

namespace LPMS.Application.Interfaces.ServiceInterfaces
{
    public interface IUserService
    {
        Task<Result<CreateUserResponse>> CreateApplicationUserAsync(CreateUserRequest request, CultureInfo culture);
    }
}
