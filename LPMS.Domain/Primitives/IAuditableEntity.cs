using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace LPMS.Domain.Primitives;

public interface IAuditableEntity
{
    Guid CreatedBy { get; set; }
    [Column(TypeName = "datetime2(3)"), DataType(DataType.DateTime)]
    DateTime CreatedOnUTC { get; set; }
    Guid? ModifiedBy { get; set; }
    [Column(TypeName = "datetime2(3)"), DataType(DataType.DateTime)]
    DateTime? ModifiedOnUTC { get; set; }
}
