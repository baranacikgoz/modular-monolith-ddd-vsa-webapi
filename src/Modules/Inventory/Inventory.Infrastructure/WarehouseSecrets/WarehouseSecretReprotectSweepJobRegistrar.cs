using Common.Application.BackgroundJobs;
using Common.Application.Options;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Inventory.Infrastructure.WarehouseSecrets;

/// <summary>Registers <see cref="WarehouseSecretReprotectSweepService" /> as a recurring background job (InventoryOptions.WarehouseSecretReprotectCron).
/// Skips registration when the BackgroundJobs module is not loaded.</summary>
public sealed partial class WarehouseSecretReprotectSweepJobRegistrar(
    IServiceProvider serviceProvider,
    IOptions<InventoryOptions> inventoryOptionsProvider,
    ILogger<WarehouseSecretReprotectSweepJobRegistrar> logger) : IHostedService
{
    private const string JobId = "inventory-warehouse-secret-reprotect-sweep";

    public Task StartAsync(CancellationToken cancellationToken)
    {
        var recurringJobs = serviceProvider.GetService<IRecurringBackgroundJobs>();
        if (recurringJobs is null)
        {
            LogBackgroundJobsUnavailable(logger);
            return Task.CompletedTask;
        }

        var cron = inventoryOptionsProvider.Value.WarehouseSecretReprotectCron;
        recurringJobs.AddOrUpdate<WarehouseSecretReprotectSweepService>(
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
        Message = "IRecurringBackgroundJobs is not available. Warehouse secret reprotect sweep will not be registered. Ensure the BackgroundJobs module is loaded.")]
    private static partial void LogBackgroundJobsUnavailable(ILogger logger);

    [LoggerMessage(Level = LogLevel.Information,
        Message = "Registered recurring warehouse secret reprotect sweep '{JobId}' with cron '{Cron}'.")]
    private static partial void LogJobRegistered(ILogger logger, string jobId, string cron);
}
