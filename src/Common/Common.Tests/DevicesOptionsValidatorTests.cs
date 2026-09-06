using Common.Application.Options;
using Xunit;

#pragma warning disable CA1515, CA1707

namespace Common.Tests;

public sealed class DevicesOptionsValidatorTests
{
    private static DevicesOptions Valid(IReadOnlyCollection<string>? allowedClientIds = null,
        string reconcileCron = "0 */6 * * *", int reconcileBatchSize = 200)
    {
        return new DevicesOptions
        {
            AllowedClientIds = allowedClientIds ?? ["mobile-app-1"],
            ReconcileCron = reconcileCron,
            ReconcileBatchSize = reconcileBatchSize
        };
    }

    [Fact]
    public void ValidOptions_PassesValidation()
    {
        var result = new DevicesOptionsValidator().Validate(Valid());

        Assert.True(result.IsValid);
    }

    [Fact]
    public void EmptyAllowList_FailsValidation()
    {
        var result = new DevicesOptionsValidator().Validate(Valid(allowedClientIds: []));

        Assert.False(result.IsValid);
    }

    [Fact]
    public void BlankEntry_FailsValidation()
    {
        var result = new DevicesOptionsValidator().Validate(Valid(allowedClientIds: ["mobile-app-1", " "]));

        Assert.False(result.IsValid);
    }

    [Fact]
    public void EmptyReconcileCron_FailsValidation()
    {
        var result = new DevicesOptionsValidator().Validate(Valid(reconcileCron: ""));

        Assert.False(result.IsValid);
    }

    [Fact]
    public void NonPositiveReconcileBatchSize_FailsValidation()
    {
        var result = new DevicesOptionsValidator().Validate(Valid(reconcileBatchSize: 0));

        Assert.False(result.IsValid);
    }
}
