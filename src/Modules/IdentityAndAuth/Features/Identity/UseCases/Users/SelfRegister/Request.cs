using System.Buffers;
using System.Globalization;
using Common.Core.Contracts.Results;
using FluentValidation;
using IdentityAndAuth.Features.Common.Validations;
using IdentityAndAuth.Features.Identity.Domain;
using Microsoft.Extensions.Localization;
using NimbleMediator.Contracts;

namespace IdentityAndAuth.Features.Identity.UseCases.Users.SelfRegister;

public sealed record Request(
        string PhoneVerificationToken,
        string PhoneNumber,
        string FirstName,
        string? MiddleName,
        string LastName,
        string NationalIdentityNumber,
        string BirthDate);

public sealed class RequestValidator : AbstractValidator<Request>
{
    // Checkout: https://www.youtube.com/watch?v=IzDMg916t98&t=573s&ab_channel=NickChapsas
    private static readonly SearchValues<char> _turkishAlphabet = SearchValues.Create("abcçdefgğhıijklmnoöprsştuüvyzABCÇDEFGĞHIİJKLMNOÖPRSŞTUÜVYZ");

    public RequestValidator(IStringLocalizer<RequestValidator> localizer)
    {
        RuleFor(x => x.PhoneVerificationToken)
            .PhoneVerificationTokenValidation(localizer);

        RuleFor(x => x.PhoneNumber)
            .PhoneNumberValidation(localizer);

        RuleFor(x => x.FirstName)
            .NotEmpty()
                .WithMessage(localizer["İsim boş olamaz."])
            .Must(str => IsEligibleForName(str))
                .WithMessage(localizer["İsim sadece Türkçe alfabesindeki karakterlerden oluşabilir."])
            .MaximumLength(ApplicationUserConstants.FirstNameMaxLength)
                .WithMessage(localizer["İsim {0} karakterden uzun olamaz.", ApplicationUserConstants.FirstNameMaxLength]);

        RuleFor(x => x.MiddleName)
            .Must(str => IsEligibleForName(str!))
                .WithMessage(localizer["Orta isimler sadece Türkçe alfabesindeki karakterlerden oluşabilir."])
            .Must(str => str!.Length <= ApplicationUserConstants.FirstNameMaxLength)
                .WithMessage(localizer["Orta isimler {0} karakterden uzun olamaz.", ApplicationUserConstants.FirstNameMaxLength])
            .When(x => x.MiddleName is not null);

        RuleFor(x => x.LastName)
            .NotEmpty()
                .WithMessage(localizer["Soyisim boş olamaz."])
            .Must(str => IsEligibleForName(str))
                .WithMessage(localizer["Soyisim sadece Türkçe alfabesindeki karakterlerden oluşabilir."])
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
            .Must(str => DateOnly.TryParse(str, CultureInfo.InvariantCulture, DateTimeStyles.None, out _))
                .WithMessage(localizer["Doğum tarihi, yyyy-MM-dd formatında olmalıdır."]);
    }

    private static bool IsEligibleForName(string s) => !s.AsSpan().ContainsAnyExcept(_turkishAlphabet);
}
