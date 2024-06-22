namespace LPMS.Domain.Models.RnRModels.ReferenceModels
{
    public class ReferenceResponse
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string Code { get; set; } = null!;
    }
}
