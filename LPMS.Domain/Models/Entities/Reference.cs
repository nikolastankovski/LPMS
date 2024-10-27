﻿namespace LPMS.Domain.Models.Entities;

public partial class Reference : IAuditableEntity
{
    public int ReferenceID { get; set; }

    public int ReferenceTypeId { get; set; }

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

    public virtual ReferenceType ReferenceType { get; set; } = null!;
}
