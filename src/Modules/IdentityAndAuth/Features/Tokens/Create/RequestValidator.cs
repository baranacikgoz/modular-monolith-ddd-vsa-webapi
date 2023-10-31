using FluentValidation;
using Microsoft.Extensions.Localization;
using IdentityAndAuth.Features.Common.Validations;

namespace IdentityAndAuth.Features.Tokens.Create;

public sealed class RequestValidator : AbstractValidator<Request>
{
    public RequestValidator(IStringLocalizer<RequestValidator> localizer)
    {
        RuleFor(x => x.PhoneVerificationToken)
            .PhoneVerificationTokenValidation(localizer);

        RuleFor(x => x.PhoneNumber)
            .PhoneNumberValidation(localizer);

    }
}
