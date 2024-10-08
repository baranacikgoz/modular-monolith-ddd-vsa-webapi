using Common.Application.Localization;
using Common.Application.Validation;
using FluentValidation;
using IAM.Application.Common.Validations;
using Microsoft.Extensions.Localization;

namespace IAM.Application.Identity.VersionNeutral.Users.VerifyOtp;

public sealed record Request(string PhoneNumber, string Otp);

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

        RuleFor(x => x.Otp)
            .NotEmpty()
                .WithMessage(localizer["OTP doğrulama kodu boş olamaz."]);

        RuleFor(x => x.Otp)
            .Length(6)
                .WithMessage(localizer["OTP doğrulama kodu 6 karakter olmalıdır."])
        .When(x => !string.IsNullOrWhiteSpace(x.Otp));
    }
}
