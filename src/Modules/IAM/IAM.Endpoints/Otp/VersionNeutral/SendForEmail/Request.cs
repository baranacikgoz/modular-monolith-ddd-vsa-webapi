using Common.Application.Localization.Resources;
using Common.Application.Validation;
using FluentValidation;

namespace IAM.Endpoints.Otp.VersionNeutral.SendForEmail;

public sealed record Request
{
    public required string Email { get; init; }
    public string? CaptchaToken { get; init; }
}

public sealed class RequestValidator : CustomValidator<Request>
{
    public RequestValidator(IResxLocalizer localizer)
    {
        RuleFor(x => x.Email)
            .NotEmpty()
            .WithMessage(localizer.Otp_SendForEmail_Email_NotEmpty)
            .EmailAddress()
            .WithMessage(localizer.Otp_SendForEmail_Email_Invalid);
    }
}
