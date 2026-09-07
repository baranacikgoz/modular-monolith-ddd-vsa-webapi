using Common.Application.Localization.Resources;
using Common.Application.Validation;
using FluentValidation;
using Common.Domain.Devices;

namespace Notifications.Infrastructure.Devices.UpdateCurrentPushToken;

public sealed record Request
{
    /// <summary>FCM device token for the current session. Null clears it (the client unregistered locally).</summary>
    public string? PushToken { get; init; }
}

public sealed class RequestValidator : CustomValidator<Request>
{
    public RequestValidator(IResxLocalizer localizer)
    {
        RuleFor(x => x.PushToken)
            .MaximumLength(DeviceSessionConstants.PushTokenMaxLength)
            .WithMessage(localizer.Users_Tokens_Create_PushToken_MaxLength);
    }
}
