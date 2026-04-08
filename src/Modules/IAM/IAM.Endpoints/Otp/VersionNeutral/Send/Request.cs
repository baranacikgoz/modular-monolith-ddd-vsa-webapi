using Common.Application.Localization.Resources;
using Common.Application.Validation;
using FluentValidation;
using IAM.Endpoints.Common.Validations;

namespace IAM.Endpoints.Otp.VersionNeutral.Send;

public sealed record Request
{
    public required string PhoneNumber { get; init; }

    /// <summary>
    ///     Optional captcha token. When provided the server validates it against the captcha provider
    ///     before generating and sending the OTP. Omit during environments where captcha is not configured.
    /// </summary>
    public string? CaptchaToken { get; init; }
}

public sealed class RequestValidator : CustomValidator<Request>
{
    public RequestValidator(IResxLocalizer localizer)
    {
        RuleFor(x => x.PhoneNumber)
            .NotEmpty()
            .WithMessage(localizer.Users_OTP_Store_PhoneNumber_NotEmpty);

        RuleFor(x => x.PhoneNumber)
            .PhoneNumberValidation(localizer)
            .When(x => !string.IsNullOrWhiteSpace(x.PhoneNumber));
    }
}
