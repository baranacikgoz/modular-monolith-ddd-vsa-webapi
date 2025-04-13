using System.Transactions;
using Common.Application.Persistence.Outbox;
using Common.Domain.Aggregates;
using Common.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Common.Application.Persistence;

public abstract partial class BaseDbContext(
    DbContextOptions options,
    IServiceScopeFactory serviceScopeFactory
) : DbContext(options)
{
    public DbSet<EventStoreEvent> EventStoreEvents => Set<EventStoreEvent>();

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        using var serviceScope = serviceScopeFactory.CreateScope();
        var outboxDbContext = serviceScope.ServiceProvider.GetRequiredService<IOutboxDbContext>();
        var timeProvider = serviceScope.ServiceProvider.GetRequiredService<TimeProvider>();
        var logger = serviceScope.ServiceProvider.GetRequiredService<ILogger<BaseDbContext>>();

        List<OutboxMessage>? outboxMessages = null;
        var utcNow = timeProvider.GetUtcNow();

        foreach (var entry in ChangeTracker.Entries<IAggregateRoot>()
                     .Where(e => e.Entity.Events.Count > 0))
        {
            var aggregateRoot = entry.Entity;
            // Collect domain events into outbox messages.
            outboxMessages ??= new List<OutboxMessage>();
            foreach (var @event in aggregateRoot.Events)
            {
                outboxMessages.Add(OutboxMessage.Create(utcNow, @event));
            }

            aggregateRoot.ClearEvents();
        }

        // If no events, run normal SaveChangesAsync.
        if (outboxMessages is null || outboxMessages.Count == 0)
        {
            logger.LogDebug("No domain events found. Calling base SaveChangesAsync.");
            return await base.SaveChangesAsync(cancellationToken);
        }

        logger.LogDebug(
            "Found {EventCount} domain events. Executing primary save and Outbox insertion within TransactionScope.",
            outboxMessages.Count);

        using var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
        try
        {
            var result = await base.SaveChangesAsync(cancellationToken);

            outboxDbContext.OutboxMessages.AddRange(outboxMessages);
            await outboxDbContext.SaveChangesAsync(cancellationToken);

            scope.Complete();

            return result;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error saving changes to the database.");
            throw;
        }
    }
}
