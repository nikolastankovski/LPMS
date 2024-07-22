using System;
using System.Collections.Generic;

namespace LPMS.Domain.Models.Entities;

public partial class EndpointxSystemRole
{
    public int EndpointxSystemRoleID { get; set; }

    public int EndpointId { get; set; }

    public Guid SystemRoleId { get; set; }

    public DateTime CreatedOn { get; set; }

    public Guid CreatedBy { get; set; }

    public virtual Endpoint Endpoint { get; set; } = null!;
}
