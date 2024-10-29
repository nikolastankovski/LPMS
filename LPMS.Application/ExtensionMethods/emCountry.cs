using LPMS.Domain.Models.RnRModels.Country;

namespace LPMS.Application.ExtensionMethods;

public static class emCountry
{
    public static ValidationResult Validate(this Country country, CultureInfo ci)
    {
        return new CountryValidator(ci).Validate(country);
    }
    public static CountryResponse? ToResponse(this Country? country, CultureInfo ci)
    {
        if (country is null)
            return null;

        return new CountryResponse()
        {
            Name = country.GetAttribute("Name_" + ci.TwoLetterISOLanguageName.ToUpper()),
            IsActive = country.IsActive ?? false
        };
    }

    public static Country ToModel(this CountryRequest? country)
    {
        if (country is null)
            return new Country();

        return new Country()
        {
            Name_EN = country.Name_EN,
            Name_MK = country.Name_MK,
            IsActive = country.IsActive
        };
    }
}