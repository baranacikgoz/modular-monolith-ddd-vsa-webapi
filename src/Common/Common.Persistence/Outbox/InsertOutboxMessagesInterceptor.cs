using Common.Core.Contracts;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Common.Persistence.Outbox;

public class InsertOutboxMessagesInterceptor(
    OutboxDbContext outboxDbContext
    ) : SaveChangesInterceptor
{
    public override async ValueTask<InterceptionResult<int>> SavingChangesAsync(
        DbContextEventData eventData,
        InterceptionResult<int> result,
        CancellationToken cancellationToken = default)
    {
        var dbContext = eventData.Context;
        if (dbContext is null)
        {
            return await base.SavingChangesAsync(eventData, result, cancellationToken);
        }

        List<OutboxMessage>? outboxMessages = null;

        foreach (var aggregateRoot in dbContext
                                      .ChangeTracker
                                      .Entries<IAggregateRoot>()
                                      .Where(e => e.Entity.Events.Count > 0)
                                      .Select(e => e.Entity))
        {
            outboxMessages ??= []; // Lazy

            foreach (var @event in aggregateRoot.Events)
            {
                outboxMessages.Add(OutboxMessage.Create(@event));
            }
        }

        if (outboxMessages is null || outboxMessages.Count == 0)
        {
            return await base.SavingChangesAsync(eventData, result, cancellationToken);
        }

        // Warning: This is a simplified, SINGLE DATABASE + Multiple DbContexts only implementation.
        // If you use multiple databases, you need to implement a distributed transaction, change this implementation accordingly.
        using var transaction = await outboxDbContext.Database.BeginTransactionAsync(cancellationToken);
        try
        {
            var saveResult = await base.SavingChangesAsync(eventData, result, cancellationToken);

            await outboxDbContext.OutboxMessages.AddRangeAsync(outboxMessages, cancellationToken);
            await outboxDbContext.SaveChangesAsync(cancellationToken);

            await transaction.CommitAsync(cancellationToken);

            return saveResult;
        }
        catch (Exception)
        {
            await transaction.RollbackAsync(cancellationToken);
            throw;
        }
    }
}
