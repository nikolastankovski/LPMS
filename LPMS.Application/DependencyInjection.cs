using LPMS.Application.Validators;
using Microsoft.Extensions.DependencyInjection;

namespace LPMS.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddValidatorsFromAssemblyContaining<ReferenceValidator>();

        return services;
    }
}