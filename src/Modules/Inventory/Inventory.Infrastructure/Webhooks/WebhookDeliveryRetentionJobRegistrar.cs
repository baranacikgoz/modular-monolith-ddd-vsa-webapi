using Common.Application.BackgroundJobs;
using Common.Application.Options;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Inventory.Infrastructure.Webhooks;

/// <summary>Registers the webhook delivery retention as a recurring background job. Skips registration, with a warning, when the BackgroundJobs module is not loaded.</summary>
public sealed partial class WebhookDeliveryRetentionJobRegistrar(
    IServiceProvider serviceProvider,
    IOptions<InventoryOptions> inventoryOptionsProvider,
    ILogger<WebhookDeliveryRetentionJobRegistrar> logger) : IHostedService
{
    private const string JobId = "inventory-webhook-delivery-retention";

    public Task StartAsync(CancellationToken cancellationToken)
    {
        var recurringJobs = serviceProvider.GetService<IRecurringBackgroundJobs>();
        if (recurringJobs is null)
        {
            LogBackgroundJobsUnavailable(logger);
            return Task.CompletedTask;
        }

        var cron = inventoryOptionsProvider.Value.WebhookDeliveryRetentionCron;
        recurringJobs.AddOrUpdate<WebhookDeliveryRetentionService>(
            JobId,
            service => service.PurgeAsync(CancellationToken.None),
            () => cron);

        LogJobRegistered(logger, JobId, cron);
        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;

    [LoggerMessage(Level = LogLevel.Warning,
        Message = "IRecurringBackgroundJobs is not available. Webhook delivery retention job will not be registered. Ensure the BackgroundJobs module is loaded.")]
    private static partial void LogBackgroundJobsUnavailable(ILogger logger);

    [LoggerMessage(Level = LogLevel.Information, Message = "Registered recurring webhook delivery retention job '{JobId}' with cron '{Cron}'.")]
    private static partial void LogJobRegistered(ILogger logger, string jobId, string cron);
}
