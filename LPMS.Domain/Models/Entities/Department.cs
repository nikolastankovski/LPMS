namespace LPMS.Domain.Models.Entities;

public partial class Department : IAuditableEntity
{
    public int DepartmentID { get; set; }

    public int DivisionId { get; set; }

    public string Name_EN { get; set; } = null!;

    public string Name_MK { get; set; } = null!;

    public string Code { get; set; } = null!;

    public Guid CreatedBy { get; set; }

    public DateTime CreatedOnUTC { get; set; }

    public Guid? ModifiedBy { get; set; }

    public DateTime? ModifiedOnUTC { get; set; }

    public bool? IsActive { get; set; }

    public virtual Division Division { get; set; } = null!;
}
