using Common.Application.Localization.Resources;
using FluentValidation;

namespace IAM.Endpoints.Common.Validations;

public static partial class CommonValidations
{
    public static IRuleBuilderOptions<T, string> EmailVerificationTokenValidation<T>(
        this IRuleBuilder<T, string> ruleBuilder,
        IResxLocalizer localizer)
    {
        return ruleBuilder
            .NotEmpty()
            .WithMessage(localizer.IAM_EmailVerificationToken_NotEmpty);
    }
}
