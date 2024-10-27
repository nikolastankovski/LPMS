namespace LPMS.Application.Interfaces.RepositoryInterfaces
{
    public interface IReferenceRepository : IReadRepository<Reference, int>
    {
        List<Reference> GetByReferenceTypeCode(string referenceTypeCode);
        Task<List<Reference>> GetByReferenceTypeCodeAsync(string referenceTypeCode);
    }
}