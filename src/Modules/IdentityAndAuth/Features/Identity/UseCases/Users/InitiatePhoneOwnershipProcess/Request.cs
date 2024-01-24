using Common.Core.Contracts.Results;
using Common.Core.Validation;
using Common.Localization;
using FluentValidation;
using IdentityAndAuth.Features.Common.Validations;
using Microsoft.Extensions.Localization;
using NimbleMediator.Contracts;

namespace IdentityAndAuth.Features.Identity.UseCases.Users.InitiatePhoneOwnershipProcess;
public sealed record Request(string PhoneNumber);

public class RequestValidator : ResilientValidator<Request>
{
    public RequestValidator(IStringLocalizer<ResxLocalizer> localizer)
    {
        RuleFor(x => x.PhoneNumber)
            .PhoneNumberValidation(localizer);
    }
}
