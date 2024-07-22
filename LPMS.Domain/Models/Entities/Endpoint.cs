using System;
using System.Collections.Generic;

namespace LPMS.Domain.Models.Entities;

public partial class Endpoint
{
    public int EndpointID { get; set; }

    public string Controller { get; set; } = null!;

    public string Action { get; set; } = null!;

    public string Method { get; set; } = null!;

    public string Route { get; set; } = null!;

    public string FullPath { get; set; } = null!;

    public DateTime CreatedOn { get; set; }

    public Guid CreatedBy { get; set; }

    public DateTime ModifiedOn { get; set; }

    public Guid ModifiedBy { get; set; }

    public virtual ICollection<EndpointOperation> EndpointOperations { get; set; } = new List<EndpointOperation>();

    public virtual ICollection<EndpointxSystemRole> EndpointxSystemRoles { get; set; } = new List<EndpointxSystemRole>();
}
