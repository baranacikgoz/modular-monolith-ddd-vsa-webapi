using Common.Application.Auth;
using Common.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Common.Infrastructure.Persistence.Context;
public class ApplyAuditingInterceptor(
    ICurrentUser currentUser,
    TimeProvider timeProvider
    ) : SaveChangesInterceptor
{
    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(
        DbContextEventData eventData,
        InterceptionResult<int> result,
        CancellationToken cancellationToken = default)
    {
        var dbContext = eventData.Context;
        if (dbContext is null)
        {
            return base.SavingChangesAsync(eventData, result, cancellationToken);
        }

        var now = timeProvider.GetUtcNow();
        var userId = currentUser.Id;
        var ipAddress = currentUser.IpAddress ?? "N/A";

        foreach (var entry in dbContext.ChangeTracker.Entries<IAuditableEntity>())
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.Entity.CreatedBy = userId;
                    entry.Entity.LastModifiedBy = userId;
                    entry.Entity.LastModifiedOn = now;
                    entry.Entity.LastModifiedIp = ipAddress;
                    break;
                case EntityState.Modified:
                    entry.Entity.LastModifiedBy = userId;
                    entry.Entity.LastModifiedOn = now;
                    entry.Entity.LastModifiedIp = ipAddress;
                    break;
                case EntityState.Detached:
                    break;
                case EntityState.Unchanged:
                    break;
                case EntityState.Deleted:
                    break;
                default:
                    break;
            }
        }

        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }
}
