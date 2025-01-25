using Common.Application.CQS;
using Common.Application.Localization;
using Common.Application.Validation;
using FluentValidation;
using IAM.Application.Common.Validations;
using Microsoft.Extensions.Localization;

namespace IAM.Application.Users.Features.CheckRegistration;

public sealed record CheckRegistrationQuery(string PhoneNumber) : IQuery<bool>;

public class CheckRegistrationQueryValidator : CustomValidator<CheckRegistrationQuery>
{
    public CheckRegistrationQueryValidator(IStringLocalizer<ResxLocalizer> localizer)
    {
        RuleFor(x => x.PhoneNumber)
            .NotEmpty()
               .WithMessage(localizer["Identity.Users.CheckRegistration.PhoneNumber.NotEmpty"]);

        RuleFor(x => x.PhoneNumber)
            .PhoneNumberValidation(localizer)
        .When(x => !string.IsNullOrWhiteSpace(x.PhoneNumber));
    }
}
