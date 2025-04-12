using Common.Application.Validation;
using FluentValidation;

namespace Common.Application.Options;

// Use this class per consumer
public class KafkaConsumer
{
    public required string BootstrapServers { get; set; }
    public required string GroupId { get; set; }
    public required string TopicName { get; set; }
    public required string AutoOffsetReset { get; set; }

    // How long the consumer can be out of contact before being considered dead.
    public required int SessionTimeoutMs { get; set; }
    // Controls how often consumer heartbeats to broker. Lower values detect failures faster.
    public required int HeartbeatIntervalMs { get; set; }
    // Optional: Max time between Consume calls before considered failed. Needs coordination with processing time.
    // public required int? MaxPollIntervalMs { get; set; } = 300000; // 5 minutes

    // Set to true if you want Consume to return null for partition EOF events
    // Useful for knowing when you've caught up, but adds noise if you don't need it.
    public required bool EnablePartitionEof { get; set; }
}

public class KafkaConsumerValidator : CustomValidator<KafkaConsumer>
{
    private static readonly HashSet<string> _allowedAutoOffsetResets = new(StringComparer.Ordinal) { "Latest", "Earliest", "Error" };
    public KafkaConsumerValidator()
    {
        RuleFor(x => x.BootstrapServers)
            .NotEmpty()
            .WithMessage("BootstrapServers is required");

        RuleFor(x => x.GroupId)
            .NotEmpty()
            .WithMessage("GroupId is required");

        RuleFor(x => x.TopicName)
            .NotEmpty()
            .WithMessage("TopicName is required");

        RuleFor(x => x.AutoOffsetReset)
            .NotEmpty()
            .WithMessage("AutoOffsetReset is required")
            .Must(_allowedAutoOffsetResets.Contains)
            .WithMessage("AutoOffsetReset is invalid");

        RuleFor(x => x.SessionTimeoutMs)
            .NotEmpty()
            .WithMessage("SessionTimeoutMs is required");

        RuleFor(x => x.HeartbeatIntervalMs)
            .NotEmpty()
            .WithMessage("HeartbeatIntervalMs is required");
    }
}
