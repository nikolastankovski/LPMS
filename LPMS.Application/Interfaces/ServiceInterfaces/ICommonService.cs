using LPMS.Domain.Models.RnRModels.UserModels;

namespace LPMS.Application.Interfaces.ServiceInterfaces
{
    public interface ICommonService
    {
        LoggedInUser? GetLoggedInUser();
    }
}
