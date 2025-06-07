using Common.Application.Localization;
using Common.Application.Validation;
using FluentValidation;
using IAM.Application.Common.Validations;
using Microsoft.Extensions.Localization;

namespace IAM.Endpoints.Tokens.VersionNeutral.Create;

public sealed record Request
{
    public required string PhoneNumber { get; init; }
    public required string Otp { get; init; }
}

public sealed class RequestValidator : CustomValidator<Request>
{
    public RequestValidator(IStringLocalizer<ResxLocalizer> localizer)
    {
        RuleFor(x => x.PhoneNumber)
            .NotEmpty()
            .WithMessage(localizer["Users.Tokens.Create.PhoneNumber.NotEmpty"]);
        RuleFor(x => x.PhoneNumber)
            .PhoneNumberValidation(localizer)
            .When(x => !string.IsNullOrWhiteSpace(x.PhoneNumber));

        RuleFor(x => x.Otp)
            .NotEmpty()
            .WithMessage(localizer["Users.Tokens.Create.Otp.NotEmpty"]);
    }
}
