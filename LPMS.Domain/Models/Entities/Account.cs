namespace LPMS.Domain.Models.Entities;

public partial class Account : IAuditableEntity
{
    public Guid AccountID { get; set; }

    public Guid SystemUserId { get; set; }

    public string Name { get; set; } = null!;

    public Guid CreatedBy { get; set; }

    public DateTime CreatedOnUTC { get; set; }

    public Guid? ModifiedBy { get; set; }

    public DateTime? ModifiedOnUTC { get; set; }

    public bool? IsActive { get; set; }
}
