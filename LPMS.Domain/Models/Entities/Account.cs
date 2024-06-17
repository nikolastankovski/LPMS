using System;
using System.Collections.Generic;

namespace LPMS.Domain.Models.Entities;

public partial class Account
{
    public Guid AccountID { get; set; }

    public Guid SystemUserId { get; set; }

    public string Name { get; set; } = null!;

    public Guid CreatedBy { get; set; }

    public DateTime CreatedOn { get; set; }

    public Guid? ModifiedBy { get; set; }

    public DateTime? ModifiedOn { get; set; }

    public bool? IsActive { get; set; }

    public virtual ICollection<AccountxDepartmentxDivision> AccountxDepartmentxDivisions { get; set; } = new List<AccountxDepartmentxDivision>();
}
