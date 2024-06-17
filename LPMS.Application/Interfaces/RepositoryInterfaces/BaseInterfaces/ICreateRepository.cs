using System.Globalization;

namespace LPMS.Application.Interfaces.RepositoryInterfaces.BaseInterfaces
{
    public interface ICreateRepository<TModel> where TModel : class
    {
        TModel Create(TModel entity);
        List<TModel> Create(List<TModel> entities);
        Task<TModel> CreateAsync(TModel entity);
        Task<List<TModel>> CreateAsync(List<TModel> entities);
    }
}
