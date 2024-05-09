using LanguageExt.Common;
using LPMS.Domain.Models.CustomModels;

namespace LPMS.Domain.Interfaces.RepositoryInterfaces.BaseInterfaces
{
    public interface IUpdateRepository<TModel> where TModel : class
    {
        Result<bool> Update(TModel entity, Guid modifiedBy, string culture);
        //Result<TModel> Update(List<TModel> entities, Guid modifiedBy, string culture);
        Task<Result<bool>> UpdateAsync(TModel entity, Guid modifiedBy, string culture);
        //Task<Result<TModel>> UpdateAsync(List<TModel> entities, Guid modifiedBy, string culture);
    }
}