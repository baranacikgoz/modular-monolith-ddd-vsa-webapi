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

            LogSeedingStarted(logger);

            using var scope = serviceScopeFactory.CreateScope();
            var seeders = scope.ServiceProvider
                .GetServices<IDatabaseSeeder>()
                .OrderBy(s => s.Priority)
                .ToList();

            if (seeders.Count == 0)
            {
                LogNoSeeders(logger);
                seedingCompletionTracker.MarkComplete();
                return;
            }

            foreach (var seeder in seeders)
            {
                var seederName = seeder.GetType().Name;
                LogSeederRunning(logger, seederName, seeder.Priority);
                await seeder.SeedAsync(stoppingToken);
                LogSeederCompleted(logger, seederName);
            }

            LogAllSeedersCompleted(logger, seeders.Count);
            seedingCompletionTracker.MarkComplete();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occurred while seeding the database.");
            seedingCompletionTracker.MarkFaulted(ex);
            throw;
        }
    }

    [LoggerMessage(Level = LogLevel.Information,
        Message = "Database seeding orchestrator started.")]
    private static partial void LogSeedingStarted(ILogger logger);

    [LoggerMessage(Level = LogLevel.Information,
        Message = "No IDatabaseSeeder implementations registered. Skipping seeding.")]
    private static partial void LogNoSeeders(ILogger logger);

    [LoggerMessage(Level = LogLevel.Information,
        Message = "Running seeder '{SeederName}' (priority {Priority}).")]
    private static partial void LogSeederRunning(ILogger logger, string seederName, int priority);

    [LoggerMessage(Level = LogLevel.Information,
        Message = "Seeder '{SeederName}' completed.")]
    private static partial void LogSeederCompleted(ILogger logger, string seederName);

    [LoggerMessage(Level = LogLevel.Information,
        Message = "All {Count} seeder(s) completed successfully.")]
    private static partial void LogAllSeedersCompleted(ILogger logger, int count);
}
