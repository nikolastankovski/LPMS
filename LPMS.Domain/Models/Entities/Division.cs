using System;
using System.Collections.Generic;

namespace LPMS.Infrastructure;

public partial class Division
{
    public int DivisionID { get; set; }

    public int DepartmentId { get; set; }

    public string Name_EN { get; set; } = null!;

    public string Name_MK { get; set; } = null!;

    public string? Code { get; set; }

    public Guid CreatedBy { get; set; }

    public DateTime CreatedOn { get; set; }

    public Guid? ModifiedBy { get; set; }

    public DateTime? ModifiedOn { get; set; }

    public bool IsActive { get; set; }

    public virtual Department Department { get; set; } = null!;
}
