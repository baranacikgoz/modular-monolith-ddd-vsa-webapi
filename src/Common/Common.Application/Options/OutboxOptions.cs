using Common.Application.Validation;
using FluentValidation;

namespace Common.Application.Options;

public class OutboxOptions
{
    public required int PollIntervalMs { get; set; }
    public required int BatchSize { get; set; }
    public required int MaxRetryCount { get; set; }
    public required bool IsProcessor { get; set; }

    public required int BaseBackoffSeconds { get; set; }
    public required int MaxBackoffSeconds { get; set; }

    public required int LagThresholdMinutes { get; set; }
    public required string MetricsCronSchedule { get; set; }

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
        RuleFor(o => o.PollIntervalMs)
            .GreaterThanOrEqualTo(100)
            .WithMessage("PollIntervalMs must be at least 100.");

        RuleFor(o => o.BatchSize)
            .InclusiveBetween(1, 1000)
            .WithMessage("BatchSize must be between 1 and 1000.");

        RuleFor(o => o.MaxRetryCount)
            .GreaterThanOrEqualTo(1)
            .WithMessage("MaxRetryCount must be at least 1.");

        RuleFor(o => o.BaseBackoffSeconds)
            .GreaterThanOrEqualTo(1)
            .WithMessage("BaseBackoffSeconds must be at least 1.");

        RuleFor(o => o.MaxBackoffSeconds)
            .GreaterThanOrEqualTo(1)
            .WithMessage("MaxBackoffSeconds must be at least 1.")
            .GreaterThanOrEqualTo(o => o.BaseBackoffSeconds)
            .WithMessage("MaxBackoffSeconds must be >= BaseBackoffSeconds.");

        RuleFor(o => o.LagThresholdMinutes)
            .GreaterThanOrEqualTo(1)
            .WithMessage("LagThresholdMinutes must be at least 1.");

        RuleFor(o => o.MetricsCronSchedule)
            .NotEmpty()
            .WithMessage("MetricsCronSchedule is required.");
    }
}
