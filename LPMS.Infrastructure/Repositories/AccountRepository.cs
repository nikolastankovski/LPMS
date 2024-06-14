using LanguageExt.Common;
using LPMS.Application.ExtensionMethods;
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

        public CRUDResult Create(Account entity, CultureInfo culture)
        {
            var validation = entity.Validate(culture);

            if (!validation.IsValid)
                return new CRUDResult(Errors: validation.GetErrors(), IsSuccess: false);

            try
            {
                _context.Accounts.Add(entity);
                _context.SaveChanges();

                return new CRUDResult(IsSuccess: true);
            }
            catch (Exception e)
            {
                Log.Error(exception: e, messageTemplate: e.Message);

                string? unexpectedError = Resources.ResourceManager.GetString(nameof(Resources.Unexpected_Error), culture);
                return new CRUDResult(Errors: [unexpectedError], IsSuccess: false);
            }
        }

        public async Task<CRUDResult> CreateAsync(Account entity, CultureInfo culture)
        {
            var validation = entity.Validate(culture);

            if (!validation.IsValid)
                return new CRUDResult(Errors: validation.GetErrors(), IsSuccess: false);

            try
            {
                await _context.Accounts.AddAsync(entity);
                await _context.SaveChangesAsync();

                return new CRUDResult(IsSuccess: true);
            }
            catch (Exception e)
            {
                Log.Error(exception: e, messageTemplate: e.Message);

                string? unexpectedError = Resources.ResourceManager.GetString(nameof(Resources.Unexpected_Error), culture);
                return new CRUDResult(Errors: [unexpectedError], IsSuccess: false);
            }
        }

        public Result<bool> Delete(object id)
        {
            try
            {
                _context.Accounts.Where(x => x.AccountID == (Guid)id).ExecuteDelete();

                return true;
            }
            catch (Exception e)
            {
                return new Result<bool>(e);
            }
        }

        public async Task<Result<bool>> DeleteAsync(object id)
        {
            try
            {
                await _context.Accounts.Where(x => x.AccountID == (Guid)id).ExecuteDeleteAsync();

                return true;
            }
            catch (Exception e)
            {
                return new Result<bool>(e);
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

            if(isActive != null) entities = entities.Where(x => x.IsActive == isActive);

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

        public CRUDResult Update(Account entity, Guid modifiedBy, CultureInfo culture)
        {
            var validation = entity.Validate(culture);

            if (!validation.IsValid)
                return new CRUDResult(Errors: validation.GetErrors(), IsSuccess: false);

            try
            {
                _context.Accounts
                    .Where(x => x.AccountID == entity.AccountID)
                    .ExecuteUpdate(setters => setters
                        .SetProperty(x => x.Name, entity.Name)
                        .SetProperty(x => x.ModifiedBy, modifiedBy)
                        .SetProperty(x => x.ModifiedOn, DateTime.Now)
                        .SetProperty(x => x.IsActive, entity.IsActive)
                    );

                return new CRUDResult(IsSuccess: true);
            }
            catch (Exception e)
            {
                Log.Error(exception: e, messageTemplate: e.Message);

                string? unexpectedError = Resources.ResourceManager.GetString(nameof(Resources.Unexpected_Error), culture);
                return new CRUDResult(Errors: [unexpectedError], IsSuccess: false);
            }
        }

        public async Task<CRUDResult> UpdateAsync(Account entity, Guid modifiedBy, CultureInfo culture)
        {
            var validation = entity.Validate(culture);

            if (!validation.IsValid)
                return new CRUDResult(Errors: validation.GetErrors(), IsSuccess: false);

            try
            {
                await _context.Accounts
                        .Where(x => x.AccountID == entity.AccountID)
                        .ExecuteUpdateAsync(setters => setters
                            .SetProperty(x => x.Name, entity.Name)
                            .SetProperty(x => x.ModifiedBy, modifiedBy)
                            .SetProperty(x => x.ModifiedOn, DateTime.Now)
                            .SetProperty(x => x.IsActive, entity.IsActive)
                        );

                return new CRUDResult(IsSuccess: true);
            }
            catch (Exception e)
            {
                Log.Error(exception: e, messageTemplate: e.Message);

                string? unexpectedError = Resources.ResourceManager.GetString(nameof(Resources.Unexpected_Error), culture);
                return new CRUDResult(Errors: [unexpectedError], IsSuccess: false);
            }
        }
    }
}
