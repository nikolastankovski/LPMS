using System.ComponentModel.DataAnnotations;

namespace LPMS.Domain.Models.Entities;

public partial class Endpoint : IAuditableEntity
{
    public int EndpointID { get; set; }

    [MaxLength(256)]
    public string RequestPath { get; set; } = null!;
    public DateTime CreatedOnUTC { get; set; }

    public Guid CreatedBy { get; set; }

    public DateTime? ModifiedOnUTC { get; set; }

    public Guid? ModifiedBy { get; set; }
    
    public virtual ICollection<EndpointxSystemRole> EndpointxSystemRoles { get; set; } = new List<EndpointxSystemRole>();
}
