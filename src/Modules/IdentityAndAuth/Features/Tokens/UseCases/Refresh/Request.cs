using Common.Core.Contracts.Results;
using Common.Core.Validation;
using FluentValidation;
using Microsoft.Extensions.Localization;
using NimbleMediator.Contracts;

namespace IdentityAndAuth.Features.Tokens.UseCases.Refresh;

public sealed record Request(string ExpiredAccessToken, string RefreshToken);

public sealed class RequestValidator : ResilientValidator<Request>
{
    public RequestValidator(IStringLocalizer<RequestValidator> localizer)
    {
        RuleFor(x => x.RefreshToken)
            .NotEmpty()
            .WithMessage(localizer["Yenileme tokeni boş olamaz."]);

        RuleFor(x => x.ExpiredAccessToken)
            .NotEmpty()
            .WithMessage(localizer["Eski erişim tokeni boş olamaz."]);

    }
}
