using System;
using System.Collections.Generic;

namespace LPMS.Domain.Models.Entities;

public partial class EndpointOperation
{
    public int EndpointOperationID { get; set; }

    public int EndpointId { get; set; }

    public bool Create { get; set; }

    public bool Read { get; set; }

    public bool Update { get; set; }

    public bool Delete { get; set; }

    public DateTime CreatedOn { get; set; }

    public Guid CreatedBy { get; set; }

    public virtual Endpoint Endpoint { get; set; } = null!;
}
