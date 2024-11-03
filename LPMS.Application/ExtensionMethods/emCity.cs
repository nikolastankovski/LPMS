using LPMS.Domain.Models.RnRModels.City;

namespace LPMS.Application.ExtensionMethods;

public static class emCity
{
    public static ValidationResult Validate(this City city, CultureInfo ci)
    {
        return new CityValidator(ci).Validate(city);
    }
    public static CityResponse? ToResponse(this City? city, CultureInfo ci)
    {
        if (city is null)
            return null;

        return new CityResponse()
        {
            Name = city.GetAttribute("Name_" + ci.TwoLetterISOLanguageName.ToUpper()),
            CountryId = city.CountryId,
            PostalCode = city.PostalCode,
            IsActive = city.IsActive ?? false
        };
    }

    public static City ToModel(this CityRequest? country)
    {
        if (country is null)
            return new City();

        return new City()
        {
            Name_EN = country.Name_EN,
            Name_MK = country.Name_MK,
            CountryId = country.CountryId,
            PostalCode = country.PostalCode,
            IsActive = country.IsActive
        };
    }
}