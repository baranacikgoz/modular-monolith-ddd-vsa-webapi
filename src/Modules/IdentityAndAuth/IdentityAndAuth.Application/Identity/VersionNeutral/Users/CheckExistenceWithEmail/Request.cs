using Common.Application.Localization;
using Common.Application.Validation;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace IdentityAndAuth.Application.Identity.VersionNeutral.Users.CheckExistenceWithEmail;

public sealed record Request(string Email);

public class RequestValidator : CustomValidator<Request>
{
    public RequestValidator(IStringLocalizer<ResxLocalizer> localizer)
    {
        RuleFor(x => x.Email)
            .NotEmpty()
               .WithMessage(localizer["Email boş olamaz."]);

        RuleFor(x => x.Email)
            .EmailAddress()
                .WithMessage(localizer["Geçerli bir email adresi giriniz."]);
    }
}
