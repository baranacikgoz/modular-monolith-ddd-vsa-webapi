using Common.Application.BackgroundJobs;
using Common.Application.Options;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Inventory.Infrastructure.BulkAdjustments;

/// <summary>Registers <see cref="StockJobsHousekeeping" /> as a recurring background job (InventoryOptions.StockJobHousekeepingCron).
/// Skips registration when the BackgroundJobs module is not loaded.</summary>
public sealed partial class StockJobsHousekeepingRegistrar(
    IServiceProvider serviceProvider,
    IOptions<InventoryOptions> optionsProvider,
    ILogger<StockJobsHousekeepingRegistrar> logger) : IHostedService
{
    private const string JobId = "inventory-stock-job-housekeeping";

    public Task StartAsync(CancellationToken cancellationToken)
    {
        var recurringJobs = serviceProvider.GetService<IRecurringBackgroundJobs>();
        if (recurringJobs is null)
        {
            LogBackgroundJobsUnavailable(logger);
            return Task.CompletedTask;
        }

        var cron = optionsProvider.Value.StockJobHousekeepingCron;
        recurringJobs.AddOrUpdate<StockJobsHousekeeping>(
            JobId,
            job => job.RunAsync(CancellationToken.None),
            () => cron);

        LogJobRegistered(logger, JobId, cron);
        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }

    [LoggerMessage(Level = LogLevel.Warning,
        Message = "IRecurringBackgroundJobs is not available. Stock job housekeeping will not be registered. Ensure the BackgroundJobs module is loaded.")]
    private static partial void LogBackgroundJobsUnavailable(ILogger logger);

    [LoggerMessage(Level = LogLevel.Information,
        Message = "Registered recurring stock job housekeeping '{JobId}' with cron '{Cron}'.")]
    private static partial void LogJobRegistered(ILogger logger, string jobId, string cron);
}
