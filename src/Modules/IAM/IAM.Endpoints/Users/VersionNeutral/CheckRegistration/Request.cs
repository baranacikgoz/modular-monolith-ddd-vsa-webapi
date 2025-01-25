using Common.Application.Localization;
using Common.Application.Validation;
using FluentValidation;
using IAM.Application.Common.Validations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace IAM.Endpoints.Users.VersionNeutral.CheckRegistration;

public sealed record Request
{
    [FromQuery]
    public required string PhoneNumber { get; init; }
}

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
    }
}
