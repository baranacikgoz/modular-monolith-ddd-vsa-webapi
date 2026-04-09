using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using EfDbContext = Microsoft.EntityFrameworkCore.DbContext;
namespace Common.Infrastructure.Persistence;

public static partial class MigrationGuard
{
    /// <summary>
    ///     Checks for pending migrations.
    ///     In production: throws <see cref="InvalidOperationException"/> if any are pending.
    ///     In test environments: auto-applies pending migrations (when <see cref="IAutoMigrateMarker"/> is registered).
    /// </summary>
    public static void EnsureNoMigrationsPending<TContext>(
        IServiceProvider serviceProvider,
        ILogger logger,
        string moduleName)
        where TContext : EfDbContext
    {
        using var scope = serviceProvider.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<TContext>();

        var pending = db.Database.GetPendingMigrations().ToList();

        if (pending.Count == 0)
        {
            LogMigrationsUpToDate(logger, moduleName);
            return;
        }

        // If IAutoMigrateMarker is registered, we are in a test environment.
        var isTestEnvironment = scope.ServiceProvider.GetService<IAutoMigrateMarker>() is not null;

        if (isTestEnvironment)
        {
            LogAutoMigrating(logger, moduleName, pending.Count);
            db.Database.Migrate();
            return;
        }

        var migrationList = string.Join(", ", pending);
        LogPendingMigrations(logger, moduleName, pending.Count, migrationList);

        throw new InvalidOperationException(
            $"Module '{moduleName}' has {pending.Count} pending migration(s): [{migrationList}]. " +
            $"Generate the SQL script with 'make ef-script-{moduleName}' and have a DBA apply it before deploying.");
    }

    [LoggerMessage(Level = LogLevel.Information,
        Message = "Module '{ModuleName}': all migrations applied.")]
    private static partial void LogMigrationsUpToDate(ILogger logger, string moduleName);

    [LoggerMessage(Level = LogLevel.Information,
        Message = "Module '{ModuleName}': test environment detected — auto-applying {Count} pending migration(s).")]
    private static partial void LogAutoMigrating(ILogger logger, string moduleName, int count);

    [LoggerMessage(Level = LogLevel.Critical,
        Message = "Module '{ModuleName}': {Count} pending migration(s) detected: [{Migrations}]. " +
                  "Application cannot start until a DBA applies the missing migrations.")]
    private static partial void LogPendingMigrations(
        ILogger logger, string moduleName, int count, string migrations);
}
