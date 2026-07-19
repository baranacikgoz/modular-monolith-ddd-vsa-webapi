using Common.Application.Validation;
using FluentValidation;

namespace Common.Application.Options;

public class CachingOptions
{
    public bool UseRedis { get; set; }
    public Redis? Redis { get; set; }
    public required CachingEntryDefaults EntryDefaults { get; set; }
    public required TimeSpan IdempotencyKeyDuration { get; set; }

    /// <summary>
    ///     L1 (in-process memory) bound for consumer idempotency keys. Duplicate deliveries cluster
    ///     within minutes (outbox retries, broker redelivery), so this is kept far shorter than
    ///     <see cref="IdempotencyKeyDuration"/> (the L2/Redis window) to avoid unbounded memory growth
    ///     proportional to daily event volume.
    /// </summary>
    public required TimeSpan IdempotencyL1Duration { get; set; }

    /// <summary>
    ///     Explicit opt-out for single-instance production deployments. With more than one instance,
    ///     in-memory-only caching breaks OTP verification, consumer idempotency, and SignalR fan-out.
    /// </summary>
    public bool AllowInMemoryOnlyInProduction { get; set; }
}

public class CachingEntryDefaults
{
    public required TimeSpan Duration { get; set; }
    public required TimeSpan FailSafeMaxDuration { get; set; }
    public required TimeSpan FailSafeThrottleDuration { get; set; }
    public required TimeSpan FactorySoftTimeout { get; set; }
    public required TimeSpan FactoryHardTimeout { get; set; }
}

public class Redis
{
    public required string Host { get; set; }

    public int Port { get; set; }

    public required string Password { get; set; }

    public required string AppName { get; set; }
}

public class CachingOptionsValidator : CustomValidator<CachingOptions>
{
    public CachingOptionsValidator()
    {
#pragma warning disable CS8620
        RuleFor(x => x.Redis)
            .SetValidator(new RedisValidator())
            .When(x => x.UseRedis);
#pragma warning restore CS8620

        RuleFor(x => x.EntryDefaults).NotNull();

        When(x => x.EntryDefaults is not null, () =>
        {
            RuleFor(x => x.EntryDefaults.Duration).GreaterThan(TimeSpan.Zero);
            RuleFor(x => x.EntryDefaults.FailSafeMaxDuration).GreaterThan(TimeSpan.Zero);
            RuleFor(x => x.EntryDefaults.FailSafeThrottleDuration).GreaterThan(TimeSpan.Zero);
            RuleFor(x => x.EntryDefaults.FactorySoftTimeout).GreaterThan(TimeSpan.Zero);
            RuleFor(x => x.EntryDefaults.FactoryHardTimeout).GreaterThan(TimeSpan.Zero);
        });

        RuleFor(x => x.IdempotencyKeyDuration).GreaterThan(TimeSpan.Zero);
        RuleFor(x => x.IdempotencyL1Duration).GreaterThan(TimeSpan.Zero);
    }
}

public class RedisValidator : CustomValidator<Redis>
{
    public RedisValidator()
    {
        RuleFor(x => x.Host)
            .NotEmpty()
            .WithMessage("Host should not be empty.");

        RuleFor(x => x.Port)
            .GreaterThan(0)
            .WithMessage("Port should be greater than 0.");

        RuleFor(x => x.Password)
            .NotEmpty()
            .WithMessage("Password should not be empty.");

        RuleFor(x => x.AppName)
            .NotEmpty()
            .WithMessage("AppName should not be empty.");
    }
}
