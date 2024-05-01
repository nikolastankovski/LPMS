using System.Linq.Expressions;

namespace LPMS.Infrastructure.Repositories
{
    public class ReferenceTypeRepository : IReferenceTypeRepository
    {
        private readonly LPMSDbContext _context;
        public ReferenceTypeRepository(LPMSDbContext context)
        {
            _context = context;
        }
        public List<ReferenceType> Get(Expression<Func<ReferenceType, bool>>? filter = null, Expression<Func<ReferenceType, int, ReferenceType>>? select = null, Func<IQueryable<ReferenceType>, IOrderedQueryable<ReferenceType>>? orderBy = null, string includeProperties = "")
        {
            IQueryable<ReferenceType> query = _context.ReferenceTypes;
            List<ReferenceType> entities = new List<ReferenceType>();

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

        public List<ReferenceType> GetAll(bool? isActive = null)
        {
            IQueryable<ReferenceType> entities = _context.ReferenceTypes;

            if (isActive != null) entities = entities.Where(x => x.IsActive == isActive);

            return entities.ToList();
        }

        public async Task<List<ReferenceType>> GetAllAsync(bool? isActive = null)
        {
            IQueryable<ReferenceType> entities = _context.ReferenceTypes;

            if (isActive != null) entities = entities.Where(x => x.IsActive == isActive);

            return await entities.ToListAsync();
        }

        public async Task<List<ReferenceType>> GetAsync(Expression<Func<ReferenceType, bool>>? filter = null, Expression<Func<ReferenceType, int, ReferenceType>>? select = null, Func<IQueryable<ReferenceType>, IOrderedQueryable<ReferenceType>>? orderBy = null, string includeProperties = "")
        {
            IQueryable<ReferenceType> query = _context.ReferenceTypes;
            List<ReferenceType> entities = new List<ReferenceType>();

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

        public ReferenceType? GetById(object id)
        {
            return _context.ReferenceTypes.Find(id);
        }

        public async Task<ReferenceType?> GetByIdAsync(object id)
        {
            return await _context.ReferenceTypes.FindAsync(id);
        }
    }
}
