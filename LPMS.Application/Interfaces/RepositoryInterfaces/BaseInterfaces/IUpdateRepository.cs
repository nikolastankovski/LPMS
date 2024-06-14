using LanguageExt.Common;
using LPMS.Domain.Models.CustomModels;
using System.Globalization;

namespace LPMS.Domain.Interfaces.RepositoryInterfaces.BaseInterfaces
{
    public interface IUpdateRepository<TModel> where TModel : class
    {
        CRUDResult Update(TModel entity, Guid modifiedBy, CultureInfo culture);
        //Result<TModel> Update(List<TModel> entities, Guid modifiedBy, string culture);
        Task<CRUDResult> UpdateAsync(TModel entity, Guid modifiedBy, CultureInfo culture);
        //Task<Result<TModel>> UpdateAsync(List<TModel> entities, Guid modifiedBy, string culture);
    }
}