
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace IdentityAndAuth.Features.Common.Validations;

public static partial class CommonValidations
{
    public static IRuleBuilderOptions<T, string> OtpValidation<T>(this IRuleBuilder<T, string> ruleBuilder, IStringLocalizer localizer)
    {
        return ruleBuilder
            .NotEmpty()
                .WithMessage(localizer["OTP doğrulama kodu boş olamaz."])
            .Length(6)
                .WithMessage(localizer["OTP doğrulama kodu 6 karakter olmalıdır."]);
    }
}
