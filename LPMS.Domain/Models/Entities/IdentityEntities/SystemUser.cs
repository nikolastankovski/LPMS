using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace LPMS.Domain.Models.Entities.IdentityEntities
{
    public class SystemUser : IdentityUser<Guid>
    {
        public int PasswordChangePeriodInMonths { get; set; }

        [Column(TypeName = "datetime2(3)"), DataType(DataType.DateTime)]
        public DateTime LastPasswordChange { get; set; }

        [Column(TypeName = "datetime2(3)"), DataType(DataType.DateTime)]
        public DateTime LastLogin { get; set; }
    }
}
