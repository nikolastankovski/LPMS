using LPMS.Infrastructure.Services.SharedServices;
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

        public CRUDResponse Create(Account entity)
        {
            try
            {
                _context.Accounts.Add(entity);
                _context.SaveChanges();

                return new CRUDResponse { IsSuccess = true, Message = Resources.Create_Success };
            }
            catch (Exception e)
            {
                Logger.Log(e);
                return new CRUDResponse { IsSuccess = false, Message = Resources.Unexpected_Error };
            }
        }

        public CRUDResponse Create(List<Account> entities)
        {
            try
            {
                _context.Accounts.AddRange(entities);
                _context.SaveChanges();

                return new CRUDResponse { IsSuccess = true, Message = Resources.Create_Success };
            }
            catch (Exception e)
            {
                Logger.Log(e);
                return new CRUDResponse { IsSuccess = false, Message = Resources.Unexpected_Error };
            }
        }

        public async Task<CRUDResponse> CreateAsync(Account entity)
        {
            try
            {
                await _context.Accounts.AddAsync(entity);
                await _context.SaveChangesAsync();

                return new CRUDResponse { IsSuccess = true, Message = Resources.Create_Success };
            }
            catch (Exception e)
            {
                Logger.Log(e);
                return new CRUDResponse { IsSuccess = false, Message = Resources.Unexpected_Error };
            }
        }

        public async Task<CRUDResponse> CreateAsync(List<Account> entities)
        {
            try
            {
                await _context.Accounts.AddRangeAsync(entities);
                await _context.SaveChangesAsync();

                return new CRUDResponse { IsSuccess = true, Message = Resources.Create_Success };
            }
            catch (Exception e)
            {
                Logger.Log(e);
                return new CRUDResponse { IsSuccess = false, Message = Resources.Unexpected_Error };
            }
        }

        public CRUDResponse Delete(object id)
        {
            try
            {
                _context.Accounts.Where(x => x.AccountID == (Guid)id).ExecuteDelete();

                return new CRUDResponse { IsSuccess = true, Message = Resources.Delete_Success };
            }
            catch (Exception e)
            {
                Logger.Log(e);
                return new CRUDResponse { IsSuccess = false, Message = Resources.Unexpected_Error };
            }
        }

        public CRUDResponse Delete(List<object> ids)
        {
            try
            {
                _context.Accounts.Where(x => ids.Contains(x.AccountID)).ExecuteDelete();

                return new CRUDResponse { IsSuccess = true, Message = Resources.Delete_Success };
            }
            catch (Exception e)
            {
                Logger.Log(e);
                return new CRUDResponse { IsSuccess = false, Message = Resources.Unexpected_Error };
            }
        }

        public async Task<CRUDResponse> DeleteAsync(object id)
        {
            try
            {
                await _context.Accounts.Where(x => x.AccountID == (Guid)id).ExecuteDeleteAsync();

                return new CRUDResponse { IsSuccess = true, Message = Resources.Delete_Success };
            }
            catch (Exception e)
            {
                Logger.Log(e);
                return new CRUDResponse { IsSuccess = false, Message = Resources.Unexpected_Error };
            }
        }

        public async Task<CRUDResponse> DeleteAsync(List<object> ids)
        {
            try
            {
                await _context.Accounts.Where(x => ids.Contains(x.AccountID)).ExecuteDeleteAsync();

                return new CRUDResponse { IsSuccess = true, Message = Resources.Delete_Success };
            }
            catch (Exception e)
            {
                Logger.Log(e);
                return new CRUDResponse { IsSuccess = false, Message = Resources.Unexpected_Error };
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

        public CRUDResponse Update(Account entity, Guid modifiedBy)
        {
            try
            {
                _context.Accounts
                    .Where(x => x.AccountID == entity.AccountID)
                    .ExecuteUpdate(setters => setters
                        .SetProperty(x => x.Name, entity.Name)
                        .SetProperty(x => x.ModifiedBy, modifiedBy)
                        .SetProperty(x => x.ModifiedOn, DateTime.Now)
                    );

                return new CRUDResponse { IsSuccess = true, Message = Resources.Update_Success };
            }
            catch (Exception)
            {
                return new CRUDResponse { IsSuccess = false, Message = Resources.Unexpected_Error };
            }
        }

        public CRUDResponse Update(List<Account> entities, Guid modifiedBy)
        {
            try
            {
                foreach (var entity in entities)
                {
                    Update(entity, modifiedBy);
                }

                return new CRUDResponse { IsSuccess = true, Message = Resources.Update_Success };
            }
            catch (Exception e)
            {
                Logger.Log(e);
                return new CRUDResponse { IsSuccess = false, Message = Resources.Unexpected_Error };
            }
        }

        public async Task<CRUDResponse> UpdateAsync(Account entity, Guid modifiedBy)
        {
            try
            {
                await _context.Accounts
                        .Where(x => x.AccountID == entity.AccountID)
                        .ExecuteUpdateAsync(setters => setters
                            .SetProperty(x => x.Name, entity.Name)
                            .SetProperty(x => x.ModifiedBy, modifiedBy)
                            .SetProperty(x => x.ModifiedOn, DateTime.Now)
                        );

                return new CRUDResponse { IsSuccess = true, Message = Resources.Update_Success };
            }
            catch (Exception e)
            {
                Logger.Log(e);
                return new CRUDResponse { IsSuccess = false, Message = Resources.Unexpected_Error };
            }
        }

        public async Task<CRUDResponse> UpdateAsync(List<Account> entities, Guid modifiedBy)
        {
            try
            {
                foreach (var entity in entities)
                {
                    await UpdateAsync(entity, modifiedBy);
                }

                return new CRUDResponse { IsSuccess = true, Message = Resources.Update_Success };
            }
            catch (Exception e)
            {
                Logger.Log(e);
                return new CRUDResponse { IsSuccess = false, Message = Resources.Unexpected_Error };
            }
        }
    }
}
