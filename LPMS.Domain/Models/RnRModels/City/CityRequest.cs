namespace LPMS.Domain.Models.RnRModels.City;

public class CityRequest
{
    public string Name_EN { get; set; } = null!;
    public string Name_MK { get; set; } = null!;
    public int CountryId { get; set; }
    public string PostalCode { get; set; }
    public bool IsActive { get; set; }
}