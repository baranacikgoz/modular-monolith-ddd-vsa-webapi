using System.ComponentModel.DataAnnotations;
using Common.Core.Contracts;
using Common.Core.Extensions;
using IdentityAndAuth.Features.Identity.Domain.DomainEvents;
using Microsoft.AspNetCore.Identity;

namespace IdentityAndAuth.Features.Identity.Domain;

public readonly record struct ApplicationUserId : IStronglyTypedId
{
    public Guid Value { get; } = Guid.NewGuid();

    // Parameterless constructor for EF
    public ApplicationUserId() : this(Guid.NewGuid()) { }

    public ApplicationUserId(Guid value)
    {
        Value = value;
    }

    public static ApplicationUserId New() => new(Guid.NewGuid());
    public override string ToString() => Value.ToString();
}

public sealed partial class ApplicationUser : IdentityUser<ApplicationUserId>, IAggregateRoot
{
    public string Name { get; private set; } = string.Empty;
    public string LastName { get; private set; } = string.Empty;
    public string NationalIdentityNumber { get; private set; } = string.Empty;
    public DateOnly BirthDate { get; private set; }
    public Uri? ImageUrl { get; private set; }
    public string RefreshToken { get; private set; } = string.Empty;
    public DateTime RefreshTokenExpiresAt { get; private set; } = DateTime.MinValue;

    [ConcurrencyCheck]
    public long Version { get; set; }
    IStronglyTypedId IAggregateRoot.Id => Id;

    public static ApplicationUser Create(
        string name,
        string lastName,
        string phoneNumber,
        string nationalIdentityNumber,
        DateOnly birthDate,
        Uri? imageUrl = null)
    {
        var id = ApplicationUserId.New();
        var @event = new UserRegisteredDomainEvent(
            id,
            name.TrimmedUpperInvariantTransliterateTurkishChars(),
            lastName.TrimmedUpperInvariantTransliterateTurkishChars(),
            phoneNumber,
            nationalIdentityNumber,
            birthDate);

        var user = new ApplicationUser();
        user.RaiseEvent(@event);
        return user;
    }

    public void UpdateImageUrl(Uri imageUrl)
    {
        var @event = new UserImageUrlUpdatedDomainEvent(Id, imageUrl);
        RaiseEvent(@event);
    }

    public void UpdateRefreshToken(string refreshToken, DateTime refreshTokenExpiresAt)
    {
        // Intentionally did not follow the usual pattern here because did not want to expose token as parameter in event.
        // Events are written in db(outbox pattern) or published to a message-queue which both cases tokens should not be there unprotected.
        // Hence, should a user's refresh token be recreatable/replayable?

        RefreshToken = refreshToken;
        RefreshTokenExpiresAt = refreshTokenExpiresAt;

        var @event = new RefreshTokenUpdatedDomainEvent(Id);
        RaiseEvent(@event);
    }

    private void ApplyEvent(IEvent @event)
    {
        switch (@event)
        {
            case UserRegisteredDomainEvent e:
                Apply(e);
                break;
            case UserImageUrlUpdatedDomainEvent e:
                Apply(e);
                break;
            case RefreshTokenUpdatedDomainEvent e:
                Apply(e);
                break;
            default:
                throw new InvalidOperationException($"Unknown event {@event.GetType().Name}");
        }
    }

    private void Apply(UserRegisteredDomainEvent @event)
    {
        Id = @event.UserId;
        Name = @event.Name;
        LastName = @event.LastName;
        PhoneNumber = @event.PhoneNumber;
        UserName = @event.PhoneNumber; // We use PhoneNumber as UserName
        NationalIdentityNumber = @event.NationalIdentityNumber;
        BirthDate = @event.BirthDate;
    }

    private void Apply(UserImageUrlUpdatedDomainEvent @event)
    {
        ImageUrl = @event.ImageUrl;
    }

#pragma warning disable CA1822, S1186, IDE0060
    private void Apply(RefreshTokenUpdatedDomainEvent @event)
    {
        /// Nothing to do here, see the explanation in <see cref="UpdateRefreshToken(string, DateTime)"/>
    }
#pragma warning restore CA1822, S1186, IDE0060

#pragma warning disable CS8618 // Orms need parameterless constructors
    private ApplicationUser() { }
#pragma warning restore CS8618
}
