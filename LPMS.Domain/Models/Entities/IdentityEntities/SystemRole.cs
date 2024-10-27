using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace LPMS.Domain.Models.Entities.IdentityEntities
{
    public class SystemRole : IdentityRole<Guid>, IAuditableEntity
    {
        [Column(TypeName = "nvarchar(256)"), DataType(DataType.Text)]
        public string DisplayName_EN { get; set; } = string.Empty;

        [Column(TypeName = "nvarchar(256)"), DataType(DataType.Text)]
        public string DisplayName_MK { get; set; } = string.Empty;
        public Guid CreatedBy { get; set; }

        [Column(TypeName = "datetime2(3)"), DataType(DataType.DateTime)]
        public DateTime CreatedOnUTC { get; set; }
        public Guid? ModifiedBy { get; set; }

        [Column(TypeName = "datetime2(3)"), DataType(DataType.DateTime)]
        public DateTime? ModifiedOnUTC { get; set; }

        [Column(TypeName = "bit")]
        public bool? IsActive { get; set; }
    }
}
