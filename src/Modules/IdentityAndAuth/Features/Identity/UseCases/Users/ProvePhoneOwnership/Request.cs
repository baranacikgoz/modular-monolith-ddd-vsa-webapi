using Common.Core.Validation;
using Common.Localization;
using FluentValidation;
using IdentityAndAuth.Features.Common.Validations;
using Microsoft.Extensions.Localization;

namespace IdentityAndAuth.Features.Identity.UseCases.Users.ProvePhoneOwnership;

public sealed record Request(string PhoneNumber, string Otp);

public class RequestValidator : ResilientValidator<Request>
{
    public RequestValidator(IStringLocalizer<ResxLocalizer> localizer)
    {
        RuleFor(x => x.PhoneNumber)
            .PhoneNumberValidation(localizer);

        RuleFor(x => x.Otp)
            .NotEmpty()
                .WithMessage(localizer["OTP doğrulama kodu boş olamaz."])
            .Length(6)
                .WithMessage(localizer["OTP doğrulama kodu 6 karakter olmalıdır."]);
    }
}
