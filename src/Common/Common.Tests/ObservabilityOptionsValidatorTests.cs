using Common.Application.Options;
using Xunit;

#pragma warning disable CA1515, CA1707

namespace Common.Tests;

public sealed class ObservabilityOptionsValidatorTests
{
    private static ObservabilityOptions ValidOptions()
    {
        var options = new ObservabilityOptions
        {
            AppName = "app",
            AppVersion = "1.0.0",
            MinimumLevel = "Information",
            ResponseTimeThresholdInMs = 1000
        };
        options.MinimumLevelOverrides["Microsoft"] = "Warning";
        return options;
    }

    [Fact]
    public void ValidOptions_PassesValidation()
    {
        var result = new ObservabilityOptionsValidator().Validate(ValidOptions());

        Assert.True(result.IsValid);
    }

    [Fact]
    public void TraceSamplingRatio_DefaultsToRecordEverything()
    {
        Assert.Equal(1.0, ValidOptions().TraceSamplingRatio);
    }

    [Theory]
    [InlineData(0.0)]
    [InlineData(0.25)]
    [InlineData(1.0)]
    public void TraceSamplingRatio_WithinUnitInterval_Passes(double ratio)
    {
        var options = ValidOptions();
        options.TraceSamplingRatio = ratio;

        Assert.True(new ObservabilityOptionsValidator().Validate(options).IsValid);
    }

    [Theory]
    [InlineData(-0.1)]
    [InlineData(1.1)]
    public void TraceSamplingRatio_OutsideUnitInterval_Fails(double ratio)
    {
        var options = ValidOptions();
        options.TraceSamplingRatio = ratio;

        var result = new ObservabilityOptionsValidator().Validate(options);

        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.PropertyName == nameof(ObservabilityOptions.TraceSamplingRatio));
    }
}
