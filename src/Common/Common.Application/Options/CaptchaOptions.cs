using Common.Application.Validation;
using FluentValidation;

namespace Common.Application.Options;

public class CaptchaOptions
{
    public required string BaseUrl { get; set; }

    public required string CaptchaEndpoint { get; set; }

    public required string ClientKey { get; set; }

    public required string SecretKey { get; set; }

    /// <summary>
    /// reCAPTCHA v3 score threshold. Requests with a score below this value are rejected.
    /// Valid range: 0.0 to 1.0. Defaults to 0.5 if not set or zero.
    /// </summary>
    public double ScoreThreshold { get; set; }
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
    }
}
