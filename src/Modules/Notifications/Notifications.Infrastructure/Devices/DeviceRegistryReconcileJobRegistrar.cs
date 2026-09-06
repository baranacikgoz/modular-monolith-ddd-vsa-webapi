using Common.Application.BackgroundJobs;
using Common.Application.Options;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Notifications.Infrastructure.Devices;

/// <summary>Registers the device registry reconcile as a recurring background job. Mirrors
/// AuditLogRetentionJobRegistrar: gracefully skips registration if the BackgroundJobs module is not loaded.</summary>
public sealed partial class DeviceRegistryReconcileJobRegistrar(
    IServiceProvider serviceProvider,
    IOptions<DevicesOptions> devicesOptionsProvider,
    ILogger<DeviceRegistryReconcileJobRegistrar> logger) : IHostedService
{
    private const string JobId = "device-registry-reconcile";

    public Task StartAsync(CancellationToken cancellationToken)
    {
        var recurringJobs = serviceProvider.GetService<IRecurringBackgroundJobs>();
        if (recurringJobs is null)
        {
            LogBackgroundJobsUnavailable(logger);
            return Task.CompletedTask;
        }

        var cron = devicesOptionsProvider.Value.ReconcileCron;
        recurringJobs.AddOrUpdate<DeviceRegistryReconciliationService>(
            JobId,
            service => service.ReconcileAsync(CancellationToken.None),
            () => cron);

        LogJobRegistered(logger, JobId, cron);
        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;

    [LoggerMessage(Level = LogLevel.Warning,
        Message = "IRecurringBackgroundJobs is not available. Device registry reconcile job will not be registered. Ensure the BackgroundJobs module is loaded.")]
    private static partial void LogBackgroundJobsUnavailable(ILogger logger);

    [LoggerMessage(Level = LogLevel.Information,
        Message = "Registered recurring device registry reconcile job '{JobId}' with cron '{Cron}'.")]
    private static partial void LogJobRegistered(ILogger logger, string jobId, string cron);
}
