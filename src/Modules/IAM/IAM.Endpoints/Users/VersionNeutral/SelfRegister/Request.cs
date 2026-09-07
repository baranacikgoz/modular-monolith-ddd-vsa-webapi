using System.Globalization;
using Common.Application.Localization.Resources;
using Common.Application.Options;
using Common.Application.Validation;
using Common.Domain.Devices;
using Common.Domain.Extensions;
using FluentValidation;
using IAM.Domain.Users;
using IAM.Endpoints.Common.Validations;
using Microsoft.Extensions.Options;

namespace IAM.Endpoints.Users.VersionNeutral.SelfRegister;

public sealed record Request
{
    public required string PhoneNumber { get; init; }
    public required string Otp { get; init; }
    public required string FirstName { get; init; }
    public required string LastName { get; init; }
    public required string BirthDate { get; init; }
    public string? CaptchaToken { get; init; }
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
            .WithMessage(localizer.Register_PhoneNumber_NotEmpty);

        RuleFor(x => x.PhoneNumber)
            .PhoneNumberValidation(localizer)
            .When(x => !string.IsNullOrWhiteSpace(x.PhoneNumber));

        RuleFor(x => x.Otp)
            .NotEmpty()
            .WithMessage(localizer.Users_Tokens_Create_Otp_NotEmpty);

        RuleFor(x => x.FirstName)
            .NotEmpty()
            .WithMessage(localizer.Register_FirstName_NotEmpty);

        RuleFor(x => x.FirstName)
            .Must(str => str.ContainsOnlyTurkishCharacters(true))
            .WithMessage(localizer.Register_FirstName_ContainsOnlyTurkishCharacters)
            .MaximumLength(Constants.NameMaxLength)
            .WithMessage(string.Format(CultureInfo.CurrentCulture, localizer.Register_FirstName_MaxLength,
                Constants.NameMaxLength))
            .When(x => !string.IsNullOrWhiteSpace(x.FirstName));

        RuleFor(x => x.LastName)
            .NotEmpty()
            .WithMessage(localizer.Register_LastName_NotEmpty);

        RuleFor(x => x.LastName)
            .Must(str => str.ContainsOnlyTurkishCharacters(true))
            .WithMessage(localizer.Register_LastName_ContainsOnlyTurkishCharacters)
            .MaximumLength(Constants.NameMaxLength)
            .WithMessage(string.Format(CultureInfo.CurrentCulture, localizer.Register_LastName_MaxLength,
                Constants.NameMaxLength))
            .When(x => !string.IsNullOrWhiteSpace(x.LastName));

        RuleFor(x => x.BirthDate)
            .NotEmpty()
            .WithMessage(localizer.Register_BirthDate_NotEmpty);

        RuleFor(x => x.BirthDate)
            .Must(str => DateOnly.TryParseExact(str, Domain.Constants.TurkishDateFormat, CultureInfo.InvariantCulture,
                DateTimeStyles.None, out _))
            .WithMessage(string.Format(CultureInfo.CurrentCulture, localizer.Register_BirthDate_Format,
                Domain.Constants.TurkishDateFormat))
            .When(x => !string.IsNullOrWhiteSpace(x.BirthDate));

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
