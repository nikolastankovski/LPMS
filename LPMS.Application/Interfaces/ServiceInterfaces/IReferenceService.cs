using LPMS.Domain.Models.RnRModels.ReferenceModels;

namespace LPMS.Application.Interfaces.ServiceInterfaces
{
    public interface IReferenceService
    {
        Task<ReferenceWReferenceTypeResponse> GetByReferenceTypeCodeAsync(string referenceTypeCode, CultureInfo ci);
    }
}