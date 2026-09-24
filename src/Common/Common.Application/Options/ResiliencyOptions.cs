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

    /// <summary>
    ///     Named profiles for <c>KeyedResiliencePipelines</c>: one rate limiter, circuit breaker and attempt timeout per
    ///     key (a third party's per-tenant quota, one partner among many behind the same client). Plain default rather
    ///     than <c>required</c>: this class is already deployed, an environment whose config predates the field must
    ///     keep booting.
    /// </summary>
    public Dictionary<string, KeyedResilienceProfile> Keyed { get; init; } = new(StringComparer.Ordinal);
}

/// <summary>Per-key resilience settings; see <see cref="ResiliencyOptions.Keyed" />.</summary>
public class KeyedResilienceProfile
{
    /// <summary>Calls admitted per <see cref="RateLimitWindowMs" /> window.</summary>
    public required int RateLimitPermits { get; set; }

    public required int RateLimitWindowMs { get; set; }

    /// <summary>Calls allowed to wait for the next window instead of being rejected right away.</summary>
    public required int RateLimitQueueLimit { get; set; }

    public required double CircuitBreakerFailureRatio { get; set; }
    public required int CircuitBreakerMinimumThroughput { get; set; }
    public required int CircuitBreakerSamplingDurationSeconds { get; set; }
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

        RuleForEach(o => o.Keyed)
            .Must(pair => !string.IsNullOrWhiteSpace(pair.Key))
            .WithMessage("Keyed profile names must not be empty.")
            .SetValidator(new KeyedResilienceProfileValidator());
    }
}

/// <summary>
///     Parameterless on purpose: FluentValidation's assembly scan registers every public validator in DI, and a
///     constructor parameter would make the container's startup validation fail.
/// </summary>
public class KeyedResilienceProfileValidator : AbstractValidator<KeyValuePair<string, KeyedResilienceProfile>>
{
    public KeyedResilienceProfileValidator()
    {
        RuleFor(p => p.Value.RateLimitPermits)
            .GreaterThan(0)
            .WithMessage(p => $"Keyed[{p.Key}].RateLimitPermits must be greater than 0.");

        RuleFor(p => p.Value.RateLimitWindowMs)
            .GreaterThan(0)
            .WithMessage(p => $"Keyed[{p.Key}].RateLimitWindowMs must be greater than 0.");

        RuleFor(p => p.Value.RateLimitQueueLimit)
            .GreaterThanOrEqualTo(0)
            .WithMessage(p => $"Keyed[{p.Key}].RateLimitQueueLimit must be greater than or equal to 0.");

        RuleFor(p => p.Value.CircuitBreakerFailureRatio)
            .GreaterThan(0)
            .LessThanOrEqualTo(1)
            .WithMessage(p => $"Keyed[{p.Key}].CircuitBreakerFailureRatio must be between 0 (exclusive) and 1 (inclusive).");

        RuleFor(p => p.Value.CircuitBreakerMinimumThroughput)
            .GreaterThanOrEqualTo(2)
            .WithMessage(p => $"Keyed[{p.Key}].CircuitBreakerMinimumThroughput must be at least 2.");

        RuleFor(p => p.Value.CircuitBreakerSamplingDurationSeconds)
            .GreaterThan(0)
            .WithMessage(p => $"Keyed[{p.Key}].CircuitBreakerSamplingDurationSeconds must be greater than 0.");

        RuleFor(p => p.Value.CircuitBreakerBreakDurationSeconds)
            .GreaterThan(0)
            .WithMessage(p => $"Keyed[{p.Key}].CircuitBreakerBreakDurationSeconds must be greater than 0.");

        RuleFor(p => p.Value.AttemptTimeoutSeconds)
            .GreaterThan(0)
            .WithMessage(p => $"Keyed[{p.Key}].AttemptTimeoutSeconds must be greater than 0.");
    }
}
