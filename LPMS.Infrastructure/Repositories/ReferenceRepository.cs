using System.Linq.Expressions;

namespace LPMS.Infrastructure.Repositories
{
    public class ReferenceRepository : BaseRepository<Reference, int>, IReferenceRepository
    {
        private readonly LPMSDbContext _context;

        public ReferenceRepository(LPMSDbContext context) : base(context)
        {
            _context = context;
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
