using Common.Application.Validation;
using FluentValidation;

namespace Common.Application.Options;

public class CaptchaOptions
{
    public required string BaseUrl { get; set; }

    public required string CaptchaEndpoint { get; set; }

    public required string ClientKey { get; set; }

    public required string SecretKey { get; set; }
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
