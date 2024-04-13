using System.Xml.Linq;
using Common.Core.Contracts;
using Common.Core.Contracts.Results;
using Common.Core.Extensions;
using Common.Core.Interfaces;
using IdentityAndAuth.Features.Identity.Domain.DomainEvents;
using Microsoft.AspNetCore.Identity;

namespace IdentityAndAuth.Features.Identity.Domain;

public sealed partial class ApplicationUser : IdentityUser<Guid>, IAggregateRoot, IAuditableEntity
{
    private ApplicationUser(UserRegisteredDomainEvent @event)
    {
        Apply(@event);
    }

    public string Name { get; private set; } = string.Empty;
    public string LastName { get; private set; } = string.Empty;
    public string NationalIdentityNumber { get; private set; } = string.Empty;
    public DateOnly BirthDate { get; private set; }
    public Uri? ImageUrl { get; private set; }
    public string RefreshToken { get; private set; } = string.Empty;
    public DateTime RefreshTokenExpiresAt { get; private set; } = DateTime.MinValue;

    public static ApplicationUser Create(
        string name,
        string lastName,
        string phoneNumber,
        string nationalIdentityNumber,
        DateOnly birthDate,
        Uri? imageUrl = null)
    {
        var id = Guid.NewGuid();
        var @event = new UserRegisteredDomainEvent(
            id,
            name.TrimmedUpperInvariantTransliterateTurkishChars(),
            lastName.TrimmedUpperInvariantTransliterateTurkishChars(),
            phoneNumber,
            nationalIdentityNumber,
            birthDate);

        var user = new ApplicationUser(@event);
        user.EnqueueEvent(@event);
        return user;
    }

    public void UpdateImageUrl(Uri imageUrl)
    {
        var @event = new UserImageUrlUpdatedDomainEvent(Id, imageUrl);
        RaiseEvent(@event);
    }

    public void UpdateRefreshToken(string refreshToken, DateTime refreshTokenExpiresAt)
    {
        // Intentionally did not follow the RaiseEvent pattern here because did not want to expose token as parameter in event.
        // Events are written in db(outbox pattern) or published to a message-queue which both cases tokens should not be there unprotected.
        // Hence, should a user's refresh token be recreatable/replayable?

        RefreshToken = refreshToken;
        RefreshTokenExpiresAt = refreshTokenExpiresAt;

        var @event = new RefreshTokenUpdatedDomainEvent(Id);
        EnqueueEvent(@event);
    }

    private void Apply(IEvent @event)
    {
        switch (@event)
        {
            case UserRegisteredDomainEvent e:
                Apply(e);
                break;
            case UserImageUrlUpdatedDomainEvent e:
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

#pragma warning disable CS8618 // Orms need parameterless constructors
    private ApplicationUser() { }
#pragma warning restore CS8618
}
