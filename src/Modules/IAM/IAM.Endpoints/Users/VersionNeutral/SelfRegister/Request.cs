using System.Globalization;
using Common.Application.Localization.Resources;
using Common.Application.Validation;
using Common.Domain.Extensions;
using FluentValidation;
using IAM.Domain.Identity;
using IAM.Endpoints.Common.Validations;

namespace IAM.Endpoints.Users.VersionNeutral.SelfRegister;

public sealed record Request
{
    public required string PhoneNumber { get; init; }
    public required string Otp { get; init; }
    public required string FullName { get; init; }
    public required string BirthDate { get; init; }
    public required string CaptchaToken { get; init; }
}

public sealed class RequestValidator : CustomValidator<Request>
{
    public RequestValidator(IResxLocalizer localizer)
    {
        RuleFor(x => x.PhoneNumber)
            .NotEmpty()
            .WithMessage(localizer.Register_PhoneNumber_NotEmpty);

        RuleFor(x => x.PhoneNumber)
            .PhoneNumberValidation(localizer)
            .When(x => !string.IsNullOrWhiteSpace(x.PhoneNumber));

        RuleFor(x => x.FullName)
            .NotEmpty()
            .WithMessage(localizer.Register_FullName_NotEmpty);

        RuleFor(x => x.FullName)
            .Must(str => str.ContainsOnlyTurkishCharacters(true))
            .WithMessage(localizer.Register_FullName_ContainsOnlyTurkishCharacters)
            .MaximumLength(Constants.FullNameMaxLength)
            .WithMessage(string.Format(CultureInfo.CurrentCulture, localizer.Register_FullName_MaxLength,
                Constants.FullNameMaxLength))
            .When(x => !string.IsNullOrWhiteSpace(x.FullName));

        RuleFor(x => x.BirthDate)
            .NotEmpty()
            .WithMessage(localizer.Register_BirthDate_NotEmpty);

        RuleFor(x => x.BirthDate)
            .Must(str => DateOnly.TryParseExact(str, Domain.Constants.TurkishDateFormat, CultureInfo.InvariantCulture,
                DateTimeStyles.None, out _))
            .WithMessage(string.Format(CultureInfo.CurrentCulture, localizer.Register_BirthDate_Format,
                Domain.Constants.TurkishDateFormat))
            .When(x => !string.IsNullOrWhiteSpace(x.BirthDate));

        RuleFor(x => x.CaptchaToken)
            .NotEmpty()
            .WithMessage(localizer.IAM_CaptchaToken_NotEmpty);
    }
}
