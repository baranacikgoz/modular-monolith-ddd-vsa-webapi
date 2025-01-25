using Common.Application.CQS;
using Common.Application.Localization;
using Common.Application.Validation;
using FluentValidation;
using IAM.Application.Tokens.DTOs;
using Microsoft.Extensions.Localization;

namespace IAM.Application.Tokens.Features.Refresh;

public sealed record RefreshTokenCommand(string RefreshToken) : ICommand<AccessTokenDto>;

public sealed class RefreshTokenCommandValidator : CustomValidator<RefreshTokenCommand>
{
    public RefreshTokenCommandValidator(IStringLocalizer<ResxLocalizer> localizer)
    {
        RuleFor(x => x.RefreshToken)
            .NotEmpty()
            .WithMessage(localizer["Tokens.Refresh.RefreshToken.NotEmpty"]);
    }
}
