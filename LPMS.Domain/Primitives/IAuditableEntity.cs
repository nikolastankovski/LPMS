namespace LPMS.Domain.Primitives;

public interface IAuditableEntity
{
    Guid CreatedBy { get; set; }
    DateTime CreatedOn { get; set; }
    Guid? ModifiedBy { get; set; }
    DateTime? ModifiedOn { get; set; }
}
