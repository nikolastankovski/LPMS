namespace LPMS.Domain.Models.RnRModels.Country;

public class CountryRequest
{
    public string Name_EN { get; set; }
    public string Name_MK { get; set; }
    public bool IsActive { get; set; } = true;
}