using System.Globalization;
using FluentResults;
using LPMS.Domain.Models.RnRModels;
using LPMS.Domain.Models.RnRModels.Country;

namespace LPMS.Infrastructure.Services;

public class CountryService(ICountryRepository countryRepository) : ICountryService
{
    public async Task<Result<CountryResponse?>> GetByIdAsync(int id, CultureInfo ci)
    {
        try
        {
            var country = await countryRepository.GetByIdAsync(id);

            return Result.Ok(country.ToResponse(ci));
        }
        catch (Exception e)
        {
            Log.Error(exception: e, messageTemplate: e.ToMessageTemplate());
        }
        
        return Result.Fail(ci.GetAttribute(nameof(Resources.Unexpected_Error)));
    }

    public async Task<Result<CreatedResponse<int>>> CreateAsync(CountryRequest request, CultureInfo ci)
    {
        try
        {
            var country = request.ToModel();

            var validationResult = country.Validate(ci);

            if (!validationResult.IsValid)
                return Result.Fail(validationResult.GetErrors());
        
            await countryRepository.CreateAsync(country);

            return Result.Ok(new CreatedResponse<int>(true, country.CountryID));
        }
        catch (Exception e)
        {
            Log.Error(exception: e, messageTemplate: e.ToMessageTemplate());
        }
        
        return Result.Fail(ci.GetAttribute(nameof(Resources.Unexpected_Error)));
    }
    
    public async Task<Result> ModifyAsync(int id, CountryRequest request, CultureInfo ci)
    {
        try
        {
            var dbCountry = await countryRepository.GetByIdAsync(id);

            if (dbCountry is null)
                Result.Fail(ci.GetResource(nameof(Resources.Entity_Not_Found)));
            
            var requestCountry = request.ToModel();
            
            dbCountry.Name_EN = requestCountry.Name_EN;
            dbCountry.Name_MK = requestCountry.Name_MK;
            dbCountry.IsActive = requestCountry.IsActive;
            
            var validationResult = dbCountry.Validate(ci);

            if (!validationResult.IsValid)
                return Result.Fail(validationResult.GetErrors());
        
            await countryRepository.ModifyAsync(dbCountry);

            return Result.Ok();
        }
        catch (Exception e)
        {
            Log.Error(exception: e, messageTemplate: e.ToMessageTemplate());
        }
        
        return Result.Fail(ci.GetAttribute(nameof(Resources.Unexpected_Error)));
    }
    
    public async Task<Result> DeleteAsync(int id, CultureInfo ci)
    {
        try
        {
            await countryRepository.DeleteAsync(id);
            
            return Result.Ok();
        }
        catch (Exception e)
        {
            Log.Error(exception: e, messageTemplate: e.ToMessageTemplate());
        }
        
        return Result.Fail(ci.GetAttribute(nameof(Resources.Unexpected_Error)));
    }
}