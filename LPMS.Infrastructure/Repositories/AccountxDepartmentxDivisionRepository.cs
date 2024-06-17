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
            throw new NotImplementedException();
        }

        public List<AccountxDepartmentxDivision> Create(List<AccountxDepartmentxDivision> entities)
        {
            throw new NotImplementedException();
        }

        public async Task<List<AccountxDepartmentxDivision>> CreateAsync(List<AccountxDepartmentxDivision> entities)
        {
            await _context.AccountxDepartmentxDivisions.AddRangeAsync(entities);
            await _context.SaveChangesAsync();

            return entities;
        }

        public Task<AccountxDepartmentxDivision> CreateAsync(AccountxDepartmentxDivision entity)
        {
            throw new NotImplementedException();
        }

        public List<AccountxDepartmentxDivision> Get(Expression<Func<AccountxDepartmentxDivision, bool>>? filter = null, Expression<Func<AccountxDepartmentxDivision, int, AccountxDepartmentxDivision>>? select = null, Func<IQueryable<AccountxDepartmentxDivision>, IOrderedQueryable<AccountxDepartmentxDivision>>? orderBy = null, string includeProperties = "")
        {
            throw new NotImplementedException();
        }

        public List<AccountxDepartmentxDivision> GetAll(bool? isActive = null)
        {
            throw new NotImplementedException();
        }

        public Task<List<AccountxDepartmentxDivision>> GetAllAsync(bool? isActive = null)
        {
            throw new NotImplementedException();
        }

        public Task<List<AccountxDepartmentxDivision>> GetAsync(Expression<Func<AccountxDepartmentxDivision, bool>>? filter = null, Expression<Func<AccountxDepartmentxDivision, int, AccountxDepartmentxDivision>>? select = null, Func<IQueryable<AccountxDepartmentxDivision>, IOrderedQueryable<AccountxDepartmentxDivision>>? orderBy = null, string includeProperties = "")
        {
            throw new NotImplementedException();
        }

        public AccountxDepartmentxDivision? GetById(object id)
        {
            throw new NotImplementedException();
        }

        public Task<AccountxDepartmentxDivision?> GetByIdAsync(object id)
        {
            throw new NotImplementedException();
        }
    }
}
