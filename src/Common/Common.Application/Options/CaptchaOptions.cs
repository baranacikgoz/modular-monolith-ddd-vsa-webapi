using Common.Application.Validation;
using FluentValidation;

namespace Common.Application.Options;

public class CaptchaOptions
{
    public required string BaseUrl { get; init; }

    public required string CaptchaEndpoint { get; init; }

    public required string ClientKey { get; init; }

    public required string SecretKey { get; init; }

    /// <summary>
    ///     reCAPTCHA v3 score threshold. Requests with a score below this value are rejected.
    ///     Valid range: 0.0 to 1.0. Defaults to 0.5 if not set or zero.
    /// </summary>
    public double ScoreThreshold { get; init; }

    /// <summary>"Dummy" (non-production only) or "ReCaptcha".</summary>
    public required string Provider { get; init; }

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

        RuleFor(o => o.Provider)
            .Must(p => p is "Dummy" or "ReCaptcha")
            .WithMessage("Provider must be 'Dummy' or 'ReCaptcha'.");

        RuleFor(o => o.AttemptTimeoutSeconds)
            .GreaterThan(0)
            .WithMessage("AttemptTimeoutSeconds must be greater than 0.");

        RuleFor(o => o.TotalRequestTimeoutSeconds)
            .GreaterThanOrEqualTo(o => o.AttemptTimeoutSeconds)
            .WithMessage("TotalRequestTimeoutSeconds must be greater than or equal to AttemptTimeoutSeconds.");
    }
}
