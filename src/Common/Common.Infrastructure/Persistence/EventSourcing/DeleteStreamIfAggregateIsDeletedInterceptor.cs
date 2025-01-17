using Common.Domain.Aggregates;
using Common.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Common.Infrastructure.Persistence.EventSourcing;

public class DeleteStreamIfAggregateIsDeletedInterceptor : SaveChangesInterceptor
{
    public override async ValueTask<InterceptionResult<int>> SavingChangesAsync(
    DbContextEventData eventData,
    InterceptionResult<int> result,
    CancellationToken cancellationToken = default)
    {
        var dbContext = eventData.Context;
        if (dbContext == null)
        {
            return await base.SavingChangesAsync(eventData, result, cancellationToken);
        }

        List<DefaultIdType>? aggregateIdsToRemove = null;
        foreach (var aggregateRoot in dbContext
                                      .ChangeTracker
                                      .Entries<IAggregateRoot>()
                                      .Where(e => e.State == EntityState.Deleted)
                                      .Select(e => e.Entity))
        {
            aggregateIdsToRemove ??= [];
            aggregateIdsToRemove.Add(aggregateRoot.Id.Value);
        }

        if (aggregateIdsToRemove?.Count > 0)
        {
            using var transaction = await dbContext.Database.BeginTransactionAsync(cancellationToken);
            try
            {
                var saveResult = await base.SavingChangesAsync(eventData, result, cancellationToken);

                await dbContext
                    .Set<EventStoreEvent>()
                    .Where(e => aggregateIdsToRemove.Contains(e.AggregateId))
                    .ExecuteDeleteAsync(cancellationToken);

                await transaction.CommitAsync(cancellationToken);
                return saveResult;
            }
            catch (Exception)
            {
                await transaction.RollbackAsync(cancellationToken);
                throw;
            }
        }

        return await base.SavingChangesAsync(eventData, result, cancellationToken);
    }

}
