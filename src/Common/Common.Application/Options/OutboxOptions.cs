using Common.Application.Validation;
using FluentValidation;

namespace Common.Application.Options;

public class OutboxOptions
{
    public required KafkaConsumer KafkaConsumer { get; set; }
    public required KafkaProducer KafkaDlqProducer { get; set; }
    public required int SetupRetryDelaySeconds { get; set; }
    public required int ConsumeErrorDelaySeconds { get; set; }
    public required int ProcessingErrorDelaySeconds { get; set; }
    public required int ProcessTimeoutSeconds { get; set; }
    public int? ProcessingErrorMaxRetryCount { get; set; }
    public int MaxConsecutiveDlqFailures { get; set; } = 5;

    public int LagThresholdMinutes { get; set; } = 5;
    public string LagCronSchedule { get; set; } = "*/5 * * * *";

    public OutboxCleanupSettings Cleanup { get; set; } = new();
}

public class OutboxCleanupSettings
{
    public bool Enabled { get; set; } = true;
    public int RetentionDays { get; set; } = 7;
    public int BatchSize { get; set; } = 1000;
    public string CronSchedule { get; set; } = "0 3 * * *";
}

public class OutboxCleanupSettingsValidator : CustomValidator<OutboxCleanupSettings>
{
    public OutboxCleanupSettingsValidator()
    {
        RuleFor(x => x.RetentionDays)
            .GreaterThanOrEqualTo(1)
            .WithMessage("RetentionDays must be at least 1.");

        RuleFor(x => x.BatchSize)
            .GreaterThanOrEqualTo(100)
            .WithMessage("BatchSize must be at least 100.");

        RuleFor(x => x.CronSchedule)
            .NotEmpty()
            .WithMessage("CronSchedule is required.");
    }
}

public class OutboxOptionsValidator : CustomValidator<OutboxOptions>
{
    public OutboxOptionsValidator()
    {
        RuleFor(o => o.KafkaConsumer)
            .SetValidator(new KafkaConsumerValidator());

        RuleFor(o => o.KafkaDlqProducer)
            .SetValidator(new KafkaProducerValidator());

        RuleFor(o => o.SetupRetryDelaySeconds)
            .GreaterThanOrEqualTo(1)
            .WithMessage("SetupRetryDelaySeconds must be at least 1.");

        RuleFor(o => o.ConsumeErrorDelaySeconds)
            .GreaterThanOrEqualTo(1)
            .WithMessage("ConsumeErrorDelaySeconds must be at least 1.");

        RuleFor(o => o.ProcessingErrorDelaySeconds)
            .GreaterThanOrEqualTo(0) // Allow 0 for immediate retry, though >=1 is often better
            .WithMessage("ProcessingErrorDelaySeconds cannot be negative.");

        RuleFor(o => o.ProcessTimeoutSeconds)
            .GreaterThanOrEqualTo(1)
            .WithMessage("ProcessTimeoutSeconds must be at least 1 to prevent hanging handlers.");

        RuleFor(o => o.MaxConsecutiveDlqFailures)
            .GreaterThanOrEqualTo(1)
            .WithMessage("MaxConsecutiveDlqFailures must be at least 1.");

        RuleFor(o => o.LagThresholdMinutes)
            .GreaterThanOrEqualTo(1)
            .WithMessage("LagThresholdMinutes must be at least 1.");

        RuleFor(o => o.LagCronSchedule)
            .NotEmpty()
            .WithMessage("LagCronSchedule is required.");

        RuleFor(o => o)
            .Must(o => o.ProcessTimeoutSeconds * 1000L < o.KafkaConsumer.MaxPollIntervalMs)
            .WithMessage("ProcessTimeoutSeconds must be less than MaxPollIntervalMs / 1000.");
    }
}
