namespace LPMS.Domain.Models.DTO
{
    public class DTODepartmentxDivisions
    {
        public int Department { get; set; }
        public List<int> Divisions { get; set; } = null!;
    }
}
