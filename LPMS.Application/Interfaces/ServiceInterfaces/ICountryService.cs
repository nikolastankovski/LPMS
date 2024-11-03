using LPMS.Domain.Models.RnRModels;
using LPMS.Domain.Models.RnRModels.Country;

namespace LPMS.Application.Interfaces.ServiceInterfaces;

public interface ICountryService
{
    Task<CountryResponse?> GetByIdAsync(int id, CultureInfo ci);
    Task<Result<CreatedResponse<int>>> CreateAsync(CountryRequest request, CultureInfo ci);
    Task<Result> ModifyAsync(int id, CountryRequest request, CultureInfo ci);
    Task<Result> DeleteAsync(int id, CultureInfo ci);
}