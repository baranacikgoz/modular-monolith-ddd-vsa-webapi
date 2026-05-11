using Common.Application.Options;
using Xunit;

#pragma warning disable CA1515, CA1707

namespace Common.Tests;

public sealed class KafkaConsumerValidatorTests
{
    [Fact]
    public void ValidConsumer_PassesValidation()
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
            MaxPollIntervalMs = 300000
        };

        var validator = new KafkaConsumerValidator();
        var result = validator.Validate(consumer);

        Assert.True(result.IsValid);
    }

    [Fact]
    public void HeartbeatInterval_ExceedsSessionTimeoutThird_Fails()
    {
        var consumer = new KafkaConsumer
        {
            BootstrapServers = "localhost:9092",
            GroupId = "test-group",
            TopicName = "test-topic",
            AutoOffsetReset = "Earliest",
            SessionTimeoutMs = 30000,
            HeartbeatIntervalMs = 15000,
            EnablePartitionEof = false,
            MaxPollIntervalMs = 300000
        };

        var validator = new KafkaConsumerValidator();
        var result = validator.Validate(consumer);

        Assert.False(result.IsValid);
    }

    [Fact]
    public void HeartbeatInterval_WithinSessionTimeoutThird_Passes()
    {
        var consumer = new KafkaConsumer
        {
            BootstrapServers = "localhost:9092",
            GroupId = "test-group",
            TopicName = "test-topic",
            AutoOffsetReset = "Earliest",
            SessionTimeoutMs = 30000,
            HeartbeatIntervalMs = 10000,
            EnablePartitionEof = false,
            MaxPollIntervalMs = 300000
        };

        var validator = new KafkaConsumerValidator();
        var result = validator.Validate(consumer);

        Assert.True(result.IsValid);
    }
}
