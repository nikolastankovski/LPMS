using System;
using System.Collections.Generic;

namespace LPMS.Domain.DbModels;

public partial class Department
{
    public Guid DepartmentId { get; set; }

    public string Name { get; set; } = null!;

    public string Code { get; set; } = null!;

    public string CreatedBy { get; set; } = null!;

    public DateTime CreatedOn { get; set; }

    public string? ModifiedBy { get; set; }

    public DateTime? ModifiedOn { get; set; }

    public bool IsActive { get; set; }
}
