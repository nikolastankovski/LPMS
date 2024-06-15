using System;
using System.Collections.Generic;

namespace LPMS.Application.Models.Entities;

public partial class AccountxDepartmentxDivision
{
    public int AccountxDepartmentxDivisionID { get; set; }

    public Guid AccountId { get; set; }

    public int DepartmentId { get; set; }

    public int DivisionId { get; set; }

    public Guid CreatedBy { get; set; }

    public DateTime CreatedOn { get; set; }

    public virtual Account Account { get; set; } = null!;

    public virtual Department Department { get; set; } = null!;

    public virtual Division Division { get; set; } = null!;
}
