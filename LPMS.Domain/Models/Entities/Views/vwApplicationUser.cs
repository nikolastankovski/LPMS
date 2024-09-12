using System;
using System.Collections.Generic;

namespace LPMS.Domain.Models.Entities;

public partial class vwApplicationUser
{
    public Guid AccountId { get; set; }

    public Guid SystemUserId { get; set; }

    public string Name { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Role { get; set; } = null!;
}
