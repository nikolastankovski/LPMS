using FluentValidation;
using LPMS.Domain.Models.RnRModels.UserModels;

namespace LPMS.Infrastructure.Repositories
{
    public class AccountRepository : BaseRepository<Account, Guid>, IAccountRepository
    {
        private readonly LPMSDbContext _context;
        public AccountRepository(LPMSDbContext context) : base(context) 
        {
            _context = context;
        }

        public override async Task ModifyAsync(Account entity)
        {
            await _context.Accounts
                .Where(x => x.AccountID == entity.AccountID)
                .ExecuteUpdateAsync(s => s
                    .SetProperty(x => x.Name, entity.Name)
                    .SetProperty(x => x.IsActive, entity.IsActive)
                );
        }

        public async Task<ApplicationUser?> GetApplicationUserAsync(string email)
        {
            return await _context.vwApplicationUsers
                                    .Where(x => x.Email == email)
                                    .Select(x => new ApplicationUser
                                    {
                                        Id = x.AccountId,
                                        Name = x.Name,
                                        Email = x.Email,
                                        Role = x.Role
                                    })
                                    .FirstOrDefaultAsync();
        }

        public async Task<ApplicationUser?> GetApplicationUserAsync(Guid accountId)
        {
            return await _context.vwApplicationUsers
                                    .Where(x => x.AccountId == accountId)
                                    .Select(x => new ApplicationUser
                                    {
                                        Id = x.AccountId,
                                        Name = x.Name,
                                        Email = x.Email,
                                        Role = x.Role
                                    })
                                    .FirstOrDefaultAsync();
        }
    }
}
 