using LPMS.Infrastructure.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace LPMS.Application.ExtensionMethods
{
    public static class MigrationExtensions
    {
        public static async Task ApplyMigrationsAsync(this IApplicationBuilder app)
        {
            using (IServiceScope scope = app.ApplicationServices.CreateScope())
            {
                using (var lpmsDbContext = scope.ServiceProvider.GetService<LPMSDbContext>())
                {
                    await lpmsDbContext.Database.MigrateAsync();
                }

                using (var systemUserDbContext = scope.ServiceProvider.GetService<SystemUserDbContext>())
                {
                    await systemUserDbContext.Database.MigrateAsync();
                }
            }
        }
    }
}
