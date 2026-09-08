using Common.Application.BackgroundJobs;
using Common.Application.Options;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Inventory.Infrastructure.StockReservations;

/// <summary>Registers the stock reservation expiry sweep as a recurring background job. Mirrors
/// DeviceRegistryReconcileJobRegistrar: gracefully skips registration if the BackgroundJobs module is not loaded.</summary>
public sealed partial class StockReservationExpirySweepJobRegistrar(
    IServiceProvider serviceProvider,
    IOptions<InventoryOptions> inventoryOptionsProvider,
    ILogger<StockReservationExpirySweepJobRegistrar> logger) : IHostedService
{
    private const string JobId = "stock-reservation-expiry-sweep";

    public Task StartAsync(CancellationToken cancellationToken)
    {
        var recurringJobs = serviceProvider.GetService<IRecurringBackgroundJobs>();
        if (recurringJobs is null)
        {
            LogBackgroundJobsUnavailable(logger);
            return Task.CompletedTask;
        }

        var cron = inventoryOptionsProvider.Value.StockReservationExpirySweepCron;
        recurringJobs.AddOrUpdate<StockReservationExpirySweepService>(
            JobId,
            service => service.SweepAsync(CancellationToken.None),
            () => cron);

        LogJobRegistered(logger, JobId, cron);
        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;

    [LoggerMessage(Level = LogLevel.Warning,
        Message = "IRecurringBackgroundJobs is not available. Stock reservation expiry sweep job will not be registered. Ensure the BackgroundJobs module is loaded.")]
    private static partial void LogBackgroundJobsUnavailable(ILogger logger);

    [LoggerMessage(Level = LogLevel.Information,
        Message = "Registered recurring stock reservation expiry sweep job '{JobId}' with cron '{Cron}'.")]
    private static partial void LogJobRegistered(ILogger logger, string jobId, string cron);
}
