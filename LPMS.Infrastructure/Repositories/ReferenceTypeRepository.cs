using System.Linq.Expressions;
using LPMS.Infrastructure.Repositories.BaseRepositories;

namespace LPMS.Infrastructure.Repositories
{
    public class ReferenceTypeRepository : BaseRepository<ReferenceType, int>, IReferenceTypeRepository
    {
        public ReferenceTypeRepository(LPMSDbContext context) : base(context) 
        {
        }
    }
}