using Common.Core.Contracts.Results;
using NimbleMediator.Contracts;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace IdentityAndAuth.Features.Tokens.Refresh;

public sealed record Request(string ExpiredAccessToken, string RefreshToken) : IRequest<Result<Response>>;

public sealed class RequestValidator : AbstractValidator<Request>
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
