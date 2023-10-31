using FluentValidation;
using IdentityAndAuth.Features.Common.Validations;
using Microsoft.Extensions.Localization;

namespace IdentityAndAuth.Features.Users.InitiatePhoneOwnershipProcess;

public class RequestValidator : AbstractValidator<Request>
{
    public RequestValidator(IStringLocalizer<RequestValidator> localizer)
    {
        RuleFor(x => x.PhoneNumber)
            .PhoneNumberValidation(localizer);
    }
}
