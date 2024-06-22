namespace LPMS.Domain.Models.RnRModels.NewFolder
{
    public class DepartmentxDivisionRequest
    {
        public int Department { get; set; }
        public List<int> Divisions { get; set; } = null!;
    }
}
