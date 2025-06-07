using Common.Application.Localization;
using Common.Application.Validation;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace IAM.Endpoints.Tokens.VersionNeutral.Refresh;

public sealed record Request
{
    public required string RefreshToken { get; init; }
}

public sealed class RequestValidator : CustomValidator<Request>
{
    public RequestValidator(IStringLocalizer<ResxLocalizer> localizer)
    {
        RuleFor(x => x.RefreshToken)
            .NotEmpty()
            .WithMessage(localizer["Tokens.Refresh.RefreshToken.NotEmpty"]);
    }
}
