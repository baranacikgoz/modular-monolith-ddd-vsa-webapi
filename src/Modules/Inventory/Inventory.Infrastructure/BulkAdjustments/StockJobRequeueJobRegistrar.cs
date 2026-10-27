using Common.Application.BackgroundJobs;
using Common.Application.Options;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Inventory.Infrastructure.BulkAdjustments;

/// <summary>Registers <see cref="StockJobRequeueService" /> as a recurring background job (InventoryOptions.StockJobRequeueCron).
/// Skips registration when the BackgroundJobs module is not loaded.</summary>
public sealed partial class StockJobRequeueJobRegistrar(
    IServiceProvider serviceProvider,
    IOptions<InventoryOptions> optionsProvider,
    ILogger<StockJobRequeueJobRegistrar> logger) : IHostedService
{
    private const string JobId = "inventory-stock-job-requeue";

    public Task StartAsync(CancellationToken cancellationToken)
    {
        var recurringJobs = serviceProvider.GetService<IRecurringBackgroundJobs>();
        if (recurringJobs is null)
        {
            LogBackgroundJobsUnavailable(logger);
            return Task.CompletedTask;
        }

        var cron = optionsProvider.Value.StockJobRequeueCron;
        recurringJobs.AddOrUpdate<StockJobRequeueService>(
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
        Message = "IRecurringBackgroundJobs is not available. Stuck job requeue sweep will not be registered. Ensure the BackgroundJobs module is loaded.")]
    private static partial void LogBackgroundJobsUnavailable(ILogger logger);

    [LoggerMessage(Level = LogLevel.Information,
        Message = "Registered recurring stuck job requeue sweep '{JobId}' with cron '{Cron}'.")]
    private static partial void LogJobRegistered(ILogger logger, string jobId, string cron);
}
