using Common.Application.Validation;
using FluentValidation;

namespace Common.Application.Options;

public class ResiliencyOptions
{
    public required int PooledConnectionLifetimeMinutes { get; set; }
    public required int TotalRequestTimeoutSeconds { get; set; }
    public required int MaxRetryAttempts { get; set; }
    public required int RetryDelaySeconds { get; set; }
    public required int CircuitBreakerSamplingDurationSeconds { get; set; }
    public required double CircuitBreakerFailureRatio { get; set; }
    public required int CircuitBreakerMinimumThroughput { get; set; }
    public required int CircuitBreakerBreakDurationSeconds { get; set; }
    public required int AttemptTimeoutSeconds { get; set; }
}

public class ResiliencyOptionsValidator : CustomValidator<ResiliencyOptions>
{
    public ResiliencyOptionsValidator()
    {
        RuleFor(o => o.PooledConnectionLifetimeMinutes)
            .GreaterThan(0)
            .WithMessage("PooledConnectionLifetimeMinutes must be greater than 0.");

        RuleFor(o => o.TotalRequestTimeoutSeconds)
            .GreaterThan(0)
            .WithMessage("TotalRequestTimeoutSeconds must be greater than 0.");

        RuleFor(o => o.MaxRetryAttempts)
            .GreaterThanOrEqualTo(0)
            .WithMessage("MaxRetryAttempts must be greater than or equal to 0.");

        RuleFor(o => o.RetryDelaySeconds)
            .GreaterThan(0)
            .WithMessage("RetryDelaySeconds must be greater than 0.");

        RuleFor(o => o.CircuitBreakerSamplingDurationSeconds)
            .GreaterThan(0)
            .WithMessage("CircuitBreakerSamplingDurationSeconds must be greater than 0.");

        RuleFor(o => o.CircuitBreakerFailureRatio)
            .GreaterThan(0)
            .LessThanOrEqualTo(1)
            .WithMessage("CircuitBreakerFailureRatio must be between 0 (exclusive) and 1 (inclusive).");

        RuleFor(o => o.CircuitBreakerMinimumThroughput)
            .GreaterThanOrEqualTo(2)
            .WithMessage("CircuitBreakerMinimumThroughput must be at least 2.");

        RuleFor(o => o.CircuitBreakerBreakDurationSeconds)
            .GreaterThan(0)
            .WithMessage("CircuitBreakerBreakDurationSeconds must be greater than 0.");

        RuleFor(o => o.AttemptTimeoutSeconds)
            .GreaterThan(0)
            .WithMessage("AttemptTimeoutSeconds must be greater than 0.")
            .LessThan(o => o.TotalRequestTimeoutSeconds)
            .WithMessage("AttemptTimeoutSeconds must be less than TotalRequestTimeoutSeconds.");

        RuleFor(o => o.CircuitBreakerSamplingDurationSeconds)
            .GreaterThanOrEqualTo(o => 2 * o.AttemptTimeoutSeconds)
            .WithMessage("CircuitBreakerSamplingDurationSeconds must be at least 2x AttemptTimeoutSeconds.");
    }
}
