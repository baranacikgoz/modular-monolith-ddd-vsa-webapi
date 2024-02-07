using System.Buffers;
using System.Globalization;
using Common.Core.Validation;
using Common.Localization;
using FluentValidation;
using IdentityAndAuth.Features.Common.Validations;
using Microsoft.Extensions.Localization;
using Common.Core.Extensions;

namespace IdentityAndAuth.Features.Identity.UseCases.Users.SelfRegister;

public sealed record Request(
        string PhoneVerificationToken,
        string PhoneNumber,
        string Name,
        string LastName,
        string NationalIdentityNumber,
        string BirthDate);

public sealed class RequestValidator : ResilientValidator<Request>
{
    public RequestValidator(IStringLocalizer<ResxLocalizer> localizer)
    {
        RuleFor(x => x.PhoneVerificationToken)
            .PhoneVerificationTokenValidation(localizer);

        RuleFor(x => x.PhoneNumber)
            .PhoneNumberValidation(localizer);

        RuleFor(x => x.Name)
            .NotEmpty()
                .WithMessage(localizer["İsim boş olamaz."])
            .Must(str => str.ContainsOnlyTurkishCharacters(allowWhiteSpace: true))
                .WithMessage(localizer["İsim sadece Türkçe alfabesindeki karakterlerden oluşabilir."])
            .MaximumLength(Domain.Constants.NameMaxLength)
                .WithMessage(localizer["İsim {0} karakterden uzun olamaz.", Domain.Constants.NameMaxLength]);

        RuleFor(x => x.LastName)
            .NotEmpty()
                .WithMessage(localizer["Soyisim boş olamaz."])
            .Must(str => str.ContainsOnlyTurkishCharacters(allowWhiteSpace: false))
                .WithMessage(localizer["Soyisim sadece Türkçe alfabesindeki karakterlerden oluşabilir."])
            .MaximumLength(Domain.Constants.LastNameMaxLength)
                .WithMessage(localizer["Soyisim {0} karakterden uzun olamaz.", Domain.Constants.LastNameMaxLength]);

        RuleFor(x => x.NationalIdentityNumber)
            .NotEmpty()
                .WithMessage(localizer["T.C. Kimlik numarası boş olamaz."])
            .Length(Domain.Constants.NationalIdentityNumberLength)
                .WithMessage(localizer["T.C. Kimlik numarası {0} karakter olmalıdır.", Domain.Constants.NationalIdentityNumberLength])
            .Must(str => str.All(char.IsDigit))
                .WithMessage(localizer["T.C. Kimlik numarası sadece rakamlardan oluşabilir."]);

        RuleFor(x => x.BirthDate)
            .NotEmpty()
                .WithMessage(localizer["Doğum tarihi boş olamaz."])
            .Must(str => DateOnly.TryParseExact(str, SelfRegister.Constants.TurkishDateFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out _))
                .WithMessage(localizer["Doğum tarihi, {0} formatında olmalıdır.", SelfRegister.Constants.TurkishDateFormat]);
    }
}
