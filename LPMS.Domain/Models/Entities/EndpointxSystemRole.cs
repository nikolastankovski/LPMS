namespace LPMS.Domain.Models.Entities;

public partial class EndpointxSystemRole : IAuditableEntity
{
    public int EndpointxSystemRoleID { get; set; }

    public int EndpointId { get; set; }

    public Guid SystemRoleId { get; set; }

    public DateTime CreatedOnUTC { get; set; }

    public Guid CreatedBy { get; set; }

    public Guid? ModifiedBy { get; set; }

    public DateTime? ModifiedOnUTC { get; set; }

    public virtual Endpoint Endpoint { get; set; } = null!;
}
