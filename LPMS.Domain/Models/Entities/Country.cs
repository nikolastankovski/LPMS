﻿using System;
using System.Collections.Generic;

namespace LPMS.Application.Models.Entities;

public partial class Country
{
    public int CountryID { get; set; }

    public string? Name_EN { get; set; }

    public string? Name_MK { get; set; }

    public Guid CreatedBy { get; set; }

    public DateTime CreatedOn { get; set; }

    public Guid? ModifiedBy { get; set; }

    public DateTime? ModifiedOn { get; set; }

    public bool? IsActive { get; set; }

    public virtual ICollection<City> Cities { get; set; } = new List<City>();
}
