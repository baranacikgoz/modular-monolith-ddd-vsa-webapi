using Common.Application.BackgroundJobs;
using Common.Application.Options;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Notifications.Infrastructure.Alerts;

/// <summary>Registers the alert recipients reconcile as a recurring background job. Mirrors
/// DeviceRegistryReconcileJobRegistrar: gracefully skips registration if the BackgroundJobs module is not loaded.</summary>
public sealed partial class AlertRecipientsReconcileJobRegistrar(
    IServiceProvider serviceProvider,
    IOptions<AlertRecipientsOptions> optionsProvider,
    ILogger<AlertRecipientsReconcileJobRegistrar> logger) : IHostedService
{
    private const string JobId = "notifications-alert-recipients-reconcile";

    public Task StartAsync(CancellationToken cancellationToken)
    {
        var recurringJobs = serviceProvider.GetService<IRecurringBackgroundJobs>();
        if (recurringJobs is null)
        {
            LogBackgroundJobsUnavailable(logger);
            return Task.CompletedTask;
        }

        var cron = optionsProvider.Value.ReconcileCron;
        recurringJobs.AddOrUpdate<AlertRecipientsReconciliationJob>(
            JobId,
            job => job.ReconcileAsync(CancellationToken.None),
            () => cron);

        LogJobRegistered(logger, JobId, cron);
        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;

    [LoggerMessage(Level = LogLevel.Warning,
        Message = "IRecurringBackgroundJobs is not available. Alert recipients reconcile job will not be registered. Ensure the BackgroundJobs module is loaded.")]
    private static partial void LogBackgroundJobsUnavailable(ILogger logger);

    [LoggerMessage(Level = LogLevel.Information,
        Message = "Registered recurring alert recipients reconcile job '{JobId}' with cron '{Cron}'.")]
    private static partial void LogJobRegistered(ILogger logger, string jobId, string cron);
}
