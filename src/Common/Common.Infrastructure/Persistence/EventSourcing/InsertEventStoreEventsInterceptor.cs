using Common.Domain.Aggregates;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Common.Infrastructure.Persistence.EventSourcing;

public class InsertEventStoreEventsInterceptor(TimeProvider timeProvider) : SaveChangesInterceptor
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
        DateTimeOffset? utcNow = null;
        foreach (var aggregateRoot in dbContext
                                      .ChangeTracker
                                      .Entries<IAggregateRoot>()
                                      .Where(e => e.Entity.Events.Count > 0)
                                      .Select(e => e.Entity))
        {
            eventsToAdd ??= [];
            utcNow ??= timeProvider.GetUtcNow();

            foreach (var @event in aggregateRoot.Events)
            {
                @event.CreatedOn = utcNow.Value;
                var eventStoreEvent = EventStoreEvent.Create(aggregateRoot.Id.Value, @event.Version, @event);
                eventsToAdd.Add(eventStoreEvent);

                // Do not add to DbSet directly here, it throws collection modified exception
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
