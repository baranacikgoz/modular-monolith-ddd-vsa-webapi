using Common.Application.Options;
using Microsoft.Extensions.Configuration;
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
            WriteToConsole = false,
            WriteToFile = false,
            EnableMetrics = false,
            EnableTracing = false,
            ResponseTimeThresholdInMs = 1000,
            TraceSamplingRatio = 1.0
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
    public void TraceSamplingRatio_Missing_Fails()
    {
        var options = ValidOptions();
        options.TraceSamplingRatio = null;

        var result = new ObservabilityOptionsValidator().Validate(options);

        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.PropertyName == nameof(ObservabilityOptions.TraceSamplingRatio));
    }

    // AddCommonOptions binds with the configuration binder, which leaves an absent key at the CLR default: a
    // non-nullable double would silently become 0 (tracing off) and pass the range rule.
    [Fact]
    public void TraceSamplingRatio_AbsentFromConfiguration_FailsInsteadOfBindingToZero()
    {
        var configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(new Dictionary<string, string?>
            {
                ["ObservabilityOptions:AppName"] = "app",
                ["ObservabilityOptions:AppVersion"] = "1.0.0",
                ["ObservabilityOptions:MinimumLevel"] = "Information",
                ["ObservabilityOptions:ResponseTimeThresholdInMs"] = "1000"
            })
            .Build();
        var options = configuration.GetSection(nameof(ObservabilityOptions)).Get<ObservabilityOptions>()!;

        var result = new ObservabilityOptionsValidator().Validate(options);

        Assert.Contains(result.Errors, e => e.PropertyName == nameof(ObservabilityOptions.TraceSamplingRatio));
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
