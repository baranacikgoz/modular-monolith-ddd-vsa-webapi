using Common.Application.Options;
using Xunit;

#pragma warning disable CA1515, CA1707

namespace Common.Tests;

public sealed class OutboxOptionsValidatorTests
{
    private static OutboxOptions ValidOptions() => new()
    {
        PollIntervalMs = 500,
        BatchSize = 50,
        MaxRetryCount = 3,
        IsProcessor = true
    };

    [Fact]
    public void ValidOptions_PassesValidation()
    {
        var validator = new OutboxOptionsValidator();
        var result = validator.Validate(ValidOptions());

        Assert.True(result.IsValid);
    }

    [Fact]
    public void PollIntervalMs_BelowMinimum_Fails()
    {
        var options = new OutboxOptions { PollIntervalMs = 50, BatchSize = 50, MaxRetryCount = 3, IsProcessor = true };
        var validator = new OutboxOptionsValidator();
        var result = validator.Validate(options);

        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.PropertyName == nameof(OutboxOptions.PollIntervalMs));
    }

    [Fact]
    public void BatchSize_Zero_Fails()
    {
        var options = new OutboxOptions { PollIntervalMs = 500, BatchSize = 0, MaxRetryCount = 3, IsProcessor = true };
        var validator = new OutboxOptionsValidator();
        var result = validator.Validate(options);

        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.PropertyName == nameof(OutboxOptions.BatchSize));
    }

    [Fact]
    public void MaxRetryCount_Zero_Fails()
    {
        var options = new OutboxOptions { PollIntervalMs = 500, BatchSize = 50, MaxRetryCount = 0, IsProcessor = true };
        var validator = new OutboxOptionsValidator();
        var result = validator.Validate(options);

        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.PropertyName == nameof(OutboxOptions.MaxRetryCount));
    }

    [Fact]
    public void LagThresholdMinutes_Zero_Fails()
    {
        var options = new OutboxOptions { PollIntervalMs = 500, BatchSize = 50, MaxRetryCount = 3, IsProcessor = true, LagThresholdMinutes = 0 };
        var validator = new OutboxOptionsValidator();
        var result = validator.Validate(options);

        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.PropertyName == nameof(OutboxOptions.LagThresholdMinutes));
    }

    [Fact]
    public void MetricsCronSchedule_Empty_Fails()
    {
        var options = new OutboxOptions { PollIntervalMs = 500, BatchSize = 50, MaxRetryCount = 3, IsProcessor = true, MetricsCronSchedule = "" };
        var validator = new OutboxOptionsValidator();
        var result = validator.Validate(options);

        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.PropertyName == nameof(OutboxOptions.MetricsCronSchedule));
    }

    [Fact]
    public void CleanupOptions_Default_IsValid()
    {
        var options = ValidOptions();
        var validator = new OutboxOptionsValidator();
        var result = validator.Validate(options);

        Assert.True(result.IsValid);
        Assert.True(options.Cleanup.Enabled);
        Assert.Equal(7, options.Cleanup.RetentionDays);
        Assert.Equal(1000, options.Cleanup.BatchSize);
    }

    [Fact]
    public void CleanupOptions_NegativeRetentionDays_Fails()
    {
        var options = new OutboxCleanupSettings { RetentionDays = 0 };
        var validator = new OutboxCleanupSettingsValidator();
        var result = validator.Validate(options);

        Assert.False(result.IsValid);
    }
}
