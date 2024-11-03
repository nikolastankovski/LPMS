using System.Linq.Expressions;

namespace LPMS.Application.Interfaces.RepositoryInterfaces.BaseInterfaces
{
    public interface IReadRepository<TModel, TPkType> 
        where TModel : class
        where TPkType : struct
    {
        List<TModel> Get(
            Expression<Func<TModel, bool>>? filter = null,
            Expression<Func<TModel, int, TModel>>? select = null,
            Func<IQueryable<TModel>, IOrderedQueryable<TModel>>? orderBy = null,
            string includeProperties = ""
        );
        Task<List<TModel>> GetAsync(
            Expression<Func<TModel, bool>>? filter = null,
            Expression<Func<TModel, int, TModel>>? select = null,
            Func<IQueryable<TModel>, IOrderedQueryable<TModel>>? orderBy = null,
            string includeProperties = ""
        );

        List<TModel> GetAll();
        Task<List<TModel>> GetAllAsync();
        TModel? GetById(TPkType id);
        Task<TModel?> GetByIdAsync(TPkType id);
    }
}
