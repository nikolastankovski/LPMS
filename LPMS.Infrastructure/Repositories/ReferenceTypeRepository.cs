using System.Linq.Expressions;

namespace LPMS.Infrastructure.Repositories
{
    public class ReferenceTypeRepository : BaseRepository<ReferenceType, int>, IReferenceTypeRepository
    {
        public ReferenceTypeRepository(LPMSDbContext context) : base(context) 
        {
        }
    }
}