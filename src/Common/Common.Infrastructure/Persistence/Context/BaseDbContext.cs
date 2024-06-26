using Common.Application.Auth;
using Common.Domain.StronglyTypedIds;
using Common.Infrastructure.Persistence.EventSourcing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Common.Infrastructure.Persistence;

public abstract partial class BaseDbContext(
    DbContextOptions options,
    ICurrentUser currentUser,
    ILogger logger
    ) : DbContext(options)
{
    public DbSet<EventStoreEvent> EventStoreEvents => Set<EventStoreEvent>();

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var userId = currentUser.Id;

        try
        {
            return await base.SaveChangesAsync(cancellationToken);
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
        ApplicationUserId UserId);

    [LoggerMessage(
        Level = LogLevel.Information,
        Message = "Concurrency exception occurred on the types/tables {@TypeAndTableNames} for the user {UserId}.")]
    static partial void LogConcurrencyExceptionOccuredOnMultipleEntities(
        ILogger logger,
        DbUpdateConcurrencyException exception,
        IEnumerable<string> TypeAndTableNames,
        ApplicationUserId UserId);

    [LoggerMessage(
        Level = LogLevel.Error,
        Message = "An error occurred while saving changes for the user {UserId}.")]
    static partial void LogErrorWhileSavingChanges(
        ILogger logger,
        Exception exception,
        ApplicationUserId UserId);
}
