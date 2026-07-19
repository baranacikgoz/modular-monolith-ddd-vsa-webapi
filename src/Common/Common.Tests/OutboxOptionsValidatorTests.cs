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
        IsProcessor = true,
        BaseBackoffSeconds = 5,
        MaxBackoffSeconds = 600,
        PublishTimeoutMs = 5000,
        ClaimLeaseSeconds = 120,
        MaxConsecutiveFailures = 3,
        LagThresholdMinutes = 5,
        MetricsCronSchedule = "*/5 * * * *"
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
        var options = ValidOptions();
        options.PollIntervalMs = 50;
        var validator = new OutboxOptionsValidator();
        var result = validator.Validate(options);

        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.PropertyName == nameof(OutboxOptions.PollIntervalMs));
    }

    [Fact]
    public void BatchSize_Zero_Fails()
    {
        var options = ValidOptions();
        options.BatchSize = 0;
        var validator = new OutboxOptionsValidator();
        var result = validator.Validate(options);

        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.PropertyName == nameof(OutboxOptions.BatchSize));
    }

    [Fact]
    public void MaxRetryCount_Zero_Fails()
    {
        var options = ValidOptions();
        options.MaxRetryCount = 0;
        var validator = new OutboxOptionsValidator();
        var result = validator.Validate(options);

        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.PropertyName == nameof(OutboxOptions.MaxRetryCount));
    }

    [Fact]
    public void LagThresholdMinutes_Zero_Fails()
    {
        var options = ValidOptions();
        options.LagThresholdMinutes = 0;
        var validator = new OutboxOptionsValidator();
        var result = validator.Validate(options);

        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.PropertyName == nameof(OutboxOptions.LagThresholdMinutes));
    }

    [Fact]
    public void MetricsCronSchedule_Empty_Fails()
    {
        var options = ValidOptions();
        options.MetricsCronSchedule = "";
        var validator = new OutboxOptionsValidator();
        var result = validator.Validate(options);

        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.PropertyName == nameof(OutboxOptions.MetricsCronSchedule));
    }

    [Fact]
    public void BaseBackoffSeconds_Zero_Fails()
    {
        var options = ValidOptions();
        options.BaseBackoffSeconds = 0;
        var validator = new OutboxOptionsValidator();
        var result = validator.Validate(options);

        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.PropertyName == nameof(OutboxOptions.BaseBackoffSeconds));
    }

    [Fact]
    public void MaxBackoffSeconds_Zero_Fails()
    {
        var options = ValidOptions();
        options.MaxBackoffSeconds = 0;
        var validator = new OutboxOptionsValidator();
        var result = validator.Validate(options);

        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.PropertyName == nameof(OutboxOptions.MaxBackoffSeconds));
    }

    [Fact]
    public void MaxBackoffSeconds_LessThanBaseBackoffSeconds_Fails()
    {
        var options = ValidOptions();
        options.BaseBackoffSeconds = 10;
        options.MaxBackoffSeconds = 5;
        var validator = new OutboxOptionsValidator();
        var result = validator.Validate(options);

        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.PropertyName == nameof(OutboxOptions.MaxBackoffSeconds));
    }

    [Fact]
    public void PublishTimeoutMs_BelowMinimum_Fails()
    {
        var options = ValidOptions();
        options.PublishTimeoutMs = 999;
        var validator = new OutboxOptionsValidator();
        var result = validator.Validate(options);

        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.PropertyName == nameof(OutboxOptions.PublishTimeoutMs));
    }

    [Fact]
    public void ClaimLeaseSeconds_BelowMinimum_Fails()
    {
        var options = ValidOptions();
        options.ClaimLeaseSeconds = 59;
        var validator = new OutboxOptionsValidator();
        var result = validator.Validate(options);

        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.PropertyName == nameof(OutboxOptions.ClaimLeaseSeconds));
    }

    [Fact]
    public void MaxConsecutiveFailures_Zero_Fails()
    {
        var options = ValidOptions();
        options.MaxConsecutiveFailures = 0;
        var validator = new OutboxOptionsValidator();
        var result = validator.Validate(options);

        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.PropertyName == nameof(OutboxOptions.MaxConsecutiveFailures));
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
