using Common.Application.Options;
using Xunit;

#pragma warning disable CA1515, CA1707

namespace Common.Tests;

#pragma warning disable CA1515, CA1707

public sealed class OutboxOptionsValidatorTests
{
    private static readonly KafkaConsumer ValidConsumer = new()
    {
        BootstrapServers = "localhost:9092",
        GroupId = "test-group",
        TopicName = "test-topic",
        AutoOffsetReset = "Earliest",
        SessionTimeoutMs = 30000,
        HeartbeatIntervalMs = 7000,
        EnablePartitionEof = false,
        MaxPollIntervalMs = 300000
    };

    private static readonly KafkaProducer ValidProducer = new()
    {
        BootstrapServers = "localhost:9092",
        TopicName = "test-dlq-topic"
    };

    [Fact]
    public void ValidOptions_PassesValidation()
    {
        var options = new OutboxOptions
        {
            KafkaConsumer = ValidConsumer,
            KafkaDlqProducer = ValidProducer,
            SetupRetryDelaySeconds = 10,
            ConsumeErrorDelaySeconds = 5,
            ProcessingErrorDelaySeconds = 1,
            ProcessTimeoutSeconds = 30
        };

        var validator = new OutboxOptionsValidator();
        var result = validator.Validate(options);

        Assert.True(result.IsValid);
    }

    [Fact]
    public void ProcessTimeoutSeconds_LessThanOne_Fails()
    {
        var options = new OutboxOptions
        {
            KafkaConsumer = ValidConsumer,
            KafkaDlqProducer = ValidProducer,
            SetupRetryDelaySeconds = 10,
            ConsumeErrorDelaySeconds = 5,
            ProcessingErrorDelaySeconds = 1,
            ProcessTimeoutSeconds = 0
        };

        var validator = new OutboxOptionsValidator();
        var result = validator.Validate(options);

        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.PropertyName == "ProcessTimeoutSeconds");
    }

    [Fact]
    public void ProcessTimeoutSeconds_ExceedsMaxPollIntervalMs_Fails()
    {
        var consumer = new KafkaConsumer
        {
            BootstrapServers = "localhost:9092",
            GroupId = "test-group",
            TopicName = "test-topic",
            AutoOffsetReset = "Earliest",
            SessionTimeoutMs = 30000,
            HeartbeatIntervalMs = 7000,
            EnablePartitionEof = false,
            MaxPollIntervalMs = 5000
        };
        var options = new OutboxOptions
        {
            KafkaConsumer = consumer,
            KafkaDlqProducer = ValidProducer,
            SetupRetryDelaySeconds = 10,
            ConsumeErrorDelaySeconds = 5,
            ProcessingErrorDelaySeconds = 1,
            ProcessTimeoutSeconds = 10
        };

        var validator = new OutboxOptionsValidator();
        var result = validator.Validate(options);

        Assert.False(result.IsValid);
    }

    [Fact]
    public void ProcessTimeoutSeconds_WithinMaxPollIntervalMs_Passes()
    {
        var consumer = new KafkaConsumer
        {
            BootstrapServers = "localhost:9092",
            GroupId = "test-group",
            TopicName = "test-topic",
            AutoOffsetReset = "Earliest",
            SessionTimeoutMs = 30000,
            HeartbeatIntervalMs = 7000,
            EnablePartitionEof = false,
            MaxPollIntervalMs = 30000
        };
        var options = new OutboxOptions
        {
            KafkaConsumer = consumer,
            KafkaDlqProducer = ValidProducer,
            SetupRetryDelaySeconds = 10,
            ConsumeErrorDelaySeconds = 5,
            ProcessingErrorDelaySeconds = 1,
            ProcessTimeoutSeconds = 15
        };

        var validator = new OutboxOptionsValidator();
        var result = validator.Validate(options);

        Assert.True(result.IsValid);
    }

    [Fact]
    public void CleanupOptions_Default_IsValid()
    {
        var options = new OutboxOptions
        {
            KafkaConsumer = ValidConsumer,
            KafkaDlqProducer = ValidProducer,
            SetupRetryDelaySeconds = 10,
            ConsumeErrorDelaySeconds = 5,
            ProcessingErrorDelaySeconds = 1,
            ProcessTimeoutSeconds = 30
        };

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

    [Fact]
    public void MaxConsecutiveDlqFailures_Zero_Fails()
    {
        var options = new OutboxOptions
        {
            KafkaConsumer = ValidConsumer,
            KafkaDlqProducer = ValidProducer,
            SetupRetryDelaySeconds = 10,
            ConsumeErrorDelaySeconds = 5,
            ProcessingErrorDelaySeconds = 1,
            ProcessTimeoutSeconds = 30,
            MaxConsecutiveDlqFailures = 0
        };

        var validator = new OutboxOptionsValidator();
        var result = validator.Validate(options);

        Assert.False(result.IsValid);
    }

    [Fact]
    public void MaxConsecutiveDlqFailures_Default_Passes()
    {
        var options = new OutboxOptions
        {
            KafkaConsumer = ValidConsumer,
            KafkaDlqProducer = ValidProducer,
            SetupRetryDelaySeconds = 10,
            ConsumeErrorDelaySeconds = 5,
            ProcessingErrorDelaySeconds = 1,
            ProcessTimeoutSeconds = 30
        };

        var validator = new OutboxOptionsValidator();
        var result = validator.Validate(options);

        Assert.True(result.IsValid);
        Assert.Equal(5, options.MaxConsecutiveDlqFailures);
    }

    [Fact]
    public void LagThresholdMinutes_Zero_Fails()
    {
        var options = new OutboxOptions
        {
            KafkaConsumer = ValidConsumer,
            KafkaDlqProducer = ValidProducer,
            SetupRetryDelaySeconds = 10,
            ConsumeErrorDelaySeconds = 5,
            ProcessingErrorDelaySeconds = 1,
            ProcessTimeoutSeconds = 30,
            LagThresholdMinutes = 0
        };

        var validator = new OutboxOptionsValidator();
        var result = validator.Validate(options);

        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.PropertyName == "LagThresholdMinutes");
    }

    [Fact]
    public void LagThresholdMinutes_Default_Passes()
    {
        var options = new OutboxOptions
        {
            KafkaConsumer = ValidConsumer,
            KafkaDlqProducer = ValidProducer,
            SetupRetryDelaySeconds = 10,
            ConsumeErrorDelaySeconds = 5,
            ProcessingErrorDelaySeconds = 1,
            ProcessTimeoutSeconds = 30
        };

        var validator = new OutboxOptionsValidator();
        var result = validator.Validate(options);

        Assert.True(result.IsValid);
        Assert.Equal(5, options.LagThresholdMinutes);
    }
}
