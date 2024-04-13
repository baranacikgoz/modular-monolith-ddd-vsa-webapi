using Common.Core.Contracts;
using Common.Persistence.EventSourcing;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Common.Persistence.Outbox;

public class InsertEventStoreEventsInterceptor : SaveChangesInterceptor
{
    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(
    DbContextEventData eventData,
    InterceptionResult<int> result,
    CancellationToken cancellationToken = default)
    {
        var dbContext = eventData.Context;
        if (dbContext == null)
        {
            return base.SavingChangesAsync(eventData, result, cancellationToken);
        }

        List<EventStoreEvent>? eventsToAdd = null;
        foreach (var entry in dbContext.ChangeTracker.Entries<IAggregateRoot>())
        {
            eventsToAdd ??= [];

            var aggregateRoot = entry.Entity;
            if (aggregateRoot.Events.Count > 0)
            {
                foreach (var @event in aggregateRoot.Events)
                {
                    var eventStoreEvent = EventStoreEvent.Create(aggregateRoot.Id.Value, @event.Version, @event);
                    eventsToAdd.Add(eventStoreEvent);
                }
            }
        }

        if (eventsToAdd?.Count > 0)
        {
            foreach (var eventStoreEvent in eventsToAdd)
            {
                dbContext.Set<EventStoreEvent>().Add(eventStoreEvent);
            }
        }

        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }

}
