using Common.Application.Validation;
using FluentValidation;

namespace Common.Application.Options;

public class SignalROptions
{
    public bool UseRedisBackplane { get; set; }
    public required string RedisConnectionString { get; set; }

    /// <summary>
    ///     Explicit opt-out for single-instance production deployments, the SignalR counterpart of
    ///     <see cref="CachingOptions.AllowInMemoryOnlyInProduction"/>. Plain default (true): added after the
    ///     file was deployed, so it must not be required.
    /// </summary>
    public bool RequireRedisBackplaneInProduction { get; set; } = true;
}

public class SignalROptionsValidator : CustomValidator<SignalROptions>
{
    public SignalROptionsValidator()
    {
        RuleFor(x => x.RedisConnectionString)
            .NotEmpty()
            .WithMessage("RedisConnectionString is required when UseRedisBackplane is enabled.")
            .When(x => x.UseRedisBackplane);

        // Multi-instance deployments require the Redis backplane for SignalR fan-out. Validated at startup by
        // AddCommonOptions, so a Production boot without the backplane fails unless the guard is switched off.
        RuleFor(x => x.UseRedisBackplane)
            .Must((options, useRedisBackplane, context) =>
                !context.IsProduction() || useRedisBackplane || !options.RequireRedisBackplaneInProduction)
            .WithMessage(
                $"{nameof(SignalROptions)}.{nameof(SignalROptions.UseRedisBackplane)} is false in Production. " +
                "Multi-instance deployments require the Redis backplane for SignalR fan-out. " +
                $"Set {nameof(SignalROptions.RequireRedisBackplaneInProduction)} = false only for single-instance deployments.");
    }
}
