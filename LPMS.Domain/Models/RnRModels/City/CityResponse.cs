namespace LPMS.Domain.Models.RnRModels.City;

public class CityResponse
{
    public string Name { get; set; } = null!;
    public int CountryId { get; set; }
    public string PostalCode { get; set; }
    public bool IsActive { get; set; }
}