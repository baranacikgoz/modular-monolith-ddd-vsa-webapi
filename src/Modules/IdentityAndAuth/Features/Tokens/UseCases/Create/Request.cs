using Common.Core.Validation;
using IdentityAndAuth.Features.Common.Validations;
using Microsoft.Extensions.Localization;

namespace IdentityAndAuth.Features.Tokens.UseCases.Create;

public sealed record Request(string PhoneVerificationToken, string PhoneNumber);

public sealed class RequestValidator : ResilientValidator<Request>
{
    public RequestValidator(IStringLocalizer<RequestValidator> localizer)
    {
        RuleFor(x => x.PhoneVerificationToken)
            .PhoneVerificationTokenValidation(localizer);

        RuleFor(x => x.PhoneNumber)
            .PhoneNumberValidation(localizer);

    }
}
