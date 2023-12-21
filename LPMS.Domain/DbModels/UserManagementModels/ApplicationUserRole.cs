using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace LPMS.Domain.DbModels.UserManagementModels
{
    public class ApplicationUserRole : IdentityUserRole<Guid>
    {
        public Guid CreatedBy { get; set; }

        [Column(TypeName = "datetime2(3)"), DataType(DataType.DateTime)]
        public DateTime CreatedOn { get; set; }
        public Guid? ModifiedBy { get; set; }

        [Column(TypeName = "datetime2(3)"), DataType(DataType.DateTime)]
        public DateTime? ModifiedOn { get; set; }
    }
}
