using System.Globalization;
using FluentValidation;
using IdentityAndAuth.Features.Common.Validations;
using IdentityAndAuth.Features.Users.Domain;
using Microsoft.Extensions.Localization;

namespace IdentityAndAuth.Features.Users.SelfRegister;

public sealed class RequestValidator : AbstractValidator<Request>
{
    private const char EmptySpace = ' ';
    private static readonly HashSet<char> _turkishAlphabetSet = new("abcçdefgğhıijklmnoöprsştuüvyzABCÇDEFGĞHIİJKLMNOÖPRSŞTUÜVYZ");

    public RequestValidator(IStringLocalizer<RequestValidator> localizer)
    {
        RuleFor(x => x.PhoneVerificationToken)
            .PhoneVerificationTokenValidation(localizer);

        RuleFor(x => x.PhoneNumber)
            .PhoneNumberValidation(localizer);

        RuleFor(x => x.FirstName)
            .NotEmpty()
                .WithMessage(localizer["İsim boş olamaz."])
            .Must(str => str.All(c => IsEligibleForFirstName(c)))
                .WithMessage(localizer["İsim sadece Türkçe alfabesindeki karakterlerden oluşabilir."])
            .MaximumLength(ApplicationUserConstants.FirstNameMaxLength)
                .WithMessage(localizer["İsim {0} karakterden uzun olamaz.", ApplicationUserConstants.FirstNameMaxLength]);

        RuleFor(x => x.LastName)
            .NotEmpty()
                .WithMessage(localizer["Soyisim boş olamaz."])
            .Must(str => str.All(c => _turkishAlphabetSet.Contains(c)))
                .WithMessage(localizer["Soyisim sadece Türkçe alfabesindeki karakterlerden oluşabilir ve boşluk içermemelidir."])
            .MaximumLength(ApplicationUserConstants.LastNameMaxLength)
                .WithMessage(localizer["Soyisim {0} karakterden uzun olamaz.", ApplicationUserConstants.LastNameMaxLength]);

        RuleFor(x => x.NationalIdentityNumber)
            .NotEmpty()
                .WithMessage(localizer["T.C. Kimlik numarası boş olamaz."])
            .Length(ApplicationUserConstants.NationalIdentityNumberLength)
                .WithMessage(localizer["T.C. Kimlik numarası {0} karakter olmalıdır.", ApplicationUserConstants.NationalIdentityNumberLength])
            .Must(str => str.All(char.IsDigit))
                .WithMessage(localizer["T.C. Kimlik numarası sadece rakamlardan oluşabilir."]);

        RuleFor(x => x.BirthDate)
            .NotEmpty()
                .WithMessage(localizer["Doğum tarihi boş olamaz."])
            // tryParse datetime with providing an IFormatProvider
            .Must(str => DateOnly.TryParse(str, CultureInfo.InvariantCulture, DateTimeStyles.None, out _))
                .WithMessage(localizer["Doğum tarihi, yyyy-MM-dd formatında olmalıdır."]);
    }

    // Empty space is included because we want to allow names with middle names.
    private static bool IsEligibleForFirstName(char c)
        => _turkishAlphabetSet.Contains(c) || c.Equals(EmptySpace);
}
