using System.Globalization;
using LPMS.Domain.Models.RnRModels.City;

namespace LPMS.Infrastructure.Services;

public class CityService(ICityRepository cityRepository) : ICityService
{
    public async Task<CityResponse?> GetByIdAsync(int id, CultureInfo ci)
    {
        var city = await cityRepository.GetByIdAsync(id);

        return city.ToResponse(ci);
    }

    public async Task<Result<CreatedResponse<int>>> CreateAsync(CityRequest request, CultureInfo ci)
    {
        try
        {
            var city = request.ToModel();

            var validationResult = city.Validate(ci);

            if (!validationResult.IsValid)
                return Result.Fail(validationResult.GetErrors());
        
            await cityRepository.CreateAsync(city);

            return Result.Ok(new CreatedResponse<int>(true, city.CityID));
        }
        catch (Exception e)
        {
            Log.Error(exception: e, messageTemplate: e.ToMessageTemplate());
        }

        return Result.Fail(ci.GetResource(nameof(Resources.Unexpected_Error)));
    }

    public async Task<Result> ModifyAsync(int id, CityRequest request, CultureInfo ci)
    {
        try
        {
            var dbCity = await cityRepository.GetByIdAsync(id);

            if (dbCity is null)
                return Result.Fail(ci.GetResource(nameof(Resources.Entity_Not_Found)));
        
            dbCity.Name_EN = request.Name_EN;
            dbCity.Name_MK = request.Name_MK;
            dbCity.CountryId = request.CountryId;
            dbCity.PostalCode = request.PostalCode;
            dbCity.IsActive = request.IsActive;

            await cityRepository.ModifyAsync(dbCity);

            return Result.Ok();
        }
        catch (Exception e)
        {
            Log.Error(exception: e, messageTemplate: e.ToMessageTemplate());
        }

        return Result.Fail(ci.GetResource(nameof(Resources.Unexpected_Error)));
    }

    public async Task<Result> DeleteAsync(int id, CultureInfo ci)
    {
        try
        {
            var entity = await cityRepository.GetByIdAsync(id);
            
            if(entity is null)
                return Result.Fail(ci.GetResource(nameof(Resources.Entity_Not_Found)));
                
            await cityRepository.DeleteAsync(entity);
            
            return Result.Ok();
        }
        catch (Exception e)
        {
            Log.Error(exception: e, messageTemplate: e.ToMessageTemplate());
        }
        
        return Result.Fail(ci.GetAttribute(nameof(Resources.Unexpected_Error)));
    }
}