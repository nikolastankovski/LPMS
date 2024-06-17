using FluentValidation;
using System.Globalization;
using System.Linq.Expressions;

namespace LPMS.Infrastructure.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private readonly LPMSDbContext _context;
        public AccountRepository(LPMSDbContext context)
        {
            _context = context;
        }

        public Account Create(Account entity)
        {
            _context.Accounts.Add(entity);
            _context.SaveChanges();

            return entity;
        }

        public async Task<Account> CreateAsync(Account entity)
        {
            await _context.Accounts.AddAsync(entity);
            await _context.SaveChangesAsync();

            return entity;
        }

        public List<Account> Create(List<Account> entities)
        {
            _context.Accounts.AddRange(entities);
            _context.SaveChanges();

            return entities;
        }

        public async Task<List<Account>> CreateAsync(List<Account> entities)
        {
            await _context.Accounts.AddRangeAsync(entities);
            await _context.SaveChangesAsync();

            return entities;
        }

        public bool Delete(object id)
        {
            try
            {
                _context.Accounts.Where(x => x.AccountID == (Guid)id).ExecuteDelete();

                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public async Task<bool> DeleteAsync(object id)
        {
            try
            {
                await _context.Accounts.Where(x => x.AccountID == (Guid)id).ExecuteDeleteAsync();

                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public List<Account> Get(Expression<Func<Account, bool>>? filter = null, Expression<Func<Account, int, Account>>? select = null, Func<IQueryable<Account>, IOrderedQueryable<Account>>? orderBy = null, string includeProperties = "")
        {
            IQueryable<Account> query = _context.Accounts;
            List<Account> entities = new List<Account>();

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

        public List<Account> GetAll(bool? isActive = null)
        {
            IQueryable<Account> entities = _context.Accounts;

            if (isActive != null) entities = entities.Where(x => x.IsActive == isActive);

            return entities.ToList();
        }

        public async Task<List<Account>> GetAllAsync(bool? isActive = null)
        {
            IQueryable<Account> entities = _context.Accounts;

            if (isActive != null) entities = entities.Where(x => x.IsActive == isActive);

            return await entities.ToListAsync();
        }

        public async Task<List<Account>> GetAsync(Expression<Func<Account, bool>>? filter = null, Expression<Func<Account, int, Account>>? select = null, Func<IQueryable<Account>, IOrderedQueryable<Account>>? orderBy = null, string includeProperties = "")
        {
            IQueryable<Account> query = _context.Accounts;
            List<Account> entities = new List<Account>();

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

        public Account? GetById(object id)
        {
            return _context.Accounts.Find(id);
        }

        public async Task<Account?> GetByIdAsync(object id)
        {
            return await _context.Accounts.FindAsync(id);
        }

        public void Update(Account entity, Guid modifiedBy)
        {
            _context.Accounts
                    .Where(x => x.AccountID == entity.AccountID)
                    .ExecuteUpdate(setters => setters
                        .SetProperty(x => x.Name, entity.Name)
                        .SetProperty(x => x.ModifiedBy, modifiedBy)
                        .SetProperty(x => x.ModifiedOn, DateTime.Now)
                        .SetProperty(x => x.IsActive, entity.IsActive)
                    );
        }

        public async Task UpdateAsync(Account entity, Guid modifiedBy)
        {
            await _context.Accounts
                        .Where(x => x.AccountID == entity.AccountID)
                        .ExecuteUpdateAsync(setters => setters
                            .SetProperty(x => x.Name, entity.Name)
                            .SetProperty(x => x.ModifiedBy, modifiedBy)
                            .SetProperty(x => x.ModifiedOn, DateTime.Now)
                            .SetProperty(x => x.IsActive, entity.IsActive)
                        );
        }
    }
}
