using LanguageExt.Common;
using LPMS.Domain.Models.CustomModels;

namespace LPMS.Domain.Interfaces.RepositoryInterfaces.BaseInterfaces
{
    public interface IDeleteRepository<TModel> where TModel : class
    {
        Result<bool> Delete(object id);
        //Result<TModel> Delete(List<object> ids, string culture);
        Task<Result<bool>> DeleteAsync(object id);
        //Task<Result<TModel>> DeleteAsync(List<object> ids, string culture);
    }
}
