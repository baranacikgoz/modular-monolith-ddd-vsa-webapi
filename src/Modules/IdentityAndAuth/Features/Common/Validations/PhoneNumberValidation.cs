using FluentValidation;
using IdentityAndAuth.Features.Users.Domain;
using IdentityAndAuth.Identity;
using Microsoft.Extensions.Localization;

namespace IdentityAndAuth.Features.Common.Validations;

public static partial class CommonValidations
{
    public static IRuleBuilderOptions<T, string> PhoneNumberValidation<T>(this IRuleBuilder<T, string> ruleBuilder, IStringLocalizer localizer)
    {
        return ruleBuilder
            .NotEmpty()
                .WithMessage(localizer["Telefon numarası boş olamaz."])
            .Length(ApplicationUserConstants.PhoneNumberLength)
                .WithMessage(localizer["Telefon numarası, alan kodu dahil {0} karakter olmalıdır.", ApplicationUserConstants.PhoneNumberLength])
            .Must(str => str.All(char.IsDigit))
                .WithMessage(localizer["Telefon numarası sadece rakamlardan oluşmalıdır."])
            .Must(str => str.StartsWith("90", StringComparison.Ordinal))
                .WithMessage(localizer["Şuanda sadece Türkiye numaraları desteklenmektedir. Numaranız 90 ile başlamalıdır."]);
    }
}
