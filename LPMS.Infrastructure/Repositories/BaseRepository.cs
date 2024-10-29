using LPMS.Application.Interfaces.RepositoryInterfaces.BaseInterfaces;
using System.Linq.Expressions;

namespace LPMS.Infrastructure.Repositories
{
    public abstract class BaseRepository<TModel, PkType> : IBaseRepository<TModel, PkType> 
        where TModel : class
        where PkType : struct
    {
        private readonly LPMSDbContext _context;
        private DbSet<TModel> _entity;

        public BaseRepository(LPMSDbContext context)
        {
            _context = context;
            _entity = _context.Set<TModel>();
        }

        public virtual TModel Create(TModel entity)
        {
            _entity.Add(entity);
            _context.SaveChanges();

            return entity;
        }

        public virtual List<TModel> Create(List<TModel> entities)
        {
            _entity.AddRange(entities);
            _context.SaveChanges();

            return entities;
        }

        public virtual async Task<TModel> CreateAsync(TModel entity)
        {
            await _entity.AddAsync(entity);
            await _context.SaveChangesAsync();

            return entity;
        }

        public virtual async Task<List<TModel>> CreateAsync(List<TModel> entities)
        {
            await _entity.AddRangeAsync(entities);
            await _context.SaveChangesAsync();

            return entities;
        }

        public virtual void Delete(PkType id)
        {
            TModel? entity = _entity.Find(id);

            if (entity is null)
                throw new Exception("Entity not found!");

            _entity.Remove(entity);
            _context.SaveChanges();
        }

        public virtual async Task DeleteAsync(PkType id)
        {
            TModel? entity = await _entity.FindAsync(id);

            if (entity is null)
                throw new Exception("Entity not found!");

            _entity.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public virtual List<TModel> Get(Expression<Func<TModel, bool>>? filter = null, Expression<Func<TModel, int, TModel>>? select = null, Func<IQueryable<TModel>, IOrderedQueryable<TModel>>? orderBy = null, string includeProperties = "")
        {
            IQueryable<TModel> query = _entity;
            List<TModel> entities = new List<TModel>();

            query = includeProperties.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Aggregate(query, (current, includeProperty) => current.Include(includeProperty));

            if (filter != null)
                query = query.Where(filter);

            if (select != null)
                query = query.Select(select);

            if (orderBy != null)
                entities = orderBy(query).ToList();
            else
                entities = query.ToList();

            _context.ChangeTracker.Clear();

            return entities;
        }

        public List<TModel> GetAll()
        {
            return _entity.ToList();
        }

        public async Task<List<TModel>> GetAllAsync()
        {
            return await _entity.ToListAsync();
        }

        public virtual async Task<List<TModel>> GetAsync(Expression<Func<TModel, bool>>? filter = null, Expression<Func<TModel, int, TModel>>? select = null, Func<IQueryable<TModel>, IOrderedQueryable<TModel>>? orderBy = null, string includeProperties = "")
        {
            IQueryable<TModel> query = _entity;
            List<TModel> entities = new List<TModel>();

            query = includeProperties.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Aggregate(query, (current, includeProperty) => current.Include(includeProperty));

            if (filter != null)
                query = query.Where(filter);

            if (select != null)
                query = query.Select(select);

            if (orderBy != null)
                entities = await orderBy(query).ToListAsync();
            else
                entities = await query.ToListAsync();

            _context.ChangeTracker.Clear();

            return entities;
        }

        public virtual TModel? GetById(PkType id)
        {
            return _entity.Find(id);
        }

        public virtual async Task<TModel?> GetByIdAsync(PkType id)
        {
            return await _entity.FindAsync(id);
        }

        public virtual void Modify(TModel entity)
        {
            _entity.Update(entity);
            _context.SaveChanges();
        }

        public virtual async Task ModifyAsync(TModel entity)
        {
            _entity.Update(entity);
            await _context.SaveChangesAsync();
        }
    }
}
