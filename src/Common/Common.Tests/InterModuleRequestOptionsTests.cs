using Common.Application.Options;
using Xunit;

#pragma warning disable CA1515, CA1707

namespace Common.Tests;

public sealed class InterModuleRequestOptionsTests
{
    private sealed record SampleRequest;

    private static InterModuleRequestOptions ValidOptions() => new() { TimeoutSeconds = 10 };

    [Fact]
    public void ValidOptions_PassesValidation()
    {
        var result = new InterModuleRequestOptionsValidator().Validate(ValidOptions());

        Assert.True(result.IsValid);
    }

    [Fact]
    public void Defaults_HandlerConcurrency_MatchCheckedInJson()
    {
        var options = ValidOptions();

        Assert.Equal(32, options.HandlerPrefetchCount);
        Assert.Equal(32, options.HandlerConcurrentMessageLimit);
        Assert.Empty(options.Timeouts);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void HandlerPrefetchCount_NotPositive_Fails(int value)
    {
        var options = ValidOptions();
        options.HandlerPrefetchCount = value;

        var result = new InterModuleRequestOptionsValidator().Validate(options);

        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.PropertyName == nameof(InterModuleRequestOptions.HandlerPrefetchCount));
    }

    [Fact]
    public void HandlerConcurrentMessageLimit_Zero_Fails()
    {
        var options = ValidOptions();
        options.HandlerConcurrentMessageLimit = 0;

        var result = new InterModuleRequestOptionsValidator().Validate(options);

        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.PropertyName == nameof(InterModuleRequestOptions.HandlerConcurrentMessageLimit));
    }

    [Fact]
    public void Timeouts_EntryNotPositive_Fails()
    {
        var options = ValidOptions();
        options.Timeouts[nameof(SampleRequest)] = 0;

        var result = new InterModuleRequestOptionsValidator().Validate(options);

        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.PropertyName.StartsWith(nameof(InterModuleRequestOptions.Timeouts), StringComparison.Ordinal));
    }

    [Fact]
    public void TimeoutSecondsFor_RequestWithOverride_ReturnsOverride()
    {
        var options = ValidOptions();
        options.Timeouts[nameof(SampleRequest)] = 3;

        Assert.Equal(3, options.TimeoutSecondsFor(typeof(SampleRequest)));
    }

    [Fact]
    public void TimeoutSecondsFor_RequestWithoutOverride_ReturnsDefault()
    {
        var options = ValidOptions();

        Assert.Equal(10, options.TimeoutSecondsFor(typeof(SampleRequest)));
    }
}
