using System.Globalization;
using Common.Application.Localization.Resources;
using FluentValidation;
using IAM.Domain.Identity;

namespace IAM.Endpoints.Common.Validations;

public static partial class CommonValidations
{
    public static IRuleBuilderOptions<T, string> PhoneNumberValidation<T>(
        this IRuleBuilder<T, string> ruleBuilder,
        IResxLocalizer localizer)
    {
        return ruleBuilder
            .Length(Constants.PhoneNumberLength)
            .WithMessage(string.Format(
                CultureInfo.CurrentCulture,
                localizer.IAM_PhoneNumber_Length,
                Constants.PhoneNumberLength))
            .Must(str => str.All(char.IsDigit))
            .WithMessage(localizer.IAM_PhoneNumber_DigitsOnly)
            .Must(str => str.StartsWith("90", StringComparison.Ordinal))
            .WithMessage(localizer.IAM_PhoneNumber_OnlyTurkey);
    }
}
