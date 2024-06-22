namespace LPMS.Domain.Models.RnRModels.ReferenceModels
{
    public class ReferenceWReferenceTypeResponse
    {
        public ReferenceResponse ReferenceType { get; set; } = null!;
        public List<ReferenceResponse> References { get; set; } = new List<ReferenceResponse>();
    }
}
