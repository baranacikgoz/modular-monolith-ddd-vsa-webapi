using Common.Core.Contracts.Results;
using IdentityAndAuth.Extensions;
using Microsoft.AspNetCore.Identity;

namespace IdentityAndAuth.Features.Identity.Domain;

public sealed class ApplicationUser : IdentityUser<Guid>
{
    private ApplicationUser(string name, string lastName, string phoneNumber, string nationalIdentityNumber, DateOnly birthDate, Uri? imageUrl = null)
    {
        Name = name;
        LastName = lastName;
        PhoneNumber = phoneNumber;
        UserName = PhoneNumber; // We use PhoneNumber as UserName
        NationalIdentityNumber = nationalIdentityNumber;
        BirthDate = birthDate;
        ImageUrl = imageUrl;
    }

    public string Name { get; private set; }
    public string LastName { get; private set; }
    public string NationalIdentityNumber { get; private set; }
    public DateOnly BirthDate { get; private set; }
    public Uri? ImageUrl { get; private set; }
    public string RefreshToken { get; private set; } = string.Empty;
    public DateTime RefreshTokenExpiresAt { get; private set; } = DateTime.MinValue;

    public static async Task<Result<ApplicationUser>> CreateAsync(
        CreateApplicationUserModel model,
        IPhoneVerificationTokenService phoneVerificationTokenService,
        string phoneVerificationToken,
        CancellationToken cancellationToken)
        => await phoneVerificationTokenService
            .ValidateTokenAsync(model.PhoneNumber, phoneVerificationToken, cancellationToken)
            .BindAsync(() => CreateName(model.FirstName, model.LastName))
            .MapAsync(names => new ApplicationUser(
                names.ProcessedName,
                names.ProcessedLastName,
                model.PhoneNumber.Trim(),
                model.NationalIdentityNumber.Trim(),
                model.BirthDate));

    private static Result<(string ProcessedName, string ProcessedLastName)> CreateName(string firstName, string lastName)
    {
        var processedFirstName = TrimmedUpperInvariantTransliterateTurkishChars(firstName);
        var processedLastName = TrimmedUpperInvariantTransliterateTurkishChars(lastName);

        return (processedFirstName, processedLastName);
    }

    public void UpdateRefreshToken(string refreshToken, DateTime refreshTokenExpiresAt)
    {
        RefreshToken = refreshToken;
        RefreshTokenExpiresAt = refreshTokenExpiresAt;
    }

    private static string TrimmedUpperInvariantTransliterateTurkishChars(string value)
    {
        value = value.Trim();

        return string.Create(value.Length, value, (chars, state) =>
        {
            for (var i = 0; i < state.Length; i++)
            {
                var c = state[i];

                var result = c switch
                {
                    'ş' => 'S',
                    'Ş' => 'S',
                    'ğ' => 'G',
                    'Ğ' => 'G',
                    'ç' => 'C',
                    'Ç' => 'C',
                    'ö' => 'O',
                    'Ö' => 'O',
                    'ü' => 'U',
                    'Ü' => 'U',
                    'ı' => 'I',
                    'İ' => 'I',
                    _ => char.ToUpperInvariant(c),
                };

                chars[i] = result;
            }
        });
    }

#pragma warning disable CS8618 // Orms need parameterless constructors
    private ApplicationUser() { }
#pragma warning restore CS8618
}
