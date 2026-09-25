using Common.Application.Validation;
using FluentValidation;

namespace Common.Application.Options;

public class CustomRateLimitingOptions
{
    public required FixedWindow Global { get; set; }

    public required FixedWindow Sms { get; set; }

    public required FixedWindow Register { get; set; }

    public required FixedWindow CreateStore { get; set; }

    public required FixedWindow TokenCreate { get; set; }

    public required FixedWindow CheckRegistration { get; set; }

    public required FixedWindow TokenRefresh { get; set; }

    public required FixedWindow Email { get; set; }

    public required FixedWindow OtpVerify { get; set; }

    /// <summary>
    /// Path prefixes the global limiter never applies to. Rate-limiting a persistent connection (e.g.
    /// a SignalR hub) is meaningless: reconnect storms and long-polling fallback would otherwise 429.
    /// </summary>
    public IReadOnlyList<string> ExemptPathPrefixes { get; set; } = [];
}

public class FixedWindow
{
    public required int Limit { get; set; }

    public required double PeriodInMs { get; set; }

    /// <summary>Only honored by the in-process (non-Redis) limiter; inert once Redis-backed.</summary>
    public required int? QueueLimit { get; set; }

    /// <summary>
    /// When Redis-backed and Redis is unreachable: true lets the request through, false
    /// rejects it. Sensitive flows (Sms, OtpVerify, BookingSubmit, BookingOtpVerify) set this false,
    /// during a Redis outage those flows are already dead anyway, since RedisOtpService is the OTP store.
    /// </summary>
    public required bool? FailOpen { get; set; }
}

public class CustomRateLimitingOptionsValidator : CustomValidator<CustomRateLimitingOptions>
{
    public CustomRateLimitingOptionsValidator()
    {
#pragma warning disable CS8620
        RuleFor(o => o.Global)
            .SetValidator(new FixedWindowValidator());

        RuleFor(o => o.Sms)
            .SetValidator(new FixedWindowValidator());

        RuleFor(o => o.Register)
            .SetValidator(new FixedWindowValidator());

        RuleFor(o => o.CreateStore)
            .SetValidator(new FixedWindowValidator());

        RuleFor(o => o.TokenCreate)
            .SetValidator(new FixedWindowValidator());

        RuleFor(o => o.CheckRegistration)
            .SetValidator(new FixedWindowValidator());

        RuleFor(o => o.TokenRefresh)
            .SetValidator(new FixedWindowValidator());

        RuleFor(o => o.Email)
            .SetValidator(new FixedWindowValidator());

        RuleFor(o => o.OtpVerify)
            .SetValidator(new FixedWindowValidator());
#pragma warning restore CS8620
    }
}

public class FixedWindowValidator : CustomValidator<FixedWindow>
{
    public FixedWindowValidator()
    {
        RuleFor(o => o.Limit)
            .NotEmpty()
            .WithMessage("Limit must not be empty.");

        RuleFor(o => o.PeriodInMs)
            .NotEmpty()
            .WithMessage("PeriodInMs must not be empty.");

        RuleFor(o => o.QueueLimit)
            .NotNull()
            .WithMessage("QueueLimit is required.")
            .GreaterThanOrEqualTo(0)
            .WithMessage("QueueLimit must be greater than or equal to 0.");

        RuleFor(o => o.FailOpen)
            .NotNull()
            .WithMessage("FailOpen is required.");
    }
}
