using LPMS.Domain.Models.CustomModels;

namespace LPMS.Domain.Interfaces.RepositoryInterfaces.BaseInterfaces
{
    public interface ICreateRepository<TModel> where TModel : class
    {
        CRUDResponse Create(TModel entity);
        CRUDResponse Create(List<TModel> entities);
        Task<CRUDResponse> CreateAsync(TModel entity);
        Task<CRUDResponse> CreateAsync(List<TModel> entities);
    }
}
