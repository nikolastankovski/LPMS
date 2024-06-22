using LPMS.Domain.Models.RnRModels.ReferenceModels;
using System.Globalization;

namespace LPMS.Infrastructure.Services
{
    public class ReferenceService : IReferenceService
    {
        private readonly IReferenceRepository _referenceRepository;

        public ReferenceService(IReferenceRepository referenceRepository)
        {
            _referenceRepository = referenceRepository;
        }

        public async Task<ReferenceWReferenceTypeResponse> GetByReferenceTypeCodeAsync(string referenceTypeCode, CultureInfo ci)
        {
            var references = await _referenceRepository.GetByReferenceTypeCodeAsync(referenceTypeCode);

            return references.ToRefWRefTypeResponse(ci);
        }
    }
}
