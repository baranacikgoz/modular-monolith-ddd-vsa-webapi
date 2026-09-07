using Common.Application.Localization.Resources;
using Common.Application.Options;
using Common.Application.Validation;
using Common.Domain.Devices;
using FluentValidation;
using IAM.Endpoints.Common.Validations;
using Microsoft.Extensions.Options;

namespace IAM.Endpoints.Tokens.VersionNeutral.Create;

public sealed record Request
{
    public required string PhoneNumber { get; init; }
    public required string Otp { get; init; }
    public required Guid DeviceId { get; init; }
    public required string ClientId { get; init; }
    public string? DeviceName { get; init; }
    public string? PushToken { get; init; }
}

public sealed class RequestValidator : CustomValidator<Request>
{
    public RequestValidator(IResxLocalizer localizer, IOptions<DevicesOptions> devicesOptions)
    {
        RuleFor(x => x.PhoneNumber)
            .NotEmpty()
            .WithMessage(localizer.Users_Tokens_Create_PhoneNumber_NotEmpty);
        RuleFor(x => x.PhoneNumber)
            .PhoneNumberValidation(localizer)
            .When(x => !string.IsNullOrWhiteSpace(x.PhoneNumber));

        RuleFor(x => x.Otp)
            .NotEmpty()
            .WithMessage(localizer.Users_Tokens_Create_Otp_NotEmpty);

        RuleFor(x => x.DeviceId)
            .NotEmpty()
            .WithMessage(localizer.Users_Tokens_Create_DeviceId_NotEmpty);

        RuleFor(x => x.ClientId)
            .NotEmpty()
            .WithMessage(localizer.Users_Tokens_Create_ClientId_NotEmpty)
            .Must(devicesOptions.Value.AllowedClientIds.Contains)
            .WithMessage(localizer.Users_Tokens_Create_ClientId_Invalid)
            .When(x => !string.IsNullOrWhiteSpace(x.ClientId));

        RuleFor(x => x.DeviceName)
            .MaximumLength(DeviceSessionConstants.DeviceNameMaxLength)
            .WithMessage(localizer.Users_Tokens_Create_DeviceName_MaxLength);

        RuleFor(x => x.PushToken)
            .MaximumLength(DeviceSessionConstants.PushTokenMaxLength)
            .WithMessage(localizer.Users_Tokens_Create_PushToken_MaxLength);
    }
}
