using Common.Application.Localization;
using Common.Application.Validation;
using FluentValidation;
using IdentityAndAuth.Application.Common.Validations;
using Microsoft.Extensions.Localization;

namespace IdentityAndAuth.Application.Identity.VersionNeutral.Users.InitiatePhoneOwnershipProcess;
public sealed record Request(string PhoneNumber);

public class RequestValidator : CustomValidator<Request>
{
    public RequestValidator(IStringLocalizer<ResxLocalizer> localizer)
    {
        RuleFor(x => x.PhoneNumber)
            .NotEmpty()
               .WithMessage(localizer["Telefon numarası boş olamaz."]);

        RuleFor(x => x.PhoneNumber)
            .PhoneNumberValidation(localizer)
        .When(x => !string.IsNullOrWhiteSpace(x.PhoneNumber));
    }
}
