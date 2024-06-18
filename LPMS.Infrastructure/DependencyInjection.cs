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
        services.AddScoped<IAccountxDepartmentxDivisionRepository, AccountxDepartmentxDivisionRepository>();
        services.AddScoped<ISystemUserRepository, SystemUserRepository>();
        services.AddScoped<IReferenceRepository, ReferenceRepository>();
        services.AddScoped<IReferenceTypeRepository, ReferenceTypeRepository>();
        #endregion

        #region SERVICES
        services.AddScoped<IAccountService, AccountService>();
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<TestService>();
        #endregion

        return services;
    }
}