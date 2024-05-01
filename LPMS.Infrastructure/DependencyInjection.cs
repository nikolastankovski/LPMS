﻿using LPMS.Infrastructure.Repositories;
using LPMS.Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;

namespace LPMS.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        #region REPOSITORIES
        services.AddScoped<IAccountRepository, AccountRepository>();
        services.AddScoped<IIdentityUserRepository, IdentityUserRepository>();
        services.AddScoped<IReferenceRepository, ReferenceRepository>();
        services.AddScoped<IReferenceTypeRepository, ReferenceTypeRepository>();
        #endregion

        #region SERVICES
        services.AddScoped<IAccountService, AccountService>();
        services.AddScoped<ITokenService, TokenService>();
        #endregion


        return services;
    }
}