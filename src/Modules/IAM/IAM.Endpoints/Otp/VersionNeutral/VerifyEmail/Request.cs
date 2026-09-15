using Common.Application.Localization.Resources;
using Common.Application.Validation;
using FluentValidation;

namespace IAM.Endpoints.Otp.VersionNeutral.VerifyEmail;

public sealed record Request
{
    public required string Email { get; init; }
    public required string Otp { get; init; }
}

public sealed class RequestValidator : CustomValidator<Request>
{
    public RequestValidator(IResxLocalizer localizer)
    {
        RuleFor(x => x.Email)
            .NotEmpty()
            .WithMessage(localizer.Otp_VerifyEmail_Email_NotEmpty)
            .EmailAddress()
            .WithMessage(localizer.Otp_VerifyEmail_Email_Invalid);

        RuleFor(x => x.Otp)
            .NotEmpty()
            .WithMessage(localizer.Otp_VerifyEmail_Otp_NotEmpty);
    }
}
