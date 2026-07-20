using Common.Application.Localization.Resources;
using Common.Application.ModelBinders;
using Common.Application.Validation;
using FluentValidation;
using IAM.Domain.Identity.Sessions;
using Microsoft.AspNetCore.Mvc;

namespace IAM.Endpoints.Tokens.VersionNeutral.Sessions.Revoke;

public sealed record Request
{
    [FromRoute]
    [ModelBinder<StronglyTypedIdBinder<SessionId>>]
    public required SessionId Id { get; init; }
}

public sealed class RequestValidator : CustomValidator<Request>
{
    public RequestValidator(IResxLocalizer localizer)
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage(localizer.Tokens_Sessions_Revoke_Id_NotEmpty);
    }
}
