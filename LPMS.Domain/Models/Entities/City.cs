using System;
using System.Collections.Generic;

namespace LPMS.Application.Models.Entities;

public partial class City
{
    public int CityID { get; set; }

    public int CountryId { get; set; }

    public string? Name_EN { get; set; }

    public string Name_MK { get; set; } = null!;

    public string PostalCode { get; set; } = null!;

    public Guid CreatedBy { get; set; }

    public DateTime CreatedOn { get; set; }

    public Guid? ModifiedBy { get; set; }

    public DateTime? ModifiedOn { get; set; }

    public bool? IsActive { get; set; }

    public virtual Country Country { get; set; } = null!;
}
