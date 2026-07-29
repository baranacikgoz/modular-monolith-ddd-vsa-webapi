using Common.Application.Localization.Resources;
using Common.Application.Validation;
using FluentValidation;
using IAM.Domain.Identity.Sessions;

namespace IAM.Endpoints.Tokens.VersionNeutral.Sessions.UpdatePushToken;

public sealed record Request
{
    public string? PushToken { get; init; }
}

public sealed class RequestValidator : CustomValidator<Request>
{
    public RequestValidator(IResxLocalizer localizer)
    {
        RuleFor(x => x.PushToken)
            .MaximumLength(Constants.PushTokenMaxLength)
            .WithMessage(localizer.Tokens_Sessions_UpdatePushToken_PushToken_MaxLength);
    }
}
