using Common.Application.Options;
using Xunit;

#pragma warning disable CA1515, CA1707

namespace Common.Tests;

public sealed class DevicesOptionsValidatorTests
{
    [Fact]
    public void ValidOptions_PassesValidation()
    {
        var result = new DevicesOptionsValidator().Validate(new DevicesOptions { AllowedClientIds = ["mobile-app-1"] });

        Assert.True(result.IsValid);
    }

    [Fact]
    public void EmptyAllowList_FailsValidation()
    {
        var result = new DevicesOptionsValidator().Validate(new DevicesOptions { AllowedClientIds = [] });

        Assert.False(result.IsValid);
    }

    [Fact]
    public void BlankEntry_FailsValidation()
    {
        var result = new DevicesOptionsValidator().Validate(new DevicesOptions { AllowedClientIds = ["mobile-app-1", " "] });

        Assert.False(result.IsValid);
    }
}
