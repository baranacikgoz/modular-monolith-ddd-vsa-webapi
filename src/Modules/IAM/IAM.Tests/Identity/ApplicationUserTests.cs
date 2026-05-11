using Common.Domain.StronglyTypedIds;
using Common.Tests;
using IAM.Domain.Identity;
using IAM.Domain.Identity.DomainEvents.v1;
using Xunit;

namespace IAM.Tests.Identity;

public class ApplicationUserTests : AggregateTests<ApplicationUser, ApplicationUserId>
{
    private const string FullName = "John Doe";
    private const string PhoneNumber = "1234567890";
    private static readonly DateOnly BirthDate = new(1990, 1, 1);
    private static readonly Uri ImageUrl = new("https://example.com/image.png");

    [Fact]
    public void CreateUserShouldRaiseUserRegisteredDomainEvent()
    {
        Given(() => ApplicationUser.Create(FullName, PhoneNumber, BirthDate, ImageUrl))
            .Then<V1UserRegisteredDomainEvent>(
                @event => Assert.Equal(FullName.Trim(), @event.FullName),
                @event => Assert.Equal(PhoneNumber, @event.PhoneNumber),
                @event => Assert.Equal(BirthDate, @event.BirthDate),
                @event => Assert.Equal(ImageUrl, @event.ImageUrl));
    }

    [Fact]
    public void UpdateImageUrlShouldRaiseUserImageUrlUpdatedDomainEvent()
    {
        var newImageUrl = new Uri("https://example.com/new-image.png");

        Given(() => ApplicationUser.Create(FullName, PhoneNumber, BirthDate, ImageUrl))
            .When(user => user.UpdateImageUrl(newImageUrl))
            .Then(user => Assert.Equal(newImageUrl, user.ImageUrl))
            .Then<V1UserImageUrlUpdatedDomainEvent>(@event => Assert.Equal(newImageUrl, @event.ImageUrl));
    }

    [Fact]
    public void UpdateRefreshTokenShouldRaiseRefreshTokenUpdatedDomainEvent()
    {
        var refreshTokenHash = new byte[] { 1, 2, 3 };
        var expiresAt = DateTimeOffset.UtcNow.AddDays(7);

        Given(() => ApplicationUser.Create(FullName, PhoneNumber, BirthDate, ImageUrl))
            .When(user => user.UpdateRefreshToken(refreshTokenHash, expiresAt))
            .Then(user => Assert.Equivalent(refreshTokenHash, user.RefreshTokenHash))
            .Then(user => Assert.Equal(expiresAt, user.RefreshTokenExpiresAt))
            .Then<V1RefreshTokenUpdatedDomainEvent>(_ => { });
    }

    [Fact]
    public void RevokeRefreshTokenShouldClearTokenFieldsAndRaiseEvent()
    {
        var refreshTokenHash = new byte[] { 1, 2, 3 };
        var expiresAt = DateTimeOffset.UtcNow.AddDays(7);

        Given(() => ApplicationUser.Create(FullName, PhoneNumber, BirthDate, ImageUrl))
            .When(user => user.UpdateRefreshToken(refreshTokenHash, expiresAt))
            .When(user => user.RevokeRefreshToken())
            .Then(user => Assert.Empty(user.RefreshTokenHash))
            .Then(user => Assert.Equal(DateTimeOffset.MinValue, user.RefreshTokenExpiresAt))
            .Then<V1RefreshTokenRevokedDomainEvent>(_ => { });
    }
}
