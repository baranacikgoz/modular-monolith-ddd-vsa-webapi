using Common.Application.Options;
using Xunit;

namespace Inventory.Tests.OptionValidators;

public class InventoryOptionsValidatorTests
{
    private static InventoryOptions Valid(string productSummaryReconciliationCron = "0 3 * * *", string businessTimeZoneId = "Europe/Berlin")
    {
        return new InventoryOptions
        {
            MaxOccurrencesPerSeries = 12,
            SafetyBufferQuantity = 5,
            StockReservationExpirySweepCron = "0 */15 * * * *",
            StockReservationExpirySweepBatchSize = 100,
            ProductSummaryReconciliationCron = productSummaryReconciliationCron,
            BusinessTimeZoneId = businessTimeZoneId,
            WarehouseGatewayProvider = WarehouseGatewayProvider.Dummy,
            WebhookSharedSecret = "secret",
            WebhookMaxBodyBytes = 65536
        };
    }

    [Fact]
    public void ValidOptions_PassValidation()
    {
        var result = new InventoryOptionsValidator().Validate(Valid());

        Assert.True(result.IsValid);
    }

    [Fact]
    public void EmptyProductSummaryReconciliationCron_FailsValidation()
    {
        var result = new InventoryOptionsValidator().Validate(Valid(productSummaryReconciliationCron: ""));

        Assert.False(result.IsValid);
    }

    [Theory]
    [InlineData("")]
    [InlineData("Not/AZone")]
    public void UnknownBusinessTimeZoneId_FailsValidation(string businessTimeZoneId)
    {
        var result = new InventoryOptionsValidator().Validate(Valid(businessTimeZoneId: businessTimeZoneId));

        Assert.False(result.IsValid);
    }
}
