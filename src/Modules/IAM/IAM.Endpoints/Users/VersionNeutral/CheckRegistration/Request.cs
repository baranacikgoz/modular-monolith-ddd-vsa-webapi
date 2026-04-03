using Common.Application.Localization.Resources;
using Common.Application.Validation;
using FluentValidation;
using IAM.Endpoints.Common.Validations;
using Microsoft.AspNetCore.Mvc;

namespace IAM.Endpoints.Users.VersionNeutral.CheckRegistration;

public sealed record Request
{
    [FromQuery] public required string PhoneNumber { get; init; }
}

public class RequestValidator : CustomValidator<Request>
{
    public RequestValidator(IResxLocalizer localizer)
    {
        RuleFor(x => x.PhoneNumber)
            .NotEmpty()
            .WithMessage(localizer.Users_CheckRegistration_PhoneNumber_NotEmpty);

        RuleFor(x => x.PhoneNumber)
            .PhoneNumberValidation(localizer)
            .When(x => !string.IsNullOrWhiteSpace(x.PhoneNumber));
    }
}
