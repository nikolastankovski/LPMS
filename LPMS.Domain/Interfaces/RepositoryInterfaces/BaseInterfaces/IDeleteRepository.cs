using LPMS.Domain.Models.CustomModels;

namespace LPMS.Domain.Interfaces.RepositoryInterfaces.BaseInterfaces
{
    public interface IDeleteRepository<TModel> where TModel : class
    {
        CRUDResponse Delete(object id);
        CRUDResponse Delete(List<object> ids);
        Task<CRUDResponse> DeleteAsync(object id);
        Task<CRUDResponse> DeleteAsync(List<object> ids);
    }
}
