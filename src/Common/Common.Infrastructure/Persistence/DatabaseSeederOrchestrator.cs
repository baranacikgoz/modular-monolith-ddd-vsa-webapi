using Common.Application.Persistence;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Common.Infrastructure.Persistence;

/// <summary>
///     Runs all registered <see cref="IDatabaseSeeder"/> implementations in <see cref="IDatabaseSeeder.Priority"/> order
///     after the application has fully started (guaranteeing MassTransit bus and all hosted services are up).
/// </summary>
internal sealed partial class DatabaseSeederOrchestrator(
    IServiceScopeFactory serviceScopeFactory,
    IHostApplicationLifetime hostLifetime,
    SeedingCompletionTracker seedingCompletionTracker,
    ILogger<DatabaseSeederOrchestrator> logger
) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        try
        {
            // Wait until all hosted services (including MassTransit) have started.
            var appStarted = new TaskCompletionSource(TaskCreationOptions.RunContinuationsAsynchronously);
            hostLifetime.ApplicationStarted.Register(() => appStarted.TrySetResult());

            await appStarted.Task.WaitAsync(stoppingToken);

            LoggerMessages.LogSeedingStarted(logger);

            using var scope = serviceScopeFactory.CreateScope();
            var seeders = scope.ServiceProvider
                .GetServices<IDatabaseSeeder>()
                .OrderBy(s => s.Priority)
                .ToList();

            if (seeders.Count == 0)
            {
                LoggerMessages.LogNoSeeders(logger);
                seedingCompletionTracker.MarkComplete();
                return;
            }

            foreach (var seeder in seeders)
            {
                var seederName = seeder.GetType().Name;
                LoggerMessages.LogSeederRunning(logger, seederName, seeder.Priority);
                await seeder.SeedAsync(stoppingToken);
                LoggerMessages.LogSeederCompleted(logger, seederName);
            }

            LoggerMessages.LogAllSeedersCompleted(logger, seeders.Count);
            seedingCompletionTracker.MarkComplete();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occurred while seeding the database.");
            seedingCompletionTracker.MarkFaulted(ex);
            throw;
        }
    }

    private static partial class LoggerMessages
    {
        [LoggerMessage(Level = LogLevel.Information,
            Message = "Database seeding orchestrator started.")]
        public static partial void LogSeedingStarted(ILogger logger);

        [LoggerMessage(Level = LogLevel.Information,
            Message = "No IDatabaseSeeder implementations registered. Skipping seeding.")]
        public static partial void LogNoSeeders(ILogger logger);

        [LoggerMessage(Level = LogLevel.Information,
            Message = "Running seeder '{SeederName}' (priority {Priority}).")]
        public static partial void LogSeederRunning(ILogger logger, string seederName, int priority);

        [LoggerMessage(Level = LogLevel.Information,
            Message = "Seeder '{SeederName}' completed.")]
        public static partial void LogSeederCompleted(ILogger logger, string seederName);

        [LoggerMessage(Level = LogLevel.Information,
            Message = "All {Count} seeder(s) completed successfully.")]
        public static partial void LogAllSeedersCompleted(ILogger logger, int count);
    }
}
