using LPMS.Domain.Primitives;
using LPMS.Infrastructure.Services;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.DependencyInjection;

namespace LPMS.Infrastructure.Interceptors;

public sealed class UpdateAuditableEntitiesInterceptor(IServiceScopeFactory serviceScopeFactory)
    : SaveChangesInterceptor
{
    public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
    {
        if (eventData?.Context == null)
            return base.SavingChanges(eventData, result);
        
        eventData = UpdateEventData(eventData);

        return base.SavingChanges(eventData, result);
    }

    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result,
        CancellationToken cancellationToken = new CancellationToken())
    {
        if (eventData?.Context == null)
            return base.SavingChangesAsync(eventData, result, cancellationToken);

        eventData = UpdateEventData(eventData);

        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }

    private DbContextEventData UpdateEventData(DbContextEventData eventData)
    {
        using IServiceScope serviceScope = serviceScopeFactory.CreateScope();
        var commonService = serviceScope.ServiceProvider.GetService<ICommonService>();

        var loggedInUser = commonService.GetLoggedInUser();

        if (loggedInUser is not null)
        {
            var modifiedEntries = eventData.Context.ChangeTracker.Entries<IAuditableEntity>();

            foreach (var entry in modifiedEntries)
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Property(x => x.CreatedOnUTC).CurrentValue = DateTime.UtcNow;
                    entry.Property(x => x.CreatedBy).CurrentValue = loggedInUser.AccountId;
                }
                
                if (entry.State == EntityState.Unchanged || entry.State == EntityState.Modified)
                {
                    entry.Property(x => x.ModifiedOnUTC).CurrentValue = DateTime.UtcNow;
                    entry.Property(x => x.ModifiedBy).CurrentValue = loggedInUser.AccountId;
                }
            }
        }

        return eventData;
    }
}