using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Common.Core.Contracts;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Logging;

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
                                      .Where(e => e.Entity.HasAnyEvent)
                                      .Select(e => e.Entity))
        {
            outboxMessages ??= []; // Lazy

            while (aggregateRoot.TryDequeueEvent(out var @event))
            {
                if (@event is null)
                {
                    continue;
                }

                var type = @event.GetType().AssemblyQualifiedName!;
                var payload = JsonSerializer.Serialize(@event, @event.GetType());

                outboxMessages.Add(OutboxMessage.Create(
                    type: type,
                    payload: payload));

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
