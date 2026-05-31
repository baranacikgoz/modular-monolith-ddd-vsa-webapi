using System.Diagnostics;
using System.Text.Json;
using Common.Application.Auth;
using Common.Domain.Aggregates;
using Common.Domain.Entities;
using Common.Domain.Events;
using Common.Domain.StronglyTypedIds;
using Common.Infrastructure.EventBus;
using Common.Infrastructure.Persistence.ValueConverters;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Npgsql;
using NpgsqlTypes;

namespace Common.Infrastructure.Persistence;

public static partial class OutboxSaveHelper
{
    public static async Task<int> SaveWithOutboxAsync(
        Microsoft.EntityFrameworkCore.DbContext context,
        TimeProvider timeProvider,
        ICurrentUser currentUser,
        ILogger logger,
        EventDispatcher eventDispatcher,
        IntegrationEventOutbox integrationEventOutbox,
        Func<CancellationToken, Task<int>> baseSaveAsync,
        CancellationToken cancellationToken)
    {
        var aggregatesWithEvents = context
            .ChangeTracker
            .Entries<IAggregateRoot>()
            .Where(e => e.Entity.Events.Count > 0)
            .ToList();

        if (aggregatesWithEvents.Count == 0)
        {
            LogNoDomainEvents(logger);
            return await baseSaveAsync(cancellationToken);
        }

        var utcNow = timeProvider.GetUtcNow();
        ApplicationUserId? userId = currentUser.Id.IsEmpty ? null : currentUser.Id;

        var auditLogEntries = new List<AuditLogEntry>(aggregatesWithEvents.Count);
        var domainEvents = new List<DomainEvent>();

        foreach (var entry in aggregatesWithEvents)
        {
            var aggregateRoot = entry.Entity;
            foreach (var @event in aggregateRoot.Events)
            {
                if (@event is not DomainEvent domainEvent)
                {
                    continue;
                }

                domainEvent.CreatedOn = utcNow;
                var auditLogEntry = AuditLogEntry.Create(
                    aggregateRoot.GetType().Name,
                    aggregateRoot.Id.Value,
                    @event.Version,
                    @event);
                auditLogEntry.CreatedOn = utcNow;
                auditLogEntry.CreatedBy = userId;
                auditLogEntries.Add(auditLogEntry);
                domainEvents.Add(domainEvent);
            }

            aggregateRoot.ClearEvents();
        }

        foreach (var entry in auditLogEntries)
        {
            context.Set<AuditLogEntry>().Add(entry);
        }

        LogFoundDomainEvents(logger, domainEvents.Count);

        // Capture trace context BEFORE dispatch (Activity.Current reflects the request span here)
        var traceId = Activity.Current?.TraceId.ToHexString();
        var parentSpanId = Activity.Current?.SpanId.ToHexString();

        // Dispatch domain events in-process BEFORE opening the transaction.
        // Handlers collect integration events into integrationEventOutbox (in-memory).
        // If dispatch throws, nothing is saved — exception propagates before transaction opens.
        foreach (var domainEvent in domainEvents)
        {
            await eventDispatcher.DispatchAsync(domainEvent, cancellationToken);
        }

        var integrationEvents = integrationEventOutbox.Drain();

        await using var transaction = await context.Database.BeginTransactionAsync(cancellationToken);
        try
        {
            var result = await baseSaveAsync(cancellationToken);

            if (integrationEvents.Count > 0)
            {
                var createdOns = new DateTimeOffset[integrationEvents.Count];
                var events = new string[integrationEvents.Count];
                var traceIds = new string?[integrationEvents.Count];
                var parentSpanIds = new string?[integrationEvents.Count];

                for (var i = 0; i < integrationEvents.Count; i++)
                {
                    createdOns[i] = utcNow;
                    events[i] = JsonSerializer.Serialize(integrationEvents[i], IntegrationEventConverter.WriteOptions);
                    traceIds[i] = traceId;
                    parentSpanIds[i] = parentSpanId;
                }

                const string InsertOutboxMessagesSql =
                    """
                    INSERT INTO "Outbox"."OutboxMessages" ("CreatedOn", "Event", "IsProcessed", "TraceId", "ParentSpanId")
                    SELECT c, e, false, ti, psi
                    FROM UNNEST({0}::timestamptz[], {1}::text[], {2}::text[], {3}::text[]) AS t(c, e, ti, psi)
                    """;
#pragma warning disable S3265 // NpgsqlDbType intentionally uses bitwise-OR for array types despite missing [Flags]
                await context.Database.ExecuteSqlRawAsync(
                    InsertOutboxMessagesSql,
                    [
                        new NpgsqlParameter
                        {
                            NpgsqlDbType = NpgsqlDbType.Array | NpgsqlDbType.TimestampTz, Value = createdOns
                        },
                        new NpgsqlParameter { NpgsqlDbType = NpgsqlDbType.Array | NpgsqlDbType.Text, Value = events },
                        new NpgsqlParameter { NpgsqlDbType = NpgsqlDbType.Array | NpgsqlDbType.Text, Value = traceIds },
                        new NpgsqlParameter
                        {
                            NpgsqlDbType = NpgsqlDbType.Array | NpgsqlDbType.Text, Value = parentSpanIds
                        }
                    ],
                    cancellationToken);
#pragma warning restore S3265
            }

            await transaction.CommitAsync(cancellationToken);
            return result;
        }
        catch (Exception ex)
        {
            // Use CancellationToken.None — cancelled request must still rollback.
            // Wrap so a broken/already-aborted transaction doesn't mask the original error.
            try
            {
                await transaction.RollbackAsync(CancellationToken.None);
            }
            catch (Exception rollbackEx)
            {
                LogRollbackError(logger, rollbackEx);
            }
            LogSavingError(logger, ex);
            throw;
        }
    }

    [LoggerMessage(Level = LogLevel.Debug, Message = "No domain events found. Calling base SaveChangesAsync.")]
    private static partial void LogNoDomainEvents(ILogger logger);

    [LoggerMessage(Level = LogLevel.Debug,
        Message = "Found {EventCount} domain events. Executing atomic save with Outbox and AuditLog.")]
    private static partial void LogFoundDomainEvents(ILogger logger, int eventCount);

    [LoggerMessage(Level = LogLevel.Warning, Message = "Error rolling back transaction after save failure.")]
    private static partial void LogRollbackError(ILogger logger, Exception ex);

    [LoggerMessage(Level = LogLevel.Error, Message = "Error saving changes to the database.")]
    private static partial void LogSavingError(ILogger logger, Exception ex);
}
