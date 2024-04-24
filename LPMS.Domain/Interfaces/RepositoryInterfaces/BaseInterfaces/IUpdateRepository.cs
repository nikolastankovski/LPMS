using LPMS.Domain.Models.CustomModels;

namespace LPMS.Domain.Interfaces.RepositoryInterfaces.BaseInterfaces
{
    public interface IUpdateRepository<TModel> where TModel : class
    {
        CRUDResponse Update(TModel entity, Guid modifiedBy);
        CRUDResponse Update(List<TModel> entities, Guid modifiedBy);
        Task<CRUDResponse> UpdateAsync(TModel entity, Guid modifiedBy);
        Task<CRUDResponse> UpdateAsync(List<TModel> entities, Guid modifiedBy);
    }
}