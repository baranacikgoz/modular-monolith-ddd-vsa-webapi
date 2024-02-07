using Common.Core.Validation;
using Common.Localization;
using IdentityAndAuth.Features.Common.Validations;
using Microsoft.Extensions.Localization;

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
