using Common.Application.Localization;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace IdentityAndAuth.Application.Common.Validations;

public static partial class CommonValidations
{
    public static IRuleBuilderOptions<T, string> PhoneNumberValidation<T>(this IRuleBuilder<T, string> ruleBuilder, IStringLocalizer<ResxLocalizer> localizer)
     => ruleBuilder
            .Length(Domain.Identity.Constants.PhoneNumberLength)
                .WithMessage(localizer["Telefon numarası, alan kodu dahil {0} karakter olmalıdır.", Domain.Identity.Constants.PhoneNumberLength])
            .Must(str => str.All(char.IsDigit))
                .WithMessage(localizer["Telefon numarası sadece rakamlardan oluşmalıdır."])
            .Must(str => str.StartsWith("90", StringComparison.Ordinal))
                .WithMessage(localizer["Şuanda sadece Türkiye numaraları desteklenmektedir. Numaranız 90 ile başlamalıdır."]);

}
