using Common.Core.Contracts.Results;
using IdentityAndAuth.Features.Identity.Domain;
using IdentityAndAuth.Features.Identity.Domain.Errors;

namespace IdentityAndAuth.Tests.Features.Identity.Domain;

public class ApplicationUserTests
{
    private readonly Mock<IPhoneVerificationTokenService> _mockPhoneVerificationTokenService;
    private readonly CancellationToken _cancellationToken;

    public ApplicationUserTests()
    {
        _mockPhoneVerificationTokenService = new Mock<IPhoneVerificationTokenService>();
        _cancellationToken = new CancellationToken();
    }

    [Fact]
    public async Task Create_ShouldCreateApplicationUser_WhenValidModelAndValidPhoneVerificationToken()
    {
        // Arrange
        var createModel = new CreateApplicationUserModel
        {
            FirstName = "John",
            LastName = "Doe",
            PhoneNumber = "5555555555",
            NationalIdentityNumber = "12345678901",
            BirthDate = new DateOnly(1990, 1, 1)
        };

        _mockPhoneVerificationTokenService
            .Setup(x => x.ValidateTokenAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(Result.Success);

        // Act
        var userResult = await ApplicationUser.CreateAsync(createModel, _mockPhoneVerificationTokenService.Object, "token", _cancellationToken);

        // Assert
        userResult.IsSuccess.Should().BeTrue();
        var user = userResult.Value!;
        user.Name.Should().Be("JOHN");
        user.LastName.Should().Be("DOE");
        user.PhoneNumber.Should().Be("5555555555");
        user.UserName.Should().Be("5555555555");
        user.NationalIdentityNumber.Should().Be("12345678901");
        user.BirthDate.Should().Be(new DateOnly(1990, 1, 1));
    }

    [Fact]
    public async Task Create_ShouldFail_WhenInvalidPhoneVerificationToken()
    {
        // Arrange
        var createModel = new CreateApplicationUserModel
        {
            FirstName = "John",
            LastName = "Doe",
            PhoneNumber = "5555555555",
            NationalIdentityNumber = "12345678901",
            BirthDate = new DateOnly(1990, 1, 1)
        };

        _mockPhoneVerificationTokenService
            .Setup(x => x.ValidateTokenAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(Result.Failure(PhoneVerificationTokenErrors.NotMatching));

        // Act
        var userResult = await ApplicationUser.CreateAsync(createModel, _mockPhoneVerificationTokenService.Object, "token", _cancellationToken);

        // Assert
        userResult.IsSuccess.Should().BeFalse();
        userResult.Error.Should().Be(PhoneVerificationTokenErrors.NotMatching);
    }

    [Fact]
    public async Task Create_ShouldCombineFirstAndMiddleNames_WhenMiddleNameIsNotNull()
    {
        // Arrange
        var createModel = new CreateApplicationUserModel
        {
            FirstName = "John",
            MiddleName = "Harrison",
            LastName = "Doe",
            PhoneNumber = "5555555555",
            NationalIdentityNumber = "12345678901",
            BirthDate = new DateOnly(1990, 1, 1)
        };

        _mockPhoneVerificationTokenService
            .Setup(x => x.ValidateTokenAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(Result.Success);

        // Act
        var userResult = await ApplicationUser.CreateAsync(createModel, _mockPhoneVerificationTokenService.Object, "token", _cancellationToken);

        // Assert
        userResult.IsSuccess.Should().BeTrue();
        var user = userResult.Value!;
        user.Name.Should().Be("JOHN HARRISON");
        user.LastName.Should().Be("DOE");
    }

    [Fact]
    public async Task UpdateRefreshToken_ShouldUpdateRefreshTokenAndRefreshTokenExpiresAt_WhenValidParameters()
    {
        // Arrange
        var model = new CreateApplicationUserModel
        {
            FirstName = "John",
            MiddleName = "Doe",
            LastName = "Doe",
            PhoneNumber = "5555555555",
            NationalIdentityNumber = "12345678901",
            BirthDate = new DateOnly(1990, 1, 1)
        };

        _mockPhoneVerificationTokenService
            .Setup(x => x.ValidateTokenAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(Result.Success);

        var userResult = await ApplicationUser.CreateAsync(model, _mockPhoneVerificationTokenService.Object, "token", _cancellationToken);
        var user = userResult.Value!;

        // Act
        user.UpdateRefreshToken("refreshToken", DateTime.Now);

        // Assert
        user.RefreshToken.Should().Be("refreshToken");
        user.RefreshTokenExpiresAt.Should().BeCloseTo(DateTime.Now, TimeSpan.FromMilliseconds(100));
    }
}
