using Common.Application.Options;
using Inventory.Tests.Support;
using Xunit;

namespace Inventory.Tests.OptionValidators;

public class InventoryOptionsValidatorTests
{
    [Fact]
    public void ValidOptions_PassValidation()
    {
        var result = new InventoryOptionsValidator().Validate(TestInventoryOptions.Create());

        Assert.True(result.IsValid);
    }

    [Fact]
    public void EmptyProductSummaryReconciliationCron_FailsValidation()
    {
        var result = new InventoryOptionsValidator().Validate(TestInventoryOptions.Create(productSummaryReconciliationCron: ""));

        Assert.False(result.IsValid);
    }

    [Fact]
    public void EmptyStoreAccessReconciliationCron_FailsValidation()
    {
        var result = new InventoryOptionsValidator().Validate(
            TestInventoryOptions.Create(configure: o => o.StoreAccessReconciliationCron = ""));

        Assert.False(result.IsValid);
    }

    [Theory]
    [InlineData("")]
    [InlineData("Not/AZone")]
    public void UnknownBusinessTimeZoneId_FailsValidation(string businessTimeZoneId)
    {
        var result = new InventoryOptionsValidator().Validate(TestInventoryOptions.Create(businessTimeZoneId: businessTimeZoneId));

        Assert.False(result.IsValid);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void NonPositiveDailyTotalsMaxRangeDays_FailsValidation(int days)
    {
        var result = new InventoryOptionsValidator().Validate(TestInventoryOptions.Create(configure: o => o.DailyTotalsMaxRangeDays = days));

        Assert.False(result.IsValid);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void NonPositiveWebhookDeliveryRetentionDays_FailsValidation(int days)
    {
        var result = new InventoryOptionsValidator().Validate(TestInventoryOptions.Create(webhookDeliveryRetentionDays: days));

        Assert.False(result.IsValid);
    }

    [Fact]
    public void EmptyWebhookDeliveryRetentionCron_FailsValidation()
    {
        var result = new InventoryOptionsValidator().Validate(TestInventoryOptions.Create(webhookDeliveryRetentionCron: ""));

        Assert.False(result.IsValid);
    }

    [Fact]
    public void EmptyStockPushDirtySweepCron_FailsValidation()
    {
        var result = new InventoryOptionsValidator().Validate(TestInventoryOptions.Create(configure: o => o.StockPushDirtySweepCron = ""));

        Assert.False(result.IsValid);
    }

    [Fact]
    public void EmptyStockJobHousekeepingCron_FailsValidation()
    {
        var result = new InventoryOptionsValidator().Validate(TestInventoryOptions.Create(configure: o => o.StockJobHousekeepingCron = ""));

        Assert.False(result.IsValid);
    }

    [Fact]
    public void EmptyStockJobRequeueCron_FailsValidation()
    {
        var result = new InventoryOptionsValidator().Validate(TestInventoryOptions.Create(configure: o => o.StockJobRequeueCron = ""));

        Assert.False(result.IsValid);
    }

    [Theory]
    [InlineData(nameof(InventoryOptions.StockPushDirtyMinAgeSeconds))]
    [InlineData(nameof(InventoryOptions.StockPushBatchSize))]
    [InlineData(nameof(InventoryOptions.StockPushBatchesPerMessage))]
    [InlineData(nameof(InventoryOptions.StockPushConcurrentMessageLimit))]
    [InlineData(nameof(InventoryOptions.SweepPageSize))]
    [InlineData(nameof(InventoryOptions.BulkAdjustJobConcurrentMessageLimit))]
    [InlineData(nameof(InventoryOptions.BulkAdjustBatchesPerMessage))]
    [InlineData(nameof(InventoryOptions.BulkAdjustMaxStoredRowErrors))]
    [InlineData(nameof(InventoryOptions.StockJobRunningLeaseMinutes))]
    [InlineData(nameof(InventoryOptions.StockJobMaxRequeues))]
    public void ZeroTunable_FailsValidation(string tunable)
    {
        var options = TestInventoryOptions.Create();
        typeof(InventoryOptions).GetProperty(tunable)!.SetValue(options, 0);

        var result = new InventoryOptionsValidator().Validate(options);

        Assert.False(result.IsValid);
    }
}
