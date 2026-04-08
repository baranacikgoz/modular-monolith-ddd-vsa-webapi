using Common.Application.Localization.Resources;
using Common.Application.Validation;
using FluentValidation;
using IAM.Endpoints.Common.Validations;

namespace IAM.Endpoints.Otp.VersionNeutral.Send;

public sealed record Request
{
    public required string PhoneNumber { get; init; }
    public required string CaptchaToken { get; init; }
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

        RuleFor(x => x.CaptchaToken)
            .NotEmpty()
            .WithMessage(localizer.IAM_CaptchaToken_NotEmpty);
    }
}
