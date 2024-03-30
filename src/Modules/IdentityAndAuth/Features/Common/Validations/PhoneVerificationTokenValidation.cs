using Common.Localization;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace IdentityAndAuth.Features.Common.Validations;

public static partial class CommonValidations
{
    public static IRuleBuilderOptions<T, string> PhoneVerificationTokenValidation<T>(this IRuleBuilder<T, string> ruleBuilder, IStringLocalizer<ResxLocalizer> localizer)
        => ruleBuilder
            .NotEmpty()
                .WithMessage(localizer["Telefon doğrulama tokeni boş olamaz."]);
}
