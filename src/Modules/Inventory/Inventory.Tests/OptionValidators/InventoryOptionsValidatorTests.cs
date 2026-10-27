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

    [Theory]
    [InlineData("")]
    [InlineData("Not/AZone")]
    public void UnknownBusinessTimeZoneId_FailsValidation(string businessTimeZoneId)
    {
        var result = new InventoryOptionsValidator().Validate(TestInventoryOptions.Create(businessTimeZoneId: businessTimeZoneId));

        Assert.False(result.IsValid);
    }
}
