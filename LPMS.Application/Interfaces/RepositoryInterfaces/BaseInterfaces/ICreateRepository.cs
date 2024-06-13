using LanguageExt.Common;
using LPMS.Domain.Models.CustomModels;
using System.Globalization;

namespace LPMS.Domain.Interfaces.RepositoryInterfaces.BaseInterfaces
{
    public interface ICreateRepository<TModel> where TModel : class
    {
        CRUDResult Create(TModel entity, CultureInfo culture);
        //Result<TModel> Create(List<TModel> entities, string culture);
        Task<Result<TModel>> CreateAsync(TModel entity, string culture);
        //Task<Result<TModel>> CreateAsync(List<TModel> entities, string culture);
    }
}
