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
    public required string Name { get; init; }
    public required string LastName { get; init; }
    public required string NationalIdentityNumber { get; init; }
    public required string BirthDate { get; init; }
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

        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage(localizer.Register_Name_NotEmpty);

        RuleFor(x => x.Name)
            .Must(str => str.ContainsOnlyTurkishCharacters(true))
            .WithMessage(localizer.Register_Name_ContainsOnlyTurkishCharacters)
            .MaximumLength(Constants.NameMaxLength)
            .WithMessage(string.Format(CultureInfo.CurrentCulture, localizer.Register_Name_MaxLength,
                Constants.NameMaxLength))
            .When(x => !string.IsNullOrWhiteSpace(x.Name));

        RuleFor(x => x.LastName)
            .NotEmpty()
            .WithMessage(localizer.Register_LastName_NotEmpty);

        RuleFor(x => x.LastName)
            .Must(str => str.ContainsOnlyTurkishCharacters(false))
            .WithMessage(localizer.Register_LastName_ContainsOnlyTurkishCharacters)
            .MaximumLength(Constants.LastNameMaxLength)
            .WithMessage(string.Format(CultureInfo.CurrentCulture, localizer.Register_LastName_MaxLength,
                Constants.LastNameMaxLength))
            .When(x => !string.IsNullOrWhiteSpace(x.LastName));

        RuleFor(x => x.NationalIdentityNumber)
            .NotEmpty()
            .WithMessage(localizer.Register_NationalIdentityNumber_NotEmpty);

        RuleFor(x => x.NationalIdentityNumber)
            .Length(Constants.NationalIdentityNumberLength)
            .WithMessage(string.Format(CultureInfo.CurrentCulture, localizer.Register_NationalIdentityNumber_Length,
                Constants.NationalIdentityNumberLength))
            .Must(str => str.All(char.IsDigit))
            .WithMessage(localizer.Register_NationalIdentityNumber_ContainsOnlyDigits)
            .When(x => !string.IsNullOrWhiteSpace(x.NationalIdentityNumber));

        RuleFor(x => x.BirthDate)
            .NotEmpty()
            .WithMessage(localizer.Register_BirthDate_NotEmpty);

        RuleFor(x => x.BirthDate)
            .Must(str => DateOnly.TryParseExact(str, Domain.Constants.TurkishDateFormat, CultureInfo.InvariantCulture,
                DateTimeStyles.None, out _))
            .WithMessage(string.Format(CultureInfo.CurrentCulture, localizer.Register_BirthDate_Format,
                Domain.Constants.TurkishDateFormat))
            .When(x => !string.IsNullOrWhiteSpace(x.BirthDate));
    }
}
