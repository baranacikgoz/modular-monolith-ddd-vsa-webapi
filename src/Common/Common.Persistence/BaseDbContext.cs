using Common.Core.Auth;
using Common.Core.Contracts;
using Common.Eventbus;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Common.Persistence;

public abstract partial class BaseDbContext(
    DbContextOptions options,
    ICurrentUser currentUser,
    IEventBus eventBus,
    ILogger logger
    ) : DbContext(options)
{
    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var now = DateTime.UtcNow;
        var userId = currentUser.Id;
        var ipAddress = currentUser.IpAddress ?? "N/A";

        List<DomainEvent>? domainEvents = null;

        foreach (var entry in ChangeTracker.Entries<IAuditableEntity>())
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.Entity.ApplyCreatedAudit(userId, ipAddress, now);
                    break;
                case EntityState.Modified:
                    entry.Entity.ApplyUpdatedAudit(userId, ipAddress, now);
                    break;
            }

            if (entry.Entity is IAggregateRoot aggregateRoot && aggregateRoot.DomainEvents.Count > 0)
            {
                domainEvents ??= []; // Lazy
                domainEvents.AddRange(aggregateRoot.DomainEvents);
                aggregateRoot.ClearDomainEvents();
            }
        }

        int result;
        try
        {
            result = await base.SaveChangesAsync(cancellationToken);
        }
        catch (DbUpdateConcurrencyException ex)
        {
            if (ex.Entries.Count > 1)
            {
                var typeAndTableNames = ex.Entries.Select(e => MergeTypeAndTableName(
                                                                e.Entity.GetType().Name,
                                                                e.Metadata.GetTableName()));

                LogConcurrencyExceptionOccuredOnMultipleEntities(logger, ex, typeAndTableNames, userId);
                throw;
            }

            var typeAndTableName = MergeTypeAndTableName(
                    ex.Entries.Single().Entity.GetType().Name,
                    ex.Entries.Single().Metadata.GetTableName());

            LogConcurrencyExceptionOccuredOnSingleEntity(logger, ex, typeAndTableName, userId);
            throw;
        }

        if (domainEvents?.Count > 0)
        {
            foreach (var domainEvent in domainEvents)
            {
                await eventBus.PublishAsync(domainEvent, cancellationToken);
            }
        }

        return result;
    }

    private static string MergeTypeAndTableName(string? typeName, string? tableName)
        => $"{typeName ?? "N/A"} / ({tableName ?? "N/A"})";

    [LoggerMessage(
        Level = LogLevel.Information,
        Message = "Concurrency exception occurred on the type/table {TypeAndTableName} for the user {UserId}.")]
    static partial void LogConcurrencyExceptionOccuredOnSingleEntity(
        ILogger logger,
        DbUpdateConcurrencyException exception,
        string TypeAndTableName,
        Guid UserId);

    [LoggerMessage(
        Level = LogLevel.Information,
        Message = "Concurrency exception occurred on the types/tables {@TypeAndTableNames} for the user {UserId}.")]
    static partial void LogConcurrencyExceptionOccuredOnMultipleEntities(
        ILogger logger,
        DbUpdateConcurrencyException exception,
        IEnumerable<string> TypeAndTableNames,
        Guid UserId);
}
