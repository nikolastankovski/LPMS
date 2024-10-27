using System.Linq.Expressions;

namespace LPMS.Application.Interfaces.RepositoryInterfaces.BaseInterfaces
{
    public interface IReadRepository<TModel, PkType> 
        where TModel : class
        where PkType : struct
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

        List<TModel> GetAll(bool? isActive = null);
        Task<List<TModel>> GetAllAsync(bool? isActive = null);
        TModel? GetById(PkType id);
        Task<TModel?> GetByIdAsync(PkType id);
    }
}
