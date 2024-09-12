namespace LPMS.Domain.Models.Entities;

public partial class Division : IAuditableEntity
{
    public int DivisionID { get; set; }

    public string Name_EN { get; set; } = null!;

    public string Name_MK { get; set; } = null!;

    public string Code { get; set; } = null!;

    public Guid CreatedBy { get; set; }

    public DateTime CreatedOn { get; set; }

    public Guid? ModifiedBy { get; set; }

    public DateTime? ModifiedOn { get; set; }

    public bool? IsActive { get; set; }

    public virtual ICollection<Department> Departments { get; set; } = new List<Department>();
}
