using System;
using System.Collections.Generic;

namespace LPMS.Application.Models.Entities;

public partial class Department
{
    public int DepartmentID { get; set; }

    public string Name_EN { get; set; } = null!;

    public string Name_MK { get; set; } = null!;

    public string Code { get; set; } = null!;

    public Guid CreatedBy { get; set; }

    public DateTime CreatedOn { get; set; }

    public Guid? ModifiedBy { get; set; }

    public DateTime? ModifiedOn { get; set; }

    public bool? IsActive { get; set; }

    public virtual ICollection<AccountxDepartmentxDivision> AccountxDepartmentxDivisions { get; set; } = new List<AccountxDepartmentxDivision>();

    public virtual ICollection<DepartmentxDivision> DepartmentxDivisions { get; set; } = new List<DepartmentxDivision>();
}
