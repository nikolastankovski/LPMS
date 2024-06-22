using System.Linq.Expressions;

namespace LPMS.Infrastructure.Repositories
{
    public class AccountxDepartmentxDivisionRepository : IAccountxDepartmentxDivisionRepository
    {
        private readonly LPMSDbContext _context;

        public AccountxDepartmentxDivisionRepository(LPMSDbContext context)
        {
            _context = context;
        }

        public AccountxDepartmentxDivision Create(AccountxDepartmentxDivision entity)
        {
            _context.AccountxDepartmentxDivisions.Add(entity);
            _context.SaveChanges();

            return entity;
        }

        public List<AccountxDepartmentxDivision> Create(List<AccountxDepartmentxDivision> entities)
        {
            _context.AccountxDepartmentxDivisions.AddRange(entities);
            _context.SaveChanges();

            return entities;
        }

        public async Task<List<AccountxDepartmentxDivision>> CreateAsync(List<AccountxDepartmentxDivision> entities)
        {
            await _context.AccountxDepartmentxDivisions.AddRangeAsync(entities);
            await _context.SaveChangesAsync();

            return entities;
        }

        public async Task<AccountxDepartmentxDivision> CreateAsync(AccountxDepartmentxDivision entity)
        {
            await _context.AccountxDepartmentxDivisions.AddAsync(entity);
            await _context.SaveChangesAsync();

            return entity;
        }

        public List<AccountxDepartmentxDivision> Get(Expression<Func<AccountxDepartmentxDivision, bool>>? filter = null, Expression<Func<AccountxDepartmentxDivision, int, AccountxDepartmentxDivision>>? select = null, Func<IQueryable<AccountxDepartmentxDivision>, IOrderedQueryable<AccountxDepartmentxDivision>>? orderBy = null, string includeProperties = "")
        {
            IQueryable<AccountxDepartmentxDivision> query = _context.AccountxDepartmentxDivisions;
            List<AccountxDepartmentxDivision> entities = new List<AccountxDepartmentxDivision>();

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

        public List<AccountxDepartmentxDivision> GetAll(bool? isActive = null)
        {
            return _context.AccountxDepartmentxDivisions.ToList();
        }

        public Task<List<AccountxDepartmentxDivision>> GetAllAsync(bool? isActive = null)
        {
            return _context.AccountxDepartmentxDivisions.ToListAsync();
        }

        public async Task<List<AccountxDepartmentxDivision>> GetAsync(Expression<Func<AccountxDepartmentxDivision, bool>>? filter = null, Expression<Func<AccountxDepartmentxDivision, int, AccountxDepartmentxDivision>>? select = null, Func<IQueryable<AccountxDepartmentxDivision>, IOrderedQueryable<AccountxDepartmentxDivision>>? orderBy = null, string includeProperties = "")
        {
            IQueryable<AccountxDepartmentxDivision> query = _context.AccountxDepartmentxDivisions;
            List<AccountxDepartmentxDivision> entities = new List<AccountxDepartmentxDivision>();

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

        public AccountxDepartmentxDivision? GetById(object id)
        {
            return _context.AccountxDepartmentxDivisions.Find(id);
        }

        public async Task<AccountxDepartmentxDivision?> GetByIdAsync(object id)
        {
            return await _context.AccountxDepartmentxDivisions.FindAsync(id);
        }

        public async Task<List<AccountxDepartmentxDivision>> GetByAccountIdAsync(Guid accountId)
        {
            return await _context.AccountxDepartmentxDivisions
                                    .Include(x => x.Department)
                                    .Include(x => x.Division)
                                    .Where(x => x.AccountId == accountId)
                                    .ToListAsync();
        }
        public async Task<List<AccountxDepartmentxDivision>> GetBySystemUserIdAsync(Guid systemUserId)
        {
            return await _context.AccountxDepartmentxDivisions
                                    .Include(x => x.Account)
                                    .Include(x => x.Department)
                                    .Include(x => x.Division)
                                    .Where(x => x.Account.SystemUserId == systemUserId)
                                    .ToListAsync();
        }
    }
}
