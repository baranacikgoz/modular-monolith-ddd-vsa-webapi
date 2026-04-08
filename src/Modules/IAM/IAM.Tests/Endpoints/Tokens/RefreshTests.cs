using System.Net;
using System.Net.Http.Json;
using System.Security.Cryptography;
using Bogus;
using Common.Tests;
using IAM.Application.Tokens.Services;
using IAM.Domain.Identity;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace IAM.Tests.Endpoints.Tokens;

[Collection("IntegrationTestCollection")]
public class RefreshTests : BaseIntegrationTest
{
    private readonly Faker _faker = new();

    public RefreshTests(IntegrationTestWebAppFactory factory) : base(factory)
    {
    }

    [Fact]
    public async Task RefreshToken_WithValidToken_ReturnsNewAccessToken()
    {
        // Arrange
        using var scope = Factory.Services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<IAM.Application.Persistence.IIAMDbContext>();
        var tokenService = scope.ServiceProvider.GetRequiredService<ITokenService>();
        var timeProvider = scope.ServiceProvider.GetRequiredService<TimeProvider>();

        var phoneNumber = "905" + _faker.Random.Number(100000000, 999999999).ToString(System.Globalization.CultureInfo.InvariantCulture);

        var user = ApplicationUser.Create(
            _faker.Name.FirstName(),
            _faker.Name.LastName(),
            phoneNumber,
            _faker.Random.Long(10000000000L, 99999999999L).ToString(System.Globalization.CultureInfo.InvariantCulture),
            DateOnly.FromDateTime(_faker.Date.Past(30))
        );

        var utcNow = timeProvider.GetUtcNow();
        var (refreshTokenBytes, refreshTokenExpiresAt) = tokenService.GenerateRefreshToken(utcNow);

        user.UpdateRefreshToken(SHA256.HashData(refreshTokenBytes), refreshTokenExpiresAt);

        db.Users.Add(user);
        await db.SaveChangesAsync(default);

        var client = Factory.CreateClient();
        var request = new IAM.Endpoints.Tokens.VersionNeutral.Refresh.Request
        {
            RefreshToken = Convert.ToBase64String(refreshTokenBytes)
        };

        // Act
        var response = await client.PostAsJsonAsync(new Uri("/tokens/refresh", UriKind.Relative), request);

        // Assert
        if (!response.IsSuccessStatusCode)
        {
            var err = await response.Content.ReadAsStringAsync();
            Assert.Fail($"Status: {response.StatusCode}. Error: {err}");
        }
        response.EnsureSuccessStatusCode();

        var rawJson = await response.Content.ReadAsStringAsync();
        using var doc = System.Text.Json.JsonDocument.Parse(rawJson);
        var root = doc.RootElement;

        Assert.True(root.TryGetProperty("accessToken", out var accessToken));
        Assert.False(string.IsNullOrWhiteSpace(accessToken.GetString()));

        Assert.True(root.TryGetProperty("accessTokenExpiresAt", out var expiresAt));
        Assert.True(expiresAt.GetDateTimeOffset() > utcNow);
    }

    [Fact]
    public async Task RefreshToken_WithExpiredToken_ReturnsError()
    {
        // Arrange
        using var scope = Factory.Services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<IAM.Application.Persistence.IIAMDbContext>();
        var tokenService = scope.ServiceProvider.GetRequiredService<ITokenService>();
        var timeProvider = scope.ServiceProvider.GetRequiredService<TimeProvider>();

        var phoneNumber = "905" + _faker.Random.Number(100000000, 999999999).ToString(System.Globalization.CultureInfo.InvariantCulture);

        var user = ApplicationUser.Create(
            _faker.Name.FirstName(),
            _faker.Name.LastName(),
            phoneNumber,
            _faker.Random.Long(10000000000L, 99999999999L).ToString(System.Globalization.CultureInfo.InvariantCulture),
            DateOnly.FromDateTime(_faker.Date.Past(30))
        );

        // Generate a refresh token but set expiry in the PAST
        var (refreshTokenBytes, _) = tokenService.GenerateRefreshToken(timeProvider.GetUtcNow());
        var expiredAt = timeProvider.GetUtcNow().AddDays(-1); // already expired
        user.UpdateRefreshToken(SHA256.HashData(refreshTokenBytes), expiredAt);

        db.Users.Add(user);
        await db.SaveChangesAsync(default);

        var client = Factory.CreateClient();
        var request = new IAM.Endpoints.Tokens.VersionNeutral.Refresh.Request
        {
            RefreshToken = Convert.ToBase64String(refreshTokenBytes)
        };

        // Act
        var response = await client.PostAsJsonAsync(new Uri("/tokens/refresh", UriKind.Relative), request);

        // Assert — expired token must be rejected
        Assert.False(response.IsSuccessStatusCode);
    }

    [Fact]
    public async Task RefreshToken_WithMalformedBase64_ReturnsBadRequest()
    {
        // Arrange — this specifically tests that the endpoint does NOT throw a 500
        // due to the FormatException that Convert.FromBase64String throws on bad input.
        var client = Factory.CreateClient();
        var request = new IAM.Endpoints.Tokens.VersionNeutral.Refresh.Request
        {
            RefreshToken = "this-is-not!!-valid-base64-%%"
        };

        // Act
        var response = await client.PostAsJsonAsync(new Uri("/tokens/refresh", UriKind.Relative), request);

        // Assert — must not be a 500; should be 4xx (domain error)
        Assert.NotEqual(HttpStatusCode.InternalServerError, response.StatusCode);
        Assert.False(response.IsSuccessStatusCode);
    }
}
