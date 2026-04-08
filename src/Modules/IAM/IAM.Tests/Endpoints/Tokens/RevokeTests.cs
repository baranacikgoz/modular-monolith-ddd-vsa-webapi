using System.Net;
using System.Net.Http.Headers;
using System.Security.Cryptography;
using Bogus;
using Common.Tests;
using IAM.Application.Persistence;
using IAM.Application.Tokens.Services;
using IAM.Domain.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace IAM.Tests.Endpoints.Tokens;

[Collection("IntegrationTestCollection")]
public class RevokeTests : BaseIntegrationTest
{
    private readonly Faker _faker = new();

    public RevokeTests(IntegrationTestWebAppFactory factory) : base(factory)
    {
    }

    [Fact]
    public async Task RevokeToken_WithValidAuth_ClearsRefreshTokenOnUser()
    {
        // Arrange
        using var scope = Factory.Services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<IIAMDbContext>();
        var tokenService = scope.ServiceProvider.GetRequiredService<ITokenService>();
        var timeProvider = scope.ServiceProvider.GetRequiredService<TimeProvider>();

        var phoneNumber = "905" + _faker.Random.Number(100000000, 999999999)
            .ToString(System.Globalization.CultureInfo.InvariantCulture);

        var user = ApplicationUser.Create(
            _faker.Name.FirstName(),
            _faker.Name.LastName(),
            phoneNumber,
            _faker.Random.Long(10000000000L, 99999999999L)
                .ToString(System.Globalization.CultureInfo.InvariantCulture),
            DateOnly.FromDateTime(_faker.Date.Past(30))
        );

        var (refreshTokenBytes, refreshTokenExpiresAt) = tokenService.GenerateRefreshToken(timeProvider.GetUtcNow());
        user.UpdateRefreshToken(SHA256.HashData(refreshTokenBytes), refreshTokenExpiresAt);

        db.Users.Add(user);
        await db.SaveChangesAsync(default);

        var client = Factory.CreateClient();
        client.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue(TestAuthHandler.AuthenticationScheme);
        client.DefaultRequestHeaders.Add("X-Test-User-Id", user.Id.Value.ToString());

        // Act
        var response = await client.PostAsync(new Uri("/tokens/revoke", UriKind.Relative), null);

        // Assert
        if (!response.IsSuccessStatusCode)
        {
            var err = await response.Content.ReadAsStringAsync();
            Assert.Fail($"Status: {response.StatusCode}. Error: {err}");
        }
        Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);

        // Verify side-effect: refresh token fields must be cleared on the DB entity
        using var verifyScope = Factory.Services.CreateScope();
        var verifyDb = verifyScope.ServiceProvider.GetRequiredService<IIAMDbContext>();
        var updatedUser = await verifyDb.Users.AsNoTracking()
            .FirstOrDefaultAsync(u => u.Id == user.Id);

        Assert.NotNull(updatedUser);
        Assert.Empty(updatedUser.RefreshTokenHash);
        Assert.Equal(DateTimeOffset.MinValue, updatedUser.RefreshTokenExpiresAt);
    }

    [Fact]
    public async Task RevokeToken_WithoutAuth_ReturnsUnauthorized()
    {
        // Arrange
        var client = Factory.CreateClient();
        // No Authorization header

        // Act
        var response = await client.PostAsync(new Uri("/tokens/revoke", UriKind.Relative), null);

        // Assert — unauthenticated callers must be rejected
        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
    }
}
