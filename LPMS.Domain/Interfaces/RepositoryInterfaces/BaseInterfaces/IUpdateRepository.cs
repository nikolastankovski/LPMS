using LPMS.Domain.Models.CustomModels;

namespace LPMS.Domain.Interfaces.RepositoryInterfaces.BaseInterfaces
{
    public interface IUpdateRepository<TModel> where TModel : class
    {
        CRUDResponse Update(TModel entity);
        CRUDResponse Update(List<TModel> entities);
        Task<CRUDResponse> UpdateAsync(TModel entity);
        Task<CRUDResponse> UpdateAsync(List<TModel> entities);
    }
}