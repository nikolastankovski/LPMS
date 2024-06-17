using System;
using System.Collections.Generic;

namespace LPMS.Domain.Models.Entities;

public partial class DepartmentxDivision
{
    public int DepartmentxDivisionID { get; set; }

    public int DepartmentId { get; set; }

    public int DivisionId { get; set; }

    public Guid CreatedBy { get; set; }

    public DateTime CreatedOn { get; set; }

    public virtual Department Department { get; set; } = null!;

    public virtual Division Division { get; set; } = null!;
}
