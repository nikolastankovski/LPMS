using LPMS.Domain.Models.DTO;

namespace LPMS.Application.Interfaces.ServiceInterfaces
{
    public interface IReferenceService
    {
        Task<List<DTOReference>> GetByReferenceTypeCodeAsync(string referenceTypeCode, CultureInfo ci);
    }
}