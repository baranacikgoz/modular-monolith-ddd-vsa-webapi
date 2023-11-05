using Common.Core.Auth;
using Common.Core.Contracts;
using Common.Eventbus;
using Microsoft.EntityFrameworkCore;

namespace Common.Persistence;

public class BaseDbContext(
    DbContextOptions options,
    ICurrentUser currentUser, IEventBus eventBus
    ) : DbContext(options)
{
    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var now = DateTime.UtcNow;
        var userId = currentUser.Id;
        var ipAddress = currentUser.IpAddress ?? "N/A";

        List<DomainEvent>? domainEvents = null;

        foreach (var entry in ChangeTracker.Entries<IAuditableEntity>())
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

            if (entry.Entity is IAggregateRoot aggregateRoot && aggregateRoot.DomainEvents.Count > 0)
            {
                domainEvents ??= new List<DomainEvent>(); // Lazy
                domainEvents.AddRange(aggregateRoot.DomainEvents);
                aggregateRoot.ClearDomainEvents();
            }
        }

        // Persist changes to the database
        var result = await base.SaveChangesAsync(cancellationToken);

        if (domainEvents?.Count > 0)
        {
            foreach (var domainEvent in domainEvents)
            {
                await eventBus.PublishAsync(domainEvent, cancellationToken);
            }
        }

        return result;
    }
}
