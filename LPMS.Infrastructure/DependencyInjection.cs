using LPMS.Infrastructure.Repositories;
using LPMS.Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;

namespace LPMS.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        #region REPOSITORIES
        services.AddScoped<IAccountRepository, AccountRepository>();
        services.AddScoped<ISystemUserRepository, SystemUserRepository>();
        services.AddScoped<IReferenceRepository, ReferenceRepository>();
        services.AddScoped<IReferenceTypeRepository, ReferenceTypeRepository>();
        services.AddScoped<IEmailHistoryRepository, EmailHistoryRepository>();
        services.AddScoped<ICountryRepository, CountryRepository>();
        services.AddScoped<ICityRepository, CityRepository>();
        #endregion

        #region SERVICES
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IReferenceService, ReferenceService>();
        services.AddScoped<ICommonService, CommonService>();
        services.AddScoped<ICountryService, CountryService>();
        services.AddScoped<ICityService, CityService>();
        services.AddScoped<TestService>();
        #endregion

        return services;
    }
}