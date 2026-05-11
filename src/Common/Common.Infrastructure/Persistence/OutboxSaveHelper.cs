using System.Text.Json;
using Common.Application.Auth;
using Common.Application.Persistence.Outbox;
using Common.Domain.Aggregates;
using Common.Domain.Entities;
using Common.Domain.StronglyTypedIds;
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

        var outboxMessages = new List<OutboxMessage>(aggregatesWithEvents.Count);
        var auditLogEntries = new List<AuditLogEntry>(aggregatesWithEvents.Count);

        foreach (var entry in aggregatesWithEvents)
        {
            var aggregateRoot = entry.Entity;
            foreach (var @event in aggregateRoot.Events)
            {
                outboxMessages.Add(OutboxMessage.Create(utcNow, @event));

                @event.CreatedOn = utcNow;
                var auditLogEntry = AuditLogEntry.Create(
                    aggregateRoot.GetType().Name,
                    aggregateRoot.Id.Value,
                    @event.Version,
                    @event);
                auditLogEntry.CreatedOn = utcNow;
                auditLogEntry.CreatedBy = userId;
                auditLogEntries.Add(auditLogEntry);
            }

            aggregateRoot.ClearEvents();
        }

        foreach (var entry in auditLogEntries)
        {
            context.Set<AuditLogEntry>().Add(entry);
        }

        LogFoundDomainEvents(logger, outboxMessages.Count);

        await using var transaction = await context.Database.BeginTransactionAsync(cancellationToken);
        try
        {
            var result = await baseSaveAsync(cancellationToken);

            var createdOns = new DateTimeOffset[outboxMessages.Count];
            var events = new string[outboxMessages.Count];
            var eventTypes = new string[outboxMessages.Count];
            for (var i = 0; i < outboxMessages.Count; i++)
            {
                createdOns[i] = outboxMessages[i].CreatedOn;
                events[i] = JsonSerializer.Serialize(outboxMessages[i].Event, EventConverter.WriteOptions);
                eventTypes[i] = OutboxMessage.EventTypeDomain;
            }

            const string InsertOutboxMessagesSql =
                """
                INSERT INTO "Outbox"."OutboxMessages" ("CreatedOn", "Event", "IsProcessed", "EventType")
                SELECT c, e, false, et
                FROM UNNEST({0}::timestamptz[], {1}::text[], {2}::text[]) AS t(c, e, et)
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
                    new NpgsqlParameter { NpgsqlDbType = NpgsqlDbType.Array | NpgsqlDbType.Text, Value = eventTypes }
                ],
                cancellationToken);
#pragma warning restore S3265

            await transaction.CommitAsync(cancellationToken);
            return result;
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync(cancellationToken);
            LogSavingError(logger, ex);
            throw;
        }
    }

    [LoggerMessage(Level = LogLevel.Debug, Message = "No domain events found. Calling base SaveChangesAsync.")]
    private static partial void LogNoDomainEvents(ILogger logger);

    [LoggerMessage(Level = LogLevel.Debug,
        Message = "Found {EventCount} domain events. Executing atomic save with Outbox and AuditLog.")]
    private static partial void LogFoundDomainEvents(ILogger logger, int eventCount);

    [LoggerMessage(Level = LogLevel.Error, Message = "Error saving changes to the database.")]
    private static partial void LogSavingError(ILogger logger, Exception ex);
}
