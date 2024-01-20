using Common.Core.Contracts.Results;
using Common.Core.Validation;
using FluentValidation;
using IdentityAndAuth.Features.Common.Validations;
using Microsoft.Extensions.Localization;
using NimbleMediator.Contracts;

namespace IdentityAndAuth.Features.Identity.UseCases.Users.ProvePhoneOwnership;

public sealed record Request(string PhoneNumber, string Otp);

public class RequestValidator : ResilientValidator<Request>
{
    public RequestValidator(IStringLocalizer<RequestValidator> localizer)
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
