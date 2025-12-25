using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Common.Domain.Aggregates;
using Common.Domain.Events;
using Common.Domain.Extensions;
using Common.Domain.StronglyTypedIds;
using IAM.Domain.Identity.DomainEvents.v1;
using Microsoft.AspNetCore.Identity;

namespace IAM.Domain.Identity;

#pragma warning disable CA1819 // Properties should not return arrays
public sealed partial class ApplicationUser : IdentityUser<ApplicationUserId>, IAggregateRoot
{
    public string Name { get; private set; } = string.Empty;
    public string LastName { get; private set; } = string.Empty;
    public string NationalIdentityNumber { get; private set; } = string.Empty;
    public DateOnly BirthDate { get; private set; }
    public Uri? ImageUrl { get; private set; }
    public byte[] RefreshTokenHash { get; private set; } = [];
    public DateTimeOffset RefreshTokenExpiresAt { get; private set; } = DateTimeOffset.MinValue;

    [ConcurrencyCheck] public long Version { get; set; }

    [NotMapped] IStronglyTypedId IAggregateRoot.Id => Id;

    public static ApplicationUser Create(
        string name,
        string lastName,
        string phoneNumber,
        string nationalIdentityNumber,
        DateOnly birthDate,
        Uri? imageUrl = null)
    {
        var id = ApplicationUserId.New();
        var @event = new V1UserRegisteredDomainEvent(
            id,
            name.TrimmedUpperInvariantTransliterateTurkishChars(),
            lastName.TrimmedUpperInvariantTransliterateTurkishChars(),
            phoneNumber,
            nationalIdentityNumber,
            birthDate,
            imageUrl);

        var user = new ApplicationUser();
        user.RaiseEvent(@event);
        return user;
    }

    public void UpdateImageUrl(Uri imageUrl)
    {
        var @event = new V1UserImageUrlUpdatedDomainEvent(Id, imageUrl);
        RaiseEvent(@event);
    }

    public void UpdateRefreshToken(byte[] refreshTokenHash, DateTimeOffset refreshTokenExpiresAt)
    {
        // Intentionally did not follow the usual pattern here because did not want to expose token as parameter in event.
        // Events are persisted in somewhere which tokens should not be there unprotected.

        RefreshTokenHash = refreshTokenHash;
        RefreshTokenExpiresAt = refreshTokenExpiresAt;

        var @event = new V1RefreshTokenUpdatedDomainEvent(Id);
        RaiseEvent(@event);
    }

    private void ApplyEvent(IEvent @event)
    {
        switch (@event)
        {
            case V1UserRegisteredDomainEvent e:
                Apply(e);
                break;
            case V1UserImageUrlUpdatedDomainEvent e:
                Apply(e);
                break;
            case V1RefreshTokenUpdatedDomainEvent e:
                Apply(e);
                break;
            default:
                throw new InvalidOperationException($"Unknown event {@event.GetType().Name}");
        }
    }

    private void Apply(V1UserRegisteredDomainEvent @event)
    {
        Id = @event.UserId;
        Name = @event.Name;
        LastName = @event.LastName;
        PhoneNumber = @event.PhoneNumber;
        UserName = @event.PhoneNumber; // We use PhoneNumber as UserName
        NationalIdentityNumber = @event.NationalIdentityNumber;
        BirthDate = @event.BirthDate;
    }

    private void Apply(V1UserImageUrlUpdatedDomainEvent @event)
    {
        ImageUrl = @event.ImageUrl;
    }

#pragma warning disable CA1822, S1186, IDE0060
    private void Apply(V1RefreshTokenUpdatedDomainEvent @event)
    {
        // Nothing to do here, see the explanation in UpdateRefreshToken method.
    }
#pragma warning restore CA1822, S1186, IDE0060

#pragma warning disable CS8618 // Orms need parameterless constructors
#pragma warning restore CS8618
#pragma warning restore CA1819 // Properties should not return arrays
}
