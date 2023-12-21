using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace LPMS.Domain.DbModels.UserManagementModels
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        public int PasswordChangePeriodInMonths { get; set; }

        [Column(TypeName = "datetime2(3)"), DataType(DataType.DateTime)]
        public DateTime LastPasswordChange { get; set; }

        [Column(TypeName = "datetime2(3)"), DataType(DataType.DateTime)]
        public DateTime LastLogin { get; set; }
        public Guid CreatedBy { get; set; }

        [Column(TypeName = "datetime2(3)"), DataType(DataType.DateTime)]
        public DateTime CreatedOn { get; set; }
        public Guid? ModifiedBy { get; set; }

        [Column(TypeName = "datetime2(3)"), DataType(DataType.DateTime)]
        public DateTime? ModifiedOn { get; set; }
    }
}
