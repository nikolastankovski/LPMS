using LPMS.Domain.Interfaces.RepositoryInterfaces.BaseInterfaces;
using LPMS.Infrastructure.DbContexts;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace LPMS.Infrastructure.Repositories.BaseRepositories
{
    public class BaseRepository<TModel> : IBaseRepository<TModel> where TModel : class
    {
        private readonly LPMSDbContext _context;
        private new DbSet<TModel> _entity;

        public BaseRepository(LPMSDbContext context)
        {
            _context = context;
            _entity = _context.Set<TModel>();
        }

        public CRUDResponse Create(TModel entity)
        {
            throw new NotImplementedException();
        }

        public CRUDResponse Create(List<TModel> entities)
        {
            throw new NotImplementedException();
        }

        public Task<CRUDResponse> CreateAsync(TModel entity)
        {
            throw new NotImplementedException();
        }

        public Task<CRUDResponse> CreateAsync(List<TModel> entities)
        {
            throw new NotImplementedException();
        }

        public CRUDResponse Delete(object id)
        {
            throw new NotImplementedException();
        }

        public CRUDResponse Delete(List<object> ids)
        {
            throw new NotImplementedException();
        }

        public Task<CRUDResponse> DeleteAsync(object id)
        {
            throw new NotImplementedException();
        }

        public Task<CRUDResponse> DeleteAsync(List<object> ids)
        {
            throw new NotImplementedException();
        }

        public List<TModel> Get(Expression<Func<TModel, bool>>? filter = null, Expression<Func<TModel, int, TModel>>? select = null, Func<IQueryable<TModel>, IOrderedQueryable<TModel>>? orderBy = null, string includeProperties = "")
        {
            throw new NotImplementedException();
        }

        public List<TModel> GetAll(bool? isActive = null)
        {
            throw new NotImplementedException();
        }

        public Task<List<TModel>> GetAllAsync(bool? isActive = null)
        {
            throw new NotImplementedException();
        }

        public Task<List<TModel>> GetAsync(Expression<Func<TModel, bool>>? filter = null, Expression<Func<TModel, int, TModel>>? select = null, Func<IQueryable<TModel>, IOrderedQueryable<TModel>>? orderBy = null, string includeProperties = "")
        {
            throw new NotImplementedException();
        }

        public TModel? GetById(object id)
        {
            throw new NotImplementedException();
        }

        public Task<TModel?> GetByIdAsync(object id)
        {
            throw new NotImplementedException();
        }

        public CRUDResponse Update(TModel entity)
        {
            throw new NotImplementedException();
        }

        public CRUDResponse Update(List<TModel> entities)
        {
            throw new NotImplementedException();
        }

        public Task<CRUDResponse> UpdateAsync(TModel entity)
        {
            throw new NotImplementedException();
        }

        public Task<CRUDResponse> UpdateAsync(List<TModel> entities)
        {
            throw new NotImplementedException();
        }
    }
}
