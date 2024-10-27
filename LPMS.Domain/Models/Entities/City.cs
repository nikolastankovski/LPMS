namespace LPMS.Domain.Models.Entities;

public partial class City : IAuditableEntity
{
    public int CityID { get; set; }

    public int CountryId { get; set; }

    public string? Name_EN { get; set; }

    public string Name_MK { get; set; } = null!;

    public string PostalCode { get; set; } = null!;

    public Guid CreatedBy { get; set; }

    public DateTime CreatedOnUTC { get; set; }

    public Guid? ModifiedBy { get; set; }

    public DateTime? ModifiedOnUTC { get; set; }

    public bool? IsActive { get; set; }

    public virtual Country Country { get; set; } = null!;
}
