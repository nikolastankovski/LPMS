using LPMS.Domain.Models.RnRModels.City;

namespace LPMS.Application.Interfaces.ServiceInterfaces;

public interface ICityService
{
    Task<CityResponse?> GetByIdAsync(int id, CultureInfo ci);
    Task<Result<CreatedResponse<int>>> CreateAsync(CityRequest request, CultureInfo ci);
    Task<Result> ModifyAsync(int id, CityRequest request, CultureInfo ci);
    Task<Result> DeleteAsync(int id, CultureInfo ci);
}