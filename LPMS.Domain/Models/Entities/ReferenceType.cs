﻿namespace LPMS.Domain.Models.Entities;

public partial class ReferenceType : IAuditableEntity
{
    public int ReferenceTypeID { get; set; }

    public string Name_EN { get; set; } = null!;

    public string Name_MK { get; set; } = null!;

    public string? Description_EN { get; set; }

    public string? Description_MK { get; set; }

    public string Code { get; set; } = null!;

    public Guid CreatedBy { get; set; }

    public DateTime CreatedOnUTC { get; set; }

    public Guid? ModifiedBy { get; set; }

    public DateTime? ModifiedOnUTC { get; set; }

    public bool? IsActive { get; set; }

    public virtual ICollection<Reference> References { get; set; } = new List<Reference>();
}
