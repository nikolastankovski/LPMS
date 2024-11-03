using System.Globalization;
using FluentResults;
using LPMS.Domain.Models.RnRModels;
using LPMS.Domain.Models.RnRModels.Country;

namespace LPMS.Infrastructure.Services;

public class CountryService(ICountryRepository countryRepository) : ICountryService
{
    public async Task<CountryResponse?> GetByIdAsync(int id, CultureInfo ci)
    {
        var country = await countryRepository.GetByIdAsync(id);

        return country.ToResponse(ci);
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

            if (dbCountry == null)
                return Result.Fail(ci.GetResource(nameof(Resources.Entity_Not_Found)));
            
            dbCountry.Name_EN = request.Name_EN;
            dbCountry.Name_MK = request.Name_MK;
            dbCountry.IsActive = request.IsActive;
            
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
            var entity = await countryRepository.GetByIdAsync(id);
            
            if(entity is null)
                return Result.Fail(ci.GetResource(nameof(Resources.Entity_Not_Found)));

            await countryRepository.DeleteAsync(entity);
            
            return Result.Ok();
        }
        catch (Exception e)
        {
            Log.Error(exception: e, messageTemplate: e.ToMessageTemplate());
        }
        
        return Result.Fail(ci.GetAttribute(nameof(Resources.Unexpected_Error)));
    }
}