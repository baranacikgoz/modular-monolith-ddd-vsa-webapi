using Common.Application.Localization;
using Common.Application.Validation;
using FluentValidation;
using IAM.Application.Common.Validations;
using Microsoft.Extensions.Localization;

namespace IAM.Application.Tokens.VersionNeutral.Create;

public sealed record Request(string PhoneVerificationToken, string PhoneNumber);

public sealed class RequestValidator : CustomValidator<Request>
{
    public RequestValidator(IStringLocalizer<ResxLocalizer> localizer)
    {
        RuleFor(x => x.PhoneVerificationToken)
            .NotEmpty()
                .WithMessage(localizer["Telefon doğrulama tokeni boş olamaz."]);

        RuleFor(x => x.PhoneVerificationToken)
            .PhoneVerificationTokenValidation(localizer)
        .When(x => !string.IsNullOrWhiteSpace(x.PhoneVerificationToken));

        RuleFor(x => x.PhoneNumber)
            .NotEmpty()
                .WithMessage(localizer["Telefon numarası boş olamaz."]);

        RuleFor(x => x.PhoneNumber)
            .PhoneNumberValidation(localizer)
        .When(x => !string.IsNullOrWhiteSpace(x.PhoneNumber));

    }
}
