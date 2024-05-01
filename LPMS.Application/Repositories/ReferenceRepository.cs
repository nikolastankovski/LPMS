using LPMS.Infrastructure.Services.SharedServices;
using System.Linq.Expressions;

namespace LPMS.Infrastructure.Repositories
{
    public class ReferenceRepository : IReferenceRepository
    {
        private readonly LPMSDbContext _context;
        public ReferenceRepository(LPMSDbContext context) 
        {
            _context = context;
        }
        public List<Reference> Get(Expression<Func<Reference, bool>>? filter = null, Expression<Func<Reference, int, Reference>>? select = null, Func<IQueryable<Reference>, IOrderedQueryable<Reference>>? orderBy = null, string includeProperties = "")
        {
            IQueryable<Reference> query = _context.References;
            List<Reference> entities = new List<Reference>();

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

        public List<Reference> GetAll(bool? isActive = null)
        {
            IQueryable<Reference> entities = _context.References;

            if (isActive != null) entities = entities.Where(x => x.IsActive == isActive);

            return entities.ToList();
        }

        public async Task<List<Reference>> GetAllAsync(bool? isActive = null)
        {
            IQueryable<Reference> entities = _context.References;

            if (isActive != null) entities = entities.Where(x => x.IsActive == isActive);

            return await entities.ToListAsync();
        }

        public async Task<List<Reference>> GetAsync(Expression<Func<Reference, bool>>? filter = null, Expression<Func<Reference, int, Reference>>? select = null, Func<IQueryable<Reference>, IOrderedQueryable<Reference>>? orderBy = null, string includeProperties = "")
        {
            IQueryable<Reference> query = _context.References;
            List<Reference> entities = new List<Reference>();

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

        public Reference? GetById(object id)
        {
            return _context.References.Find(id);
        }

        public async Task<Reference?> GetByIdAsync(object id)
        {
            return await _context.References.FindAsync(id);
        }

        public List<Reference> GetByReferenceTypeCode(string referenceTypeCode)
        {
            List<Reference> entities = _context.References
                                                    .Include(x => x.ReferenceType)
                                                    .Where(x => x.ReferenceType.Code == referenceTypeCode)
                                                    .ToList();

            return entities;
        }
        public async Task<List<Reference>> GetByReferenceTypeCodeAsync(string referenceTypeCode)
        {
            List<Reference> entities = await _context.References
                                                    .Include(x => x.ReferenceType)
                                                    .Where(x => x.ReferenceType.Code == referenceTypeCode)
                                                    .ToListAsync();

            return entities;
        }
    }
}
