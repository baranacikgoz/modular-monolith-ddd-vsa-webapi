using Common.Application.Validation;
using FluentValidation;

namespace Common.Application.Options;

public enum CaptchaProvider
{
    /// <summary>No-op, always passes. Non-production only.</summary>
    Dummy,
    ReCaptcha
}

public class CaptchaOptions
{
    public required string BaseUrl { get; init; }

    public required string CaptchaEndpoint { get; init; }

    public required string ClientKey { get; init; }

    public required string SecretKey { get; init; }

    /// <summary>
    ///     reCAPTCHA v3 score threshold. Requests with a score below this value are rejected.
    ///     Valid range: greater than 0.0 up to 1.0.
    /// </summary>
    public required double? ScoreThreshold { get; init; }

    public required CaptchaProvider Provider { get; init; }

    /// <summary>Per-attempt timeout for the resilient HTTP client calling the captcha provider.</summary>
    public required int AttemptTimeoutSeconds { get; init; }

    /// <summary>Total timeout across all retry attempts for a single captcha validation call.</summary>
    public required int TotalRequestTimeoutSeconds { get; init; }
}

public class CaptchaOptionsValidator : CustomValidator<CaptchaOptions>
{
    public CaptchaOptionsValidator()
    {
        RuleFor(o => o.BaseUrl)
            .NotEmpty()
            .WithMessage("BaseUrl must not be empty.")
            .Must(x => Uri.TryCreate(x, UriKind.Absolute, out _))
            .WithMessage("BaseUrl must be a valid URL.");

        RuleFor(o => o.CaptchaEndpoint)
            .NotEmpty()
            .WithMessage("CaptchaEndpoint must not be empty.");

        RuleFor(o => o.ClientKey)
            .NotEmpty()
            .WithMessage("ClientKey must not be empty.");

        RuleFor(o => o.SecretKey)
            .NotEmpty()
            .WithMessage("SecretKey must not be empty.");

        // Dummy captcha always passes; set Provider to 'ReCaptcha' (with real keys) before deploying.
        RuleFor(o => o.Provider)
            .Must((_, provider, context) => !context.IsProduction() || provider != CaptchaProvider.Dummy)
            .WithMessage("CaptchaOptions.Provider is 'Dummy' in Production. Dummy captcha always passes; " +
                         "set Provider to 'ReCaptcha' (with real keys) before deploying.");

        RuleFor(o => o.ScoreThreshold)
            .NotNull()
            .WithMessage("ScoreThreshold is required.")
            .GreaterThan(0.0)
            .LessThanOrEqualTo(1.0)
            .WithMessage("ScoreThreshold must be greater than 0 and at most 1.");

        RuleFor(o => o.AttemptTimeoutSeconds)
            .GreaterThan(0)
            .WithMessage("AttemptTimeoutSeconds must be greater than 0.");

        RuleFor(o => o.TotalRequestTimeoutSeconds)
            .GreaterThanOrEqualTo(o => o.AttemptTimeoutSeconds)
            .WithMessage("TotalRequestTimeoutSeconds must be greater than or equal to AttemptTimeoutSeconds.");
    }
}
