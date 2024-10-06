using System.Globalization;

namespace LPMS.Application.Interfaces.RepositoryInterfaces.BaseInterfaces
{
    public interface IUpdateRepository<TModel> where TModel : class
    {
        void Update(TModel entity);
        //Result<TModel> Update(List<TModel> entities, Guid modifiedBy, string culture);
        Task UpdateAsync(TModel entity);
        //Task<Result<TModel>> UpdateAsync(List<TModel> entities, Guid modifiedBy, string culture);
    }
}