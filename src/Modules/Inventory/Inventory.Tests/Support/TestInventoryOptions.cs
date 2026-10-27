using Common.Application.Options;

namespace Inventory.Tests.Support;

internal static class TestInventoryOptions
{
    /// <summary>A valid options object; the arguments are the fields a test varies.</summary>
    public static InventoryOptions Create(
        string productSummaryReconciliationCron = "0 3 * * *", string businessTimeZoneId = "Europe/Berlin")
    {
        return new InventoryOptions
        {
            MaxOccurrencesPerSeries = 12,
            SafetyBufferQuantity = 5,
            StockReservationExpirySweepCron = "0 */15 * * * *",
            StockReservationExpirySweepBatchSize = 100,
            ProductSummaryReconciliationCron = productSummaryReconciliationCron,
            BusinessTimeZoneId = businessTimeZoneId,
            WebhookMaxBodyBytes = 65536
        };
    }
}
