using Common.Application.BackgroundJobs;
using Common.Application.Options;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Inventory.Infrastructure.StockPushes;

/// <summary>Registers <see cref="StockPushSweepService" /> as a recurring background job (InventoryOptions.StockPushDirtySweepCron).
/// Skips registration when the BackgroundJobs module is not loaded.</summary>
public sealed partial class StockPushSweepJobRegistrar(
    IServiceProvider serviceProvider,
    IOptions<InventoryOptions> inventoryOptionsProvider,
    ILogger<StockPushSweepJobRegistrar> logger) : IHostedService
{
    private const string JobId = "inventory-stock-push-sweep";

    public Task StartAsync(CancellationToken cancellationToken)
    {
        var recurringJobs = serviceProvider.GetService<IRecurringBackgroundJobs>();
        if (recurringJobs is null)
        {
            LogBackgroundJobsUnavailable(logger);
            return Task.CompletedTask;
        }

        var cron = inventoryOptionsProvider.Value.StockPushDirtySweepCron;
        recurringJobs.AddOrUpdate<StockPushSweepService>(
            JobId,
            service => service.SweepAsync(CancellationToken.None),
            () => cron);

        LogJobRegistered(logger, JobId, cron);
        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }

    [LoggerMessage(Level = LogLevel.Warning,
        Message = "IRecurringBackgroundJobs is not available. Stock push sweep will not be registered. Ensure the BackgroundJobs module is loaded.")]
    private static partial void LogBackgroundJobsUnavailable(ILogger logger);

    [LoggerMessage(Level = LogLevel.Information,
        Message = "Registered recurring stock push sweep '{JobId}' with cron '{Cron}'.")]
    private static partial void LogJobRegistered(ILogger logger, string jobId, string cron);
}
