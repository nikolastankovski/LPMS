namespace LPMS.Domain.Models.Entities;

public partial class EndpointOperation : IAuditableEntity
{
    public int EndpointOperationID { get; set; }

    public int EndpointId { get; set; }

    public bool Create { get; set; }

    public bool Read { get; set; }

    public bool Update { get; set; }

    public bool Delete { get; set; }

    public DateTime CreatedOnUTC { get; set; }

    public Guid CreatedBy { get; set; }

    public Guid? ModifiedBy { get; set; }

    public DateTime? ModifiedOnUTC { get; set; }

    public virtual Endpoint Endpoint { get; set; } = null!;
}
