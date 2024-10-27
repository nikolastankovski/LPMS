using FluentResults;
using LPMS.Domain.Models.RnRModels;
using LPMS.Domain.Models.RnRModels.UserManagementModels;

namespace LPMS.Application.Interfaces.ServiceInterfaces
{
    public interface IUserService
    {
        Task<Result<ApplicationUser>> GetAppUserAsyncById(Guid id, CultureInfo culture);
        Task<Result<CreatedResponse<Guid>>> CreateAppUserAsync(CreateModifyUserRequest request, CultureInfo culture);
        Task<Result> ModifyAppUserAsync(Guid id, CreateModifyUserRequest request, CultureInfo culture);
        Task<Result> DeleteAppUserAsync(Guid id, CultureInfo culture);
    }
}
