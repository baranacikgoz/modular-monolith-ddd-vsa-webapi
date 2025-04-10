using Common.Application.Auth;
using Common.Domain.Aggregates;
using Common.Domain.Entities;
using Common.Domain.StronglyTypedIds;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Common.Infrastructure.Persistence.EventSourcing;

public class InsertEventStoreEventsInterceptor(TimeProvider timeProvider, ICurrentUser currentUser) : SaveChangesInterceptor
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

        List<EventStoreEvent> eventsToAdd = [];
        var utcNow = timeProvider.GetUtcNow();
        ApplicationUserId? userId = currentUser.Id.IsEmpty ? null : currentUser.Id;
        foreach (var aggregateRoot in dbContext
                                      .ChangeTracker
                                      .Entries<IAggregateRoot>()
                                      .Where(e => e.Entity.Events.Count > 0)
                                      .Select(e => e.Entity))
        {
            foreach (var @event in aggregateRoot.Events)
            {
                @event.CreatedOn = utcNow;
                var eventStoreEvent = EventStoreEvent.Create(aggregateRoot.GetType().Name, aggregateRoot.Id.Value, @event.Version, @event);
                eventStoreEvent.CreatedOn = utcNow;
                eventStoreEvent.CreatedBy = userId;
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
