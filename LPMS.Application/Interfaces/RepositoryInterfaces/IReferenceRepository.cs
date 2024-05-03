using LPMS.Domain.Interfaces.RepositoryInterfaces.BaseInterfaces;
using LPMS.Domain.Models.Entities;

namespace LPMS.Domain.Interfaces.RepositoryInterfaces
{
    public interface IReferenceRepository : IReadRepository<Reference>
    {
        List<Reference> GetByReferenceTypeCode(string referenceTypeCode);
        Task<List<Reference>> GetByReferenceTypeCodeAsync(string referenceTypeCode);
    }
}