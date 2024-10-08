using System.Globalization;
using Common.Application.Localization;
using Common.Application.Validation;
using FluentValidation;
using IAM.Application.Common.Validations;
using Microsoft.Extensions.Localization;
using Common.Domain.Extensions;

namespace IAM.Application.Identity.VersionNeutral.Users.Register;

public sealed record Request(
        string PhoneVerificationToken,
        string PhoneNumber,
        string Name,
        string LastName,
        string NationalIdentityNumber,
        string BirthDate);

public sealed class RequestValidator : CustomValidator<Request>
{
    public RequestValidator(IStringLocalizer<ResxLocalizer> localizer)
    {
        RuleFor(x => x.PhoneVerificationToken)
            .NotEmpty()
                .WithMessage(localizer["Telefon doğrulama tokeni boş olamaz."]);

        RuleFor(x => x.PhoneVerificationToken)
            .PhoneVerificationTokenValidation(localizer)
        .When(x => !string.IsNullOrWhiteSpace(x.PhoneVerificationToken));

        RuleFor(x => x.PhoneNumber)
            .NotEmpty()
                .WithMessage(localizer["Telefon numarası boş olamaz."]);

        RuleFor(x => x.PhoneNumber)
            .PhoneNumberValidation(localizer)
        .When(x => !string.IsNullOrWhiteSpace(x.PhoneNumber));

        RuleFor(x => x.Name)
            .NotEmpty()
                .WithMessage(localizer["İsim boş olamaz."]);

        RuleFor(x => x.Name)
            .Must(str => str.ContainsOnlyTurkishCharacters(allowWhiteSpace: true))
                .WithMessage(localizer["İsim sadece Türkçe alfabesindeki karakterlerden oluşabilir."])
            .MaximumLength(Domain.Identity.Constants.NameMaxLength)
                .WithMessage(localizer["İsim {0} karakterden uzun olamaz.", Domain.Identity.Constants.NameMaxLength])
        .When(x => !string.IsNullOrWhiteSpace(x.Name));

        RuleFor(x => x.LastName)
            .NotEmpty()
                .WithMessage(localizer["Soyisim boş olamaz."]);

        RuleFor(x => x.LastName)
            .Must(str => str.ContainsOnlyTurkishCharacters(allowWhiteSpace: false))
                .WithMessage(localizer["Soyisim sadece Türkçe alfabesindeki karakterlerden oluşabilir."])
            .MaximumLength(Domain.Identity.Constants.LastNameMaxLength)
                .WithMessage(localizer["Soyisim {0} karakterden uzun olamaz.", Domain.Identity.Constants.LastNameMaxLength])
        .When(x => !string.IsNullOrWhiteSpace(x.LastName));

        RuleFor(x => x.NationalIdentityNumber)
            .NotEmpty()
                .WithMessage(localizer["T.C. Kimlik numarası boş olamaz."]);

        RuleFor(x => x.NationalIdentityNumber)
            .Length(Domain.Identity.Constants.NationalIdentityNumberLength)
                .WithMessage(localizer["T.C. Kimlik numarası {0} karakter olmalıdır.", Domain.Identity.Constants.NationalIdentityNumberLength])
            .Must(str => str.All(char.IsDigit))
                .WithMessage(localizer["T.C. Kimlik numarası sadece rakamlardan oluşabilir."])
        .When(x => !string.IsNullOrWhiteSpace(x.NationalIdentityNumber));

        RuleFor(x => x.BirthDate)
            .NotEmpty()
                .WithMessage(localizer["Doğum tarihi boş olamaz."]);

        RuleFor(x => x.BirthDate)
            .Must(str => DateOnly.TryParseExact(str, Constants.TurkishDateFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out _))
                .WithMessage(localizer["Doğum tarihi, {0} formatında olmalıdır.", Constants.TurkishDateFormat])
        .When(x => !string.IsNullOrWhiteSpace(x.BirthDate));
    }
}
