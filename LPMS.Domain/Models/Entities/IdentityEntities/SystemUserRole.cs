﻿using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace LPMS.Domain.Models.Entities.IdentityEntities
{
    public class SystemUserRole : IdentityUserRole<Guid>
    {
        public Guid CreatedBy { get; set; }

        //[Column(TypeName = "datetime2(3)"), DataType(DataType.DateTime)]
        [Column(TypeName = "TIMESTAMP(3)"), DataType(DataType.DateTime)]
        public DateTime CreatedOn { get; set; }
    }
}
