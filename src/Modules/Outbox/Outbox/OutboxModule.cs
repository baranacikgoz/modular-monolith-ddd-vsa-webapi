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
        // AddDbContext (not AddDbContextPool): pooled contexts do not support connection swapping,
        // and OutboxDbContext is used only by the processors — not by BaseDbContext anymore.
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
            .AddScoped<IOutboxDbContext>(sp => sp.GetRequiredService<OutboxDbContext>())
            .AddHostedService<OutboxKafkaProcessor>();
    }

    public void UseModule(IApplicationBuilder app)
    {
        var logger = app.ApplicationServices
            .GetRequiredService<ILoggerFactory>()
            .CreateLogger(typeof(OutboxModule).FullName!);

        MigrationGuard.EnsureNoMigrationsPending<OutboxDbContext>(
            app.ApplicationServices, logger, nameof(Outbox));

        var outboxOptions = app.ApplicationServices
            .GetRequiredService<IOptions<OutboxOptions>>()
            .Value;

        var cleanupOptions = outboxOptions.Cleanup;

        if (cleanupOptions.Enabled)
        {
            var recurringJobManager = app.ApplicationServices.GetService<IRecurringJobManagerV2>();
            if (recurringJobManager is not null)
            {
                recurringJobManager.AddOrUpdate(
                    "outbox-cleanup",
                    (OutboxCleanupJob job) => job.ExecuteAsync(CancellationToken.None),
                    () => cleanupOptions.CronSchedule);

                LogCleanupJobRegistered(logger, cleanupOptions.CronSchedule, cleanupOptions.RetentionDays);
            }
            else
            {
                LogCleanupJobSkipped(logger);
            }
        }

        var recurringJobs = app.ApplicationServices.GetService<IRecurringJobManagerV2>();
        if (recurringJobs is not null)
        {
            recurringJobs.AddOrUpdate(
                "outbox-lag",
                (OutboxLagJob job) => job.ExecuteAsync(CancellationToken.None),
                () => outboxOptions.LagCronSchedule);

            LogLagJobRegistered(logger, outboxOptions.LagCronSchedule);
        }
    }

    public void MapEndpoints(IEndpointRouteBuilder endpoints)
    {
    }

    [LoggerMessage(Level = LogLevel.Information,
        Message = "Outbox cleanup recurring job registered with schedule '{CronSchedule}', retention {RetentionDays} days.")]
    private static partial void LogCleanupJobRegistered(ILogger logger, string cronSchedule, int retentionDays);

    [LoggerMessage(Level = LogLevel.Warning,
        Message = "Outbox cleanup enabled but Hangfire is not available. Recurring job not registered.")]
    private static partial void LogCleanupJobSkipped(ILogger logger);

    [LoggerMessage(Level = LogLevel.Information,
        Message = "Outbox lag monitoring recurring job registered with schedule '{CronSchedule}'.")]
    private static partial void LogLagJobRegistered(ILogger logger, string cronSchedule);
}
