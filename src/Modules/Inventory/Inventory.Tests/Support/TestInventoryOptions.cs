using Common.Application.Options;
using Microsoft.Extensions.Options;

namespace Inventory.Tests.Support;

internal static class TestInventoryOptions
{
    /// <summary>A valid options object; the arguments are the fields a test varies, <paramref name="configure" /> bends any other.</summary>
    public static InventoryOptions Create(
        string productSummaryReconciliationCron = "0 3 * * *",
        string businessTimeZoneId = "Europe/Berlin",
        int webhookDeliveryRetentionDays = 30,
        string webhookDeliveryRetentionCron = "0 4 * * *",
        Action<InventoryOptions>? configure = null)
    {
        var options = new InventoryOptions
        {
            MaxOccurrencesPerSeries = 12,
            SafetyBufferQuantity = 5,
            StockReservationExpirySweepCron = "0 */15 * * * *",
            StockReservationExpirySweepBatchSize = 100,
            StockReleaseReconcileSweepCron = "0 */10 * * * *",
            StockReleaseReconcileMinAgeSeconds = 600,
            ProductSummaryReconciliationCron = productSummaryReconciliationCron,
            StoreAccessReconciliationCron = "30 3 * * *",
            BusinessTimeZoneId = businessTimeZoneId,
            DailyTotalsMaxRangeDays = 366,
            WebhookMaxBodyBytes = 65536,
            WebhookDeliveryRetentionDays = webhookDeliveryRetentionDays,
            WebhookDeliveryRetentionCron = webhookDeliveryRetentionCron,
            StockPushDirtySweepCron = "0 */2 * * * *",
            StockPushDirtyMinAgeSeconds = 120,
            StockPushBatchSize = 100,
            StockPushBatchesPerMessage = 10,
            StockPushConcurrentMessageLimit = 4,
            SweepPageSize = 100,
            BulkAdjustJobConcurrentMessageLimit = 2,
            BulkAdjustBatchesPerMessage = 10,
            BulkAdjustMaxStoredRowErrors = 200,
            StockJobHousekeepingCron = "0 25 * * * *",
            StockJobRequeueCron = "0 */5 * * * *",
            StockJobRunningLeaseMinutes = 15,
            StockJobMaxRequeues = 3
        };
        configure?.Invoke(options);
        return options;
    }

    /// <summary>A private copy of the options for a runner under test, so no test changes what the running host shares.</summary>
    public static IOptions<InventoryOptions> Wrapped(Action<InventoryOptions>? configure = null)
    {
        return Options.Create(Create(configure: configure));
    }

    public static IOptions<BulkImportOptions> BulkImport(Action<BulkImportOptions>? configure = null)
    {
        var options = new BulkImportOptions { MaxFileBytes = 10485760, MaxRows = 50000, SaveChunkSize = 200 };
        configure?.Invoke(options);
        return Options.Create(options);
    }
}
