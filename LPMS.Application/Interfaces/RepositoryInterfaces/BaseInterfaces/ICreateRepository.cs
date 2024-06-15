using System.Globalization;

namespace LPMS.Application.Interfaces.RepositoryInterfaces.BaseInterfaces
{
    public interface ICreateRepository<TModel> where TModel : class
    {
        TModel Create(TModel entity);
        //Result<TModel> Create(List<TModel> entities, string culture);
        Task<TModel> CreateAsync(TModel entity, CultureInfo culture);
        //Task<Result<TModel>> CreateAsync(List<TModel> entities, string culture);
    }
}
