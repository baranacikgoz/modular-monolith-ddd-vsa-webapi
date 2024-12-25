using Common.Application.Auth;
using Common.Domain.StronglyTypedIds;
using Common.Infrastructure.Options;
using Common.Infrastructure.Persistence.EventSourcing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Common.Infrastructure.Persistence;

public abstract partial class BaseDbContext(
    DbContextOptions options,
    ICurrentUser currentUser,
    ILogger logger,
    IOptions<ObservabilityOptions> observabilityOptionsProvider
    ) : DbContext(options)
{
    public DbSet<EventStoreEvent> EventStoreEvents => Set<EventStoreEvent>();

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);

        if (observabilityOptionsProvider.Value.LogGeneratedSqlQueries)
        {
#pragma warning disable
            optionsBuilder.LogTo(
            sql => logger.LogDebug(sql),                  // Log the SQL query
            new[] { DbLoggerCategory.Database.Command.Name }, // Only log database commands
            LogLevel.Information                           // Set the log level
            );
#pragma warning restore
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
