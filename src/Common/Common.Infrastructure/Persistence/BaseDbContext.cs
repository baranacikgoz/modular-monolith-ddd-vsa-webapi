using Common.Application.Auth;
using Common.Application.Persistence.Outbox;
using Common.Infrastructure.Persistence.Outbox;
using Common.Domain.Aggregates;
using Common.Domain.Entities;
using Common.Domain.StronglyTypedIds;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Common.Infrastructure.Persistence;

public abstract partial class BaseDbContext(
    DbContextOptions options,
    IServiceScopeFactory serviceScopeFactory
) : Microsoft.EntityFrameworkCore.DbContext(options)
{
    public DbSet<AuditLogEntry> AuditLog => Set<AuditLogEntry>();

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        using var serviceScope = serviceScopeFactory.CreateScope();
        var outboxDbContext = serviceScope.ServiceProvider.GetRequiredService<IOutboxDbContext>();
        var timeProvider = serviceScope.ServiceProvider.GetRequiredService<TimeProvider>();
        var currentUser = serviceScope.ServiceProvider.GetRequiredService<ICurrentUser>();
        var logger = serviceScope.ServiceProvider.GetRequiredService<ILogger<BaseDbContext>>();

        var utcNow = timeProvider.GetUtcNow();
        ApplicationUserId? userId = currentUser.Id.IsEmpty ? null : currentUser.Id;

        List<OutboxMessage>? outboxMessages = null;
        List<AuditLogEntry>? auditLogEntries = null;

        // Collect domain events for BOTH Outbox and AuditLog in a single pass
        foreach (var entry in ChangeTracker.Entries<IAggregateRoot>()
                     .Where(e => e.Entity.Events.Count > 0))
        {
            var aggregateRoot = entry.Entity;

            outboxMessages ??= [];
            auditLogEntries ??= [];

            foreach (var @event in aggregateRoot.Events)
            {
                // Outbox message
                outboxMessages.Add(OutboxMessage.Create(utcNow, @event));

                // Audit log entry
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

        // If no events, run normal SaveChangesAsync.
        if (outboxMessages is null || outboxMessages.Count == 0)
        {
            LogNoDomainEvents(logger);
            return await base.SaveChangesAsync(cancellationToken);
        }

        LogFoundDomainEvents(logger, outboxMessages.Count);

        // Add audit log entries to the change tracker so they are saved in the same transaction
        if (auditLogEntries is { Count: > 0 })
        {
            foreach (var auditLogEntry in auditLogEntries)
            {
                AuditLog.Add(auditLogEntry);
            }
        }

        // Use a single database transaction (shared connection) to guarantee atomicity
        // between module changes, outbox messages, and audit log entries.
        // EF Core manages the connection lifecycle; we share the underlying DbTransaction
        // with the OutboxDbContext so both participate in the same transaction.
        await using var transaction = await Database.BeginTransactionAsync(cancellationToken);
        try
        {
            var result = await base.SaveChangesAsync(cancellationToken);

            // Share the module's connection + transaction with OutboxDbContext
            var dbTransaction = transaction.GetDbTransaction();
            outboxDbContext.Database.SetDbConnection(dbTransaction.Connection);
            await outboxDbContext.Database.UseTransactionAsync(dbTransaction, cancellationToken);
            outboxDbContext.OutboxMessages.AddRange(outboxMessages);
            await outboxDbContext.SaveChangesAsync(cancellationToken);

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

    [LoggerMessage(Level = LogLevel.Debug, Message = "Found {EventCount} domain events. Executing atomic save with Outbox and AuditLog.")]
    private static partial void LogFoundDomainEvents(ILogger logger, int eventCount);

    [LoggerMessage(Level = LogLevel.Error, Message = "Error saving changes to the database.")]
    private static partial void LogSavingError(ILogger logger, Exception ex);
}
