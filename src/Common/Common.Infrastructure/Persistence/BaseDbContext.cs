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

public abstract partial class BaseDbContext(
    DbContextOptions options,
    TimeProvider timeProvider,
    ICurrentUser currentUser,
    ILogger<BaseDbContext> logger
) : Microsoft.EntityFrameworkCore.DbContext(options)
{
    public DbSet<AuditLogEntry> AuditLog => Set<AuditLogEntry>();

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        // Fast path: scan for events first — zero extra allocation when none exist.
        var aggregatesWithEvents = ChangeTracker
            .Entries<IAggregateRoot>()
            .Where(e => e.Entity.Events.Count > 0)
            .ToList();

        if (aggregatesWithEvents.Count == 0)
        {
            LogNoDomainEvents(logger);
            return await base.SaveChangesAsync(cancellationToken);
        }

        // Slow path: services already injected, no scope creation needed.
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
            AuditLog.Add(entry);
        }

        LogFoundDomainEvents(logger, outboxMessages.Count);

        // Single transaction on the module's own connection — no second DbContext needed.
        await using var transaction = await Database.BeginTransactionAsync(cancellationToken);
        try
        {
            var result = await base.SaveChangesAsync(cancellationToken);

            var createdOns = new DateTimeOffset[outboxMessages.Count];
            var events = new string[outboxMessages.Count];
            for (var i = 0; i < outboxMessages.Count; i++)
            {
                createdOns[i] = outboxMessages[i].CreatedOn;
                events[i] = JsonSerializer.Serialize(outboxMessages[i].Event, DomainEventConverter.WriteOptions);
            }

            const string InsertOutboxMessagesSql =
                """
                INSERT INTO "Outbox"."OutboxMessages" ("CreatedOn", "Event", "IsProcessed")
                SELECT c, e, false
                FROM UNNEST({0}::timestamptz[], {1}::text[]) AS t(c, e)
                """;
#pragma warning disable S3265 // NpgsqlDbType intentionally uses bitwise-OR for array types despite missing [Flags]
            await Database.ExecuteSqlRawAsync(
                InsertOutboxMessagesSql,
                new object[]
                {
                    new NpgsqlParameter
                    {
                        NpgsqlDbType = NpgsqlDbType.Array | NpgsqlDbType.TimestampTz, Value = createdOns
                    },
                    new NpgsqlParameter { NpgsqlDbType = NpgsqlDbType.Array | NpgsqlDbType.Text, Value = events }
                },
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
