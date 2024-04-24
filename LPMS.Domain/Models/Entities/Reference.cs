using System;
using System.Collections.Generic;

namespace LPMS.Domain.Models.Entities;

public partial class Reference
{
    public int ReferenceID { get; set; }

    public int ReferenceTypeId { get; set; }

    public string Name_EN { get; set; } = null!;

    public string Name_MK { get; set; } = null!;

    public string? Description_EN { get; set; }

    public string? Description_MK { get; set; }

    public string Code { get; set; } = null!;

    public Guid CreatedBy { get; set; }

    public DateTime CreatedOn { get; set; }

    public Guid? ModifiedBy { get; set; }

    public DateTime? ModifiedOn { get; set; }

    public bool IsActive { get; set; }

    public virtual ReferenceType ReferenceType { get; set; } = null!;
}
