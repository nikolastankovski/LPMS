using LPMS.Domain.Primitives;
using LPMS.Infrastructure.Services;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.DependencyInjection;

namespace LPMS.Infrastructure.Interceptors;

public sealed class UpdateAuditableEntitiesInterceptor : SaveChangesInterceptor
{
    private readonly IServiceScopeFactory _serviceScopeFactory;
    public UpdateAuditableEntitiesInterceptor(IServiceScopeFactory serviceScopeFactory)
    {
        _serviceScopeFactory = serviceScopeFactory;
    }

    public override ValueTask<int> SavedChangesAsync(SaveChangesCompletedEventData eventData, int result, CancellationToken cancellationToken = default)
    {
        if (eventData.Context == null)
            return base.SavedChangesAsync(eventData, result, cancellationToken);
        
        using IServiceScope serviceScope = _serviceScopeFactory.CreateScope();
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

        return base.SavedChangesAsync(eventData, result, cancellationToken);
    }
}
