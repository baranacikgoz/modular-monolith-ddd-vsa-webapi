using System.Globalization;
using Common.Application.Localization.Resources;
using Common.Application.Options;
using Common.Application.Validation;
using Common.Domain.Devices;
using FluentValidation;
using IAM.Domain.Users;
using Microsoft.Extensions.Options;

namespace IAM.Endpoints.Tokens.VersionNeutral.CreateByEmail;

public sealed record Request
{
    public required string Email { get; init; }
    public required string Password { get; init; }
    public required Guid DeviceId { get; init; }
    public required string ClientId { get; init; }
    public string? DeviceName { get; init; }
    public string? PushToken { get; init; }
}

public sealed class RequestValidator : CustomValidator<Request>
{
    public RequestValidator(IResxLocalizer localizer, IOptions<DevicesOptions> devicesOptions)
    {
        RuleFor(x => x.Email)
            .NotEmpty()
            .WithMessage(localizer.Tokens_CreateByEmail_Email_NotEmpty)
            .EmailAddress()
            .WithMessage(localizer.Tokens_CreateByEmail_Email_Invalid)
            .MaximumLength(Constants.EmailMaxLength)
            .WithMessage(string.Format(CultureInfo.CurrentCulture, localizer.Tokens_CreateByEmail_Email_MaxLength,
                Constants.EmailMaxLength));

        RuleFor(x => x.Password)
            .NotEmpty()
            .WithMessage(localizer.Tokens_CreateByEmail_Password_NotEmpty)
            .MaximumLength(Constants.PasswordMaxLength)
            .WithMessage(string.Format(CultureInfo.CurrentCulture, localizer.Tokens_CreateByEmail_Password_MaxLength,
                Constants.PasswordMaxLength));

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
