namespace LPMS.Infrastructure.Repositories
{
    public class EmailHistoryRepository : IEmailHistoryRepository
    {
        private readonly LPMSDbContext _context;

        public EmailHistoryRepository(LPMSDbContext context)
        {
            _context = context;
        }
        public async Task<EmailHistory> CreateAsync(EmailHistory entity)
       {
            await _context.EmailHistories.AddAsync(entity);
            await _context.SaveChangesAsync();

            return entity;
        }

        public async Task UpdateAsync(EmailHistory entity)
        {
            _context.EmailHistories.Update(entity);
            await _context.SaveChangesAsync();  
        }
    }
}
