using Common.Application.BackgroundJobs;
using Common.Application.Options;
using Inventory.Application.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Inventory.Infrastructure.StoreAccesses;

/// <summary>
///     Registers the nightly store access reconciliation as a recurring background job and, when the copy is still empty
///     (first start after it was introduced), queues one run right away so the store routes do not answer "no store" until the
///     night. Gracefully skips registration if the BackgroundJobs module is not loaded. The queued run is the same idempotent
///     walk as the nightly one, so several replicas queueing it is harmless.
/// </summary>
public sealed partial class StoreAccessReconciliationJobRegistrar(
    IServiceProvider serviceProvider,
    IOptions<InventoryOptions> inventoryOptionsProvider,
    ILogger<StoreAccessReconciliationJobRegistrar> logger) : IHostedService
{
    private const string JobId = "inventory-store-access-reconciliation";

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        var recurringJobs = serviceProvider.GetService<IRecurringBackgroundJobs>();
        if (recurringJobs is null)
        {
            LogBackgroundJobsUnavailable(logger);
            return;
        }

        var cron = inventoryOptionsProvider.Value.StoreAccessReconciliationCron;
        recurringJobs.AddOrUpdate<StoreAccessReconciliationJob>(
            JobId,
            job => job.RunAsync(CancellationToken.None),
            () => cron);

        LogJobRegistered(logger, JobId, cron);

        await QueueFirstRunWhenEmptyAsync(cancellationToken);
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }

    private async Task QueueFirstRunWhenEmptyAsync(CancellationToken cancellationToken)
    {
        var backgroundJobs = serviceProvider.GetService<IBackgroundJobs>();
        if (backgroundJobs is null)
        {
            return;
        }

        try
        {
            using var scope = serviceProvider.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<IInventoryDbContext>();

            var isEmpty = !await dbContext.StoreAccess
                .AsNoTracking()
                .TagWith(nameof(QueueFirstRunWhenEmptyAsync))
                .AnyAsync(cancellationToken);
            if (!isEmpty)
            {
                return;
            }

            backgroundJobs.Enqueue<StoreAccessReconciliationJob>(job => job.RunAsync(CancellationToken.None));
            LogFirstRunQueued(logger, JobId);
        }
#pragma warning disable CA1031 // A failed start-up probe must not stop the host: the nightly run heals the copy anyway.
        catch (Exception ex)
#pragma warning restore CA1031
        {
            LogFirstRunNotQueued(logger, JobId, ex);
        }
    }

    [LoggerMessage(Level = LogLevel.Warning,
        Message = "IRecurringBackgroundJobs is not available. Store access reconciliation job will not be registered. Ensure the BackgroundJobs module is loaded.")]
    private static partial void LogBackgroundJobsUnavailable(ILogger logger);

    [LoggerMessage(Level = LogLevel.Information,
        Message = "Registered recurring store access reconciliation job '{JobId}' with cron '{Cron}'.")]
    private static partial void LogJobRegistered(ILogger logger, string jobId, string cron);

    [LoggerMessage(Level = LogLevel.Information,
        Message = "The store access copy is empty: queued one run of '{JobId}' now instead of waiting for the cron.")]
    private static partial void LogFirstRunQueued(ILogger logger, string jobId);

    [LoggerMessage(Level = LogLevel.Warning,
        Message = "Could not queue the first run of '{JobId}'; the cron run will fill the store access copy.")]
    private static partial void LogFirstRunNotQueued(ILogger logger, string jobId, Exception exception);
}
