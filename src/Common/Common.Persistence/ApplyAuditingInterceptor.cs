using Common.Core.Auth;
using Common.Core.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Common.Persistence;
public class ApplyAuditingInterceptor(
    ICurrentUser currentUser
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

        var now = DateTime.UtcNow;
        var userId = currentUser.Id;
        var ipAddress = currentUser.IpAddress ?? "N/A";

        foreach (var entry in dbContext.ChangeTracker.Entries<IAuditableEntity>())
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.Entity.ApplyCreatedAudit(userId, ipAddress, now);
                    break;
                case EntityState.Modified:
                    entry.Entity.ApplyUpdatedAudit(userId, ipAddress, now);
                    break;
            }
        }

        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }
}
