using System.Text.Json;
using Common.Application.Auth;
using Common.Application.Options;
using Common.Domain.StronglyTypedIds;
using IAM.Domain.Identity.Sessions;
using IAM.Infrastructure.Tokens.Services;
using Microsoft.Extensions.Options;
using Xunit;

namespace IAM.Tests.Tokens;

public class TokenServiceTests
{
    private static readonly TokenService _sut = new(Options.Create(new JwtOptions
    {
        Secret = "super-secret-signing-key-that-is-long-enough-for-hmac-sha256",
        Issuer = "test-issuer",
        Audience = "test-audience",
        AccessTokenExpirationInMinutes = 15,
        RefreshTokenExpirationInDays = 7,
        SessionAbsoluteExpirationInDays = 90,
        RefreshTokenReuseGraceWindowInSeconds = 30,
        AllowedClientIds = ["mobile-app-1", "web-app-1"]
    }));

    [Fact]
    public void GenerateAccessToken_WithSingleRole_EmitsRolesAsJsonArray()
    {
        var (accessToken, _) = _sut.GenerateAccessToken(
            DateTimeOffset.UtcNow, new ApplicationUserId(DefaultIdType.NewGuid()), SessionId.New(), [CustomRoles.Basic]);

        var roles = DecodePayload(accessToken).GetProperty(JwtClaimNames.Roles);

        // Even for ONE role the wire shape must be a JSON array — the FE never branches on string-vs-array.
        Assert.Equal(JsonValueKind.Array, roles.ValueKind);
        Assert.Equal([CustomRoles.Basic], roles.EnumerateArray().Select(x => x.GetString()!).ToArray());
    }

    [Fact]
    public void GenerateAccessToken_WithMultipleRoles_EmitsAllRolesInArray()
    {
        var (accessToken, _) = _sut.GenerateAccessToken(
            DateTimeOffset.UtcNow, new ApplicationUserId(DefaultIdType.NewGuid()), SessionId.New(),
            [CustomRoles.Basic, CustomRoles.SystemAdmin]);

        var roles = DecodePayload(accessToken).GetProperty(JwtClaimNames.Roles);

        Assert.Equal(JsonValueKind.Array, roles.ValueKind);
        Assert.Equal(
            [CustomRoles.Basic, CustomRoles.SystemAdmin],
            roles.EnumerateArray().Select(x => x.GetString()!).ToArray());
    }

    private static JsonElement DecodePayload(string jwt)
    {
        var payload = jwt.Split('.')[1];
        var padded = payload.Replace('-', '+').Replace('_', '/');
        padded = padded.PadRight(padded.Length + ((4 - (padded.Length % 4)) % 4), '=');
        return JsonDocument.Parse(Convert.FromBase64String(padded)).RootElement;
    }
}
