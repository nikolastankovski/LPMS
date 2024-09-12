using LPMS.Domain.Primitives;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace LPMS.Infrastructure.Interceptors;

public sealed class UpdateAuditableEntitiesInterceptor : SaveChangesInterceptor
{
    private readonly ICommonService _commonService;

    public UpdateAuditableEntitiesInterceptor(ICommonService commonService)
    {
        _commonService = commonService;
    }

    public override ValueTask<int> SavedChangesAsync(SaveChangesCompletedEventData eventData, int result, CancellationToken cancellationToken = default)
    {
        if(eventData.Context == null)
            return base.SavedChangesAsync(eventData, result, cancellationToken);

        var loggedInUser = _commonService.GetLoggedInUser();

        if(loggedInUser is not null)
        {
            var createdEntries = eventData.Context.ChangeTracker.Entries<IAuditableEntity>();

            foreach (var entry in createdEntries)
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Property(x => x.CreatedOn).CurrentValue = DateTime.Now;
                    entry.Property(x => x.CreatedBy).CurrentValue = loggedInUser.AccountId;
                }

                if (entry.State == EntityState.Modified)
                {
                    entry.Property(x => x.ModifiedOn).CurrentValue = DateTime.Now;
                    entry.Property(x => x.ModifiedBy).CurrentValue = loggedInUser.AccountId;
                }
            }
        }

        return base.SavedChangesAsync(eventData, result, cancellationToken);
    }
}
