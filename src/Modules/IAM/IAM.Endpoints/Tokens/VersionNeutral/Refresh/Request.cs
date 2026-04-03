using Common.Application.Localization.Resources;
using Common.Application.Validation;
using FluentValidation;

namespace IAM.Endpoints.Tokens.VersionNeutral.Refresh;

public sealed record Request
{
    public required string RefreshToken { get; init; }
}

public sealed class RequestValidator : CustomValidator<Request>
{
    public RequestValidator(IResxLocalizer localizer)
    {
        RuleFor(x => x.RefreshToken)
            .NotEmpty()
            .WithMessage(localizer.Tokens_Refresh_RefreshToken_NotEmpty);
    }
}
