using System.ComponentModel.DataAnnotations.Schema;
using Common.Core.Contracts;
using Common.DomainEvents.viaIdentityAndAuth;
using IdentityAndAuth.Extensions;
using Microsoft.AspNetCore.Identity;

namespace IdentityAndAuth.Features.Users.Domain;

public sealed class ApplicationUser : IdentityUser<Guid>, IAggregateRoot
{
    private ApplicationUser(string firstName, string lastName, string phoneNumber, string nationalIdentityNumber, DateOnly birthDate, Uri? imageUrl = null)
    {
        FirstName = firstName;
        LastName = lastName;
        PhoneNumber = phoneNumber;
        UserName = PhoneNumber; // We use PhoneNumber as UserName
        NationalIdentityNumber = nationalIdentityNumber;
        BirthDate = birthDate;
        ImageUrl = imageUrl;

        AddDomainEvent(new Events.IdentityAndAuth.UserCreatedEvent(Id));
    }

    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public string NationalIdentityNumber { get; private set; }
    public DateOnly BirthDate { get; private set; }
    public Uri? ImageUrl { get; private set; }
    public string RefreshToken { get; private set; } = string.Empty;
    public DateTime RefreshTokenExpiresAt { get; private set; } = DateTime.MinValue;

    public static ApplicationUser Create(CreateApplicationUserModel model)
        => new(
            model.FirstName.TrimmedUpperInvariantTransliterateTurkishChars(),
            model.LastName.TrimmedUpperInvariantTransliterateTurkishChars(),
            model.PhoneNumber.Trim(),
            model.NationalIdentityNumber.Trim(),
            model.BirthDate)
        {
            PhoneNumberConfirmed = true // We first validated phone number, then create user.
        };

    public void UpdateRefreshToken(string refreshToken, DateTime refreshTokenExpiresAt)
    {
        RefreshToken = refreshToken;
        RefreshTokenExpiresAt = refreshTokenExpiresAt;
    }

    private readonly List<DomainEvent> _domainEvents = new();
    [NotMapped]
    public IReadOnlyCollection<DomainEvent> DomainEvents => _domainEvents.AsReadOnly();
    private void AddDomainEvent(DomainEvent domainEvent) => _domainEvents.Add(domainEvent);
    public void ClearDomainEvents() => _domainEvents.Clear();

#pragma warning disable CS8618 // Orms need parameterless constructors
    private ApplicationUser() { }
#pragma warning restore CS8618
}
