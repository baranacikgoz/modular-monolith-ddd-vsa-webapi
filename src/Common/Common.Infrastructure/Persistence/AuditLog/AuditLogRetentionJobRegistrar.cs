using Common.Application.BackgroundJobs;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Common.Infrastructure.Persistence.AuditLog;

/// <summary>
/// Registers the audit log retention purge as a recurring background job
/// that runs daily at 2:00 AM. Gracefully skips registration if the
/// BackgroundJobs module is not loaded.
/// </summary>
public sealed partial class AuditLogRetentionJobRegistrar(
    IServiceProvider serviceProvider,
    ILogger<AuditLogRetentionJobRegistrar> logger) : IHostedService
{
    private const string JobId = "audit-log-retention-purge";
    private const string DailyCron = "0 2 * * *"; // Every day at 2:00 AM

    public Task StartAsync(CancellationToken cancellationToken)
    {
        var recurringJobs = serviceProvider.GetService<IRecurringBackgroundJobs>();
        if (recurringJobs is null)
        {
            LoggerMessages.LogBackgroundJobsUnavailable(logger);
            return Task.CompletedTask;
        }

        recurringJobs.AddOrUpdate<AuditLogRetentionService>(
            JobId,
            service => service.PurgeExpiredEntriesAsync(CancellationToken.None),
            () => DailyCron);

        LoggerMessages.LogJobRegistered(logger, JobId, DailyCron);
        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;

    private static partial class LoggerMessages
    {
        [LoggerMessage(Level = LogLevel.Warning,
            Message = "IRecurringBackgroundJobs is not available. Audit log retention job will not be registered. Ensure the BackgroundJobs module is loaded.")]
        public static partial void LogBackgroundJobsUnavailable(ILogger logger);

        [LoggerMessage(Level = LogLevel.Information,
            Message = "Registered recurring audit log retention job '{JobId}' with cron '{Cron}'.")]
        public static partial void LogJobRegistered(ILogger logger, string jobId, string cron);
    }
}
