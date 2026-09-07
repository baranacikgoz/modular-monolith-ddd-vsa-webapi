using System.Globalization;
using Common.Application.Localization.Resources;
using Common.Application.Validation;
using Common.Domain.Devices;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace IAM.Endpoints.Tokens.VersionNeutral.Sessions.Revoke;

public sealed record Request
{
    /// <summary>Keycloak session id as returned by the session list.</summary>
    [FromRoute] public required string Id { get; init; }
}

public sealed class RequestValidator : CustomValidator<Request>
{
    public RequestValidator(IResxLocalizer localizer)
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage(localizer.Tokens_Sessions_Revoke_Id_NotEmpty)
            .MaximumLength(DeviceSessionConstants.SessionIdMaxLength)
            .WithMessage(string.Format(CultureInfo.CurrentCulture, localizer.Tokens_Sessions_Revoke_Id_MaxLength,
                DeviceSessionConstants.SessionIdMaxLength));
    }
}
