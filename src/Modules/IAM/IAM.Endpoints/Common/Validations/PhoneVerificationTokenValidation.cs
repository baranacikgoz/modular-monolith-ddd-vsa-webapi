using Common.Application.Localization;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace IAM.Endpoints.Common.Validations;

public static partial class CommonValidations
{
    public static IRuleBuilderOptions<T, string> PhoneVerificationTokenValidation<T>(
        this IRuleBuilder<T, string> ruleBuilder, IStringLocalizer<ResxLocalizer> localizer)
    {
        return ruleBuilder
            .NotEmpty()
            .WithMessage(localizer["Telefon doğrulama tokeni boş olamaz."]);
    }
}
