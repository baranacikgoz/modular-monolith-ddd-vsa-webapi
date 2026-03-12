
using Common.Domain.StronglyTypedIds;
using FluentAssertions;
using Common.Tests;
using Xunit;
using IAM.Domain.Identity;
using IAM.Domain.Identity.DomainEvents.v1;

namespace IAM.UnitTests;

public class ApplicationUserTests : AggregateTests<ApplicationUser, ApplicationUserId>
{
    private const string Name = "John";
    private const string LastName = "Doe";
    private const string PhoneNumber = "1234567890";
    private const string NationalIdentityNumber = "12345678901";
    private static readonly DateOnly BirthDate = new(1990, 1, 1);
    private static readonly Uri ImageUrl = new("https://example.com/image.png");

    [Fact]
    public void CreateUserShouldRaiseUserRegisteredDomainEvent()
    {
        Given(() => ApplicationUser.Create(Name, LastName, PhoneNumber, NationalIdentityNumber, BirthDate, ImageUrl))
            .Then<V1UserRegisteredDomainEvent>(
                @event => @event.Name.Should().Be(Name.ToUpperInvariant()), // Domain logic enforces upper case
                @event => @event.LastName.Should().Be(LastName.ToUpperInvariant()),
                @event => @event.PhoneNumber.Should().Be(PhoneNumber),
                @event => @event.NationalIdentityNumber.Should().Be(NationalIdentityNumber),
                @event => @event.BirthDate.Should().Be(BirthDate),
                @event => @event.ImageUrl.Should().Be(ImageUrl));
    }

    [Fact]
    public void UpdateImageUrlShouldRaiseUserImageUrlUpdatedDomainEvent()
    {
        var newImageUrl = new Uri("https://example.com/new-image.png");

        Given(() => ApplicationUser.Create(Name, LastName, PhoneNumber, NationalIdentityNumber, BirthDate, ImageUrl))
            .When(user => user.UpdateImageUrl(newImageUrl))
            .Then(user => user.ImageUrl.Should().Be(newImageUrl))
            .Then<V1UserImageUrlUpdatedDomainEvent>(
                @event => @event.ImageUrl.Should().Be(newImageUrl));
    }

    [Fact]
    public void UpdateRefreshTokenShouldRaiseRefreshTokenUpdatedDomainEvent()
    {
        var refreshTokenHash = new byte[] { 1, 2, 3 };
        var expiresAt = DateTimeOffset.UtcNow.AddDays(7);

        Given(() => ApplicationUser.Create(Name, LastName, PhoneNumber, NationalIdentityNumber, BirthDate, ImageUrl))
            .When(user => user.UpdateRefreshToken(refreshTokenHash, expiresAt))
            .Then(user => user.RefreshTokenHash.Should().BeEquivalentTo(refreshTokenHash))
            .Then(user => user.RefreshTokenExpiresAt.Should().Be(expiresAt))
            .Then<V1RefreshTokenUpdatedDomainEvent>(
                _ => { });
    }
}
