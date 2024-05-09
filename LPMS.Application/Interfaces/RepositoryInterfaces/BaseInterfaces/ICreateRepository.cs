using LanguageExt.Common;
using LPMS.Domain.Models.CustomModels;

namespace LPMS.Domain.Interfaces.RepositoryInterfaces.BaseInterfaces
{
    public interface ICreateRepository<TModel> where TModel : class
    {
        Result<TModel> Create(TModel entity, string culture);
        //Result<TModel> Create(List<TModel> entities, string culture);
        Task<Result<TModel>> CreateAsync(TModel entity, string culture);
        //Task<Result<TModel>> CreateAsync(List<TModel> entities, string culture);
    }
}
