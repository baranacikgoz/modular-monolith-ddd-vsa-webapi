using Common.Application.Options;
using Inventory.Application.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Inventory.Infrastructure.Webhooks;

/// <summary>
///     Deletes recorded webhook deliveries older than <see cref="InventoryOptions.WebhookDeliveryRetentionDays" />. A delivery id
///     only has to be remembered for as long as the warehouse system may deliver it again. One statement per run, served by
///     the index on the arrival time: the daily cadence keeps each run to about a day of rows.
/// </summary>
public sealed partial class WebhookDeliveryRetentionService(
    IInventoryDbContext dbContext,
    TimeProvider timeProvider,
    IOptions<InventoryOptions> inventoryOptionsProvider,
    ILogger<WebhookDeliveryRetentionService> logger)
{
    public async Task PurgeAsync(CancellationToken cancellationToken = default)
    {
        var cutoff = timeProvider.GetUtcNow().AddDays(-inventoryOptionsProvider.Value.WebhookDeliveryRetentionDays);

        var deleted = await dbContext.WebhookDeliveries
            .TagWith(nameof(WebhookDeliveryRetentionService))
            .Where(d => d.ReceivedOn < cutoff)
            .ExecuteDeleteAsync(cancellationToken);

        LogPurged(logger, deleted, cutoff);
    }

    [LoggerMessage(Level = LogLevel.Information, Message = "Webhook delivery retention deleted {DeletedCount} deliveries received before {Cutoff}.")]
    private static partial void LogPurged(ILogger logger, int deletedCount, DateTimeOffset cutoff);
}
