using Common.Core.Contracts.Results;
using FluentValidation;
using IdentityAndAuth.Features.Common.Validations;
using Microsoft.Extensions.Localization;
using NimbleMediator.Contracts;

namespace IdentityAndAuth.Features.Tokens.UseCases.Create;

public sealed record Request(string PhoneVerificationToken, string PhoneNumber) : IRequest<Result<Response>>;

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
