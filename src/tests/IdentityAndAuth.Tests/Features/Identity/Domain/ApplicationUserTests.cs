using Common.Core.Contracts.Results;
using IdentityAndAuth.Features.Identity.Domain;
using IdentityAndAuth.Features.Identity.Domain.Errors;

namespace IdentityAndAuth.Tests.Features.Identity.Domain;

public class ApplicationUserTests
{

    [Fact]
    public void Create_ShouldCreateApplicationUser_WhenValidParameters()
    {
        // Arrange & Act
        var firstName = "John";
        var lastName = "Doe";
        var phoneNumber = "5555555555";
        var nationalIdentityNumber = "12345678901";
        var birthDate = new DateOnly(1990, 1, 1);

        var user = ApplicationUser.Create(
            firstName,
            lastName,
            phoneNumber,
            nationalIdentityNumber,
            birthDate);

        // Assert
        user.Name.Should().Be("JOHN");
        user.LastName.Should().Be("DOE");
        user.PhoneNumber.Should().Be("5555555555");
        user.UserName.Should().Be("5555555555");
        user.NationalIdentityNumber.Should().Be("12345678901");
        user.BirthDate.Should().Be(new DateOnly(1990, 1, 1));
    }

    [Fact]
    public void UpdateRefreshToken_ShouldUpdateRefreshTokenAndRefreshTokenExpiresAt_WhenValidParameters()
    {
        // Arrange
        var firstName = "John";
        var lastName = "Doe";
        var phoneNumber = "5555555555";
        var nationalIdentityNumber = "12345678901";
        var birthDate = new DateOnly(1990, 1, 1);

        var user = ApplicationUser.Create(
            firstName,
            lastName,
            phoneNumber,
            nationalIdentityNumber,
            birthDate);

        // Act
        user.UpdateRefreshToken("refreshToken", DateTime.Now);

        // Assert
        user.RefreshToken.Should().Be("refreshToken");
        user.RefreshTokenExpiresAt.Should().BeCloseTo(DateTime.Now, TimeSpan.FromMilliseconds(100));
    }
}
