using Common.Application.BackgroundJobs;
using Common.Application.Options;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Common.Infrastructure.Persistence.AuditLog;

/// <summary>
/// Registers the audit log retention purge as a recurring background job on the
/// <see cref="AuditLogOptions.RetentionCron" /> schedule. Gracefully skips registration
/// if the BackgroundJobs module is not loaded.
/// </summary>
public sealed partial class AuditLogRetentionJobRegistrar(
    IServiceProvider serviceProvider,
    IOptions<AuditLogOptions> options,
    ILogger<AuditLogRetentionJobRegistrar> logger) : IHostedService
{
    private const string JobId = "audit-log-retention-purge";

    public Task StartAsync(CancellationToken cancellationToken)
    {
        var recurringJobs = serviceProvider.GetService<IRecurringBackgroundJobs>();
        if (recurringJobs is null)
        {
            LogBackgroundJobsUnavailable(logger);
            return Task.CompletedTask;
        }

        var cron = options.Value.RetentionCron;
        recurringJobs.AddOrUpdate<AuditLogRetentionService>(
            JobId,
            service => service.PurgeExpiredEntriesAsync(CancellationToken.None),
            () => cron);

        LogJobRegistered(logger, JobId, cron);
        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;

    [LoggerMessage(Level = LogLevel.Warning,
        Message = "IRecurringBackgroundJobs is not available. Audit log retention job will not be registered. Ensure the BackgroundJobs module is loaded.")]
    private static partial void LogBackgroundJobsUnavailable(ILogger logger);

    [LoggerMessage(Level = LogLevel.Information,
        Message = "Registered recurring audit log retention job '{JobId}' with cron '{Cron}'.")]
    private static partial void LogJobRegistered(ILogger logger, string jobId, string cron);
}
