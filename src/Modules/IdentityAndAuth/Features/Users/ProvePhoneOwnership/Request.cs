using Common.Core.Contracts.Results;
using NimbleMediator.Contracts;
using FluentValidation;
using IdentityAndAuth.Features.Common.Validations;
using Microsoft.Extensions.Localization;

namespace IdentityAndAuth.Features.Users.ProvePhoneOwnership;

public sealed record Request(string PhoneNumber, string Otp) : IRequest<Result<Response>>;

public class RequestValidator : AbstractValidator<Request>
{
    public RequestValidator(IStringLocalizer<RequestValidator> localizer)
    {
        RuleFor(x => x.PhoneNumber)
            .PhoneNumberValidation(localizer);

        RuleFor(x => x.Otp)
            .OtpValidation(localizer);
    }
}
