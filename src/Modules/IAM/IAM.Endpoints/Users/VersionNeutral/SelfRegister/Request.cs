using System.Globalization;
using Common.Application.Localization;
using Common.Application.Validation;
using Common.Domain.Extensions;
using FluentValidation;
using IAM.Endpoints.Common.Validations;
using Microsoft.Extensions.Localization;

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
    public RequestValidator(IStringLocalizer<ResxLocalizer> localizer)
    {
        RuleFor(x => x.PhoneNumber)
            .NotEmpty()
                .WithMessage(localizer["Register.PhoneNumber.NotEmpty"]);

        RuleFor(x => x.PhoneNumber)
            .PhoneNumberValidation(localizer)
        .When(x => !string.IsNullOrWhiteSpace(x.PhoneNumber));

        RuleFor(x => x.Name)
            .NotEmpty()
                .WithMessage(localizer["Register.Name.NotEmpty"]);

        RuleFor(x => x.Name)
            .Must(str => str.ContainsOnlyTurkishCharacters(allowWhiteSpace: true))
                .WithMessage(localizer["Register.Name.ContainsOnlyTurkishCharacters"])
            .MaximumLength(Domain.Identity.Constants.NameMaxLength)
                .WithMessage(localizer["Register.Name.MaxLength {0}", Domain.Identity.Constants.NameMaxLength])
        .When(x => !string.IsNullOrWhiteSpace(x.Name));

        RuleFor(x => x.LastName)
            .NotEmpty()
                .WithMessage(localizer["Register.LastName.NotEmpty"]);

        RuleFor(x => x.LastName)
            .Must(str => str.ContainsOnlyTurkishCharacters(allowWhiteSpace: false))
                .WithMessage(localizer["Register.LastName.ContainsOnlyTurkishCharacters"])
            .MaximumLength(Domain.Identity.Constants.LastNameMaxLength)
                .WithMessage(localizer["Register.LastName.MaxLength {0}", Domain.Identity.Constants.LastNameMaxLength])
        .When(x => !string.IsNullOrWhiteSpace(x.LastName));

        RuleFor(x => x.NationalIdentityNumber)
            .NotEmpty()
                .WithMessage(localizer["Register.NationalIdentityNumber.NotEmpty"]);

        RuleFor(x => x.NationalIdentityNumber)
            .Length(Domain.Identity.Constants.NationalIdentityNumberLength)
                .WithMessage(localizer["Register.NationalIdentityNumber.Length {0}", Domain.Identity.Constants.NationalIdentityNumberLength])
            .Must(str => str.All(char.IsDigit))
                .WithMessage(localizer["Register.NationalIdentityNumber.ContainsOnlyDigits"])
        .When(x => !string.IsNullOrWhiteSpace(x.NationalIdentityNumber));

        RuleFor(x => x.BirthDate)
            .NotEmpty()
                .WithMessage(localizer["Register.BirthDate.NotEmpty"]);

        RuleFor(x => x.BirthDate)
            .Must(str => DateOnly.TryParseExact(str, Domain.Constants.TurkishDateFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out _))
                .WithMessage(localizer["Register.BirthDate.Format {0}", Domain.Constants.TurkishDateFormat])
        .When(x => !string.IsNullOrWhiteSpace(x.BirthDate));
    }
}
