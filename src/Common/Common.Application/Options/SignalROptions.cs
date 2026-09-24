using Common.Application.Validation;
using FluentValidation;

namespace Common.Application.Options;

public class SignalROptions
{
    public bool UseRedisBackplane { get; set; }
    public required string RedisConnectionString { get; set; }

    /// <summary>
    ///     Explicit opt-out for single-instance production deployments, the SignalR counterpart of
    ///     <see cref="CachingOptions.AllowInMemoryOnlyInProduction"/>. Nullable so a missing key fails NotNull at
    ///     boot instead of binding to false and silently switching the guard off.
    /// </summary>
    public required bool? RequireRedisBackplaneInProduction { get; set; }
}

public class SignalROptionsValidator : CustomValidator<SignalROptions>
{
    public SignalROptionsValidator()
    {
        RuleFor(x => x.RedisConnectionString)
            .NotEmpty()
            .WithMessage("RedisConnectionString is required when UseRedisBackplane is enabled.")
            .When(x => x.UseRedisBackplane);

        RuleFor(x => x.RequireRedisBackplaneInProduction)
            .NotNull()
            .WithMessage("RequireRedisBackplaneInProduction is required.");

        // Multi-instance deployments require the Redis backplane for SignalR fan-out. Validated at startup by
        // AddCommonOptions, so a Production boot without the backplane fails unless the guard is switched off.
        RuleFor(x => x.UseRedisBackplane)
            .Must((options, useRedisBackplane, context) =>
                !context.IsProduction() || useRedisBackplane || options.RequireRedisBackplaneInProduction == false)
            .WithMessage(
                $"{nameof(SignalROptions)}.{nameof(SignalROptions.UseRedisBackplane)} is false in Production. " +
                "Multi-instance deployments require the Redis backplane for SignalR fan-out. " +
                $"Set {nameof(SignalROptions.RequireRedisBackplaneInProduction)} = false only for single-instance deployments.");
    }
}
