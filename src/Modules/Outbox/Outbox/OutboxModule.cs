using Common.Application.Options;
using Common.Infrastructure.Modules;
using Common.Infrastructure.Persistence;
using Common.Infrastructure.Persistence.Outbox;
using EntityFramework.Exceptions.PostgreSQL;
using Hangfire;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Outbox.Persistence;

namespace Outbox;

public sealed partial class OutboxModule : ICoreModule
{
    public string Name => "Outbox";
    public int StartupPriority => 1;

    public IEnumerable<string> ActivitySourceNames => [Telemetry.OutboxTelemetry.ActivitySourceName];

    public IEnumerable<string> MeterNames => [Telemetry.OutboxTelemetry.MeterName];

    public void AddServices(IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddDbContext<OutboxDbContext>((sp, options) =>
            {
                var connectionString = sp.GetRequiredService<IOptions<DatabaseOptions>>().Value.ConnectionString;

                options
                    .UseNpgsql(
                        connectionString,
                        o => o.MigrationsHistoryTable(HistoryRepository.DefaultTableName, nameof(Outbox)))
                    .UseExceptionProcessor();
            })
            .AddScoped<IOutboxDbContext>(sp => sp.GetRequiredService<OutboxDbContext>());

        var isProcessor = configuration
            .GetSection(nameof(OutboxOptions))
            .Get<OutboxOptions>()?
            .IsProcessor ?? throw new InvalidOperationException("OutboxOptions is not configured.");

        if (isProcessor)
        {
            services.AddHostedService<OutboxProcessor>();
        }
    }

    public void UseModule(IApplicationBuilder app)
    {
        var services = app.ApplicationServices;
        var logger = services.GetRequiredService<ILoggerFactory>()
            .CreateLogger(typeof(OutboxModule).FullName!);

        MigrationGuard.EnsureNoMigrationsPending<OutboxDbContext>(services, logger, nameof(Outbox));

        var outboxOptions = services.GetRequiredService<IOptions<OutboxOptions>>().Value;

        // Defer registration until after all hosted services start. RecurringJobScheduler runs
        // immediately after Hangfire's hosted service starts and briefly holds the per-job lock.
        // We retry with exponential backoff so we always land in the gap between scheduler runs.
        services.GetRequiredService<IHostApplicationLifetime>().ApplicationStarted.Register(() =>
        {
            var recurringJobManager = services.GetService<IRecurringJobManagerV2>();
            if (recurringJobManager is null)
            {
                LogCleanupJobSkipped(logger);
                return;
            }

            var cleanupOptions = outboxOptions.Cleanup;
            if (cleanupOptions.Enabled)
            {
                RegisterWithRetry(logger, "outbox-cleanup", () =>
                {
                    recurringJobManager.AddOrUpdate(
                        "outbox-cleanup",
                        (OutboxCleanupJob job) => job.ExecuteAsync(CancellationToken.None),
                        () => cleanupOptions.CronSchedule);

                    LogCleanupJobRegistered(logger, cleanupOptions.CronSchedule, cleanupOptions.RetentionDays);
                });
            }

            recurringJobManager.RemoveIfExists("outbox-lag");

            RegisterWithRetry(logger, "outbox-metrics", () =>
            {
                recurringJobManager.AddOrUpdate(
                    "outbox-metrics",
                    (OutboxMetricsJob job) => job.ExecuteAsync(CancellationToken.None),
                    () => outboxOptions.MetricsCronSchedule);

                LogMetricsJobRegistered(logger, outboxOptions.MetricsCronSchedule);
            });
        });
    }

    public void MapEndpoints(IEndpointRouteBuilder endpoints)
    {
    }

    private static void RegisterWithRetry(ILogger logger, string jobId, Action register)
    {
        const int maxAttempts = 10;
        var delayMs = 200;

        for (var attempt = 1; attempt < maxAttempts; attempt++)
        {
            try
            {
                register();
                return;
            }
            catch (Exception ex) when (IsLockContention(ex))
            {
                LogRecurringJobLockRetry(logger, jobId, attempt, maxAttempts, ex);
                Thread.Sleep(delayMs);
                delayMs = Math.Min(delayMs * 2, 5_000);
            }
        }

        register(); // final attempt — propagates on failure
    }

    private static bool IsLockContention(Exception ex) =>
        ex.GetType().Name.Contains("DistributedLock", StringComparison.OrdinalIgnoreCase)
        || ex.Message.Contains("distributed lock", StringComparison.OrdinalIgnoreCase);

    [LoggerMessage(Level = LogLevel.Information,
        Message = "Outbox cleanup recurring job registered with schedule '{CronSchedule}', retention {RetentionDays} days.")]
    private static partial void LogCleanupJobRegistered(ILogger logger, string cronSchedule, int retentionDays);

    [LoggerMessage(Level = LogLevel.Warning,
        Message = "Outbox cleanup enabled but Hangfire is not available. Recurring job not registered.")]
    private static partial void LogCleanupJobSkipped(ILogger logger);

    [LoggerMessage(Level = LogLevel.Information,
        Message = "Outbox metrics monitoring recurring job registered with schedule '{CronSchedule}'.")]
    private static partial void LogMetricsJobRegistered(ILogger logger, string cronSchedule);

    [LoggerMessage(Level = LogLevel.Warning,
        Message = "Recurring job '{JobId}' lock contention on attempt {Attempt}/{MaxAttempts}; retrying.")]
    private static partial void LogRecurringJobLockRetry(ILogger logger, string jobId, int attempt, int maxAttempts, Exception ex);
}
