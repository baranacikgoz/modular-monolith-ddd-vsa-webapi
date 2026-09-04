using System.Net;
using System.Net.Http.Json;
using Common.Tests;
using IAM.Endpoints.Tokens.VersionNeutral.Refresh;
using Xunit;

namespace IAM.Tests.Endpoints.Tokens;

[Collection("IntegrationTestCollection")]
public class RefreshTests(IntegrationTestWebAppFactory factory) : BaseIntegrationTest(factory)
{
    [Fact]
    public async Task Refresh_ValidToken_RotatesAndKeepsSession()
    {
        var login = await IamTestClient.LoginByPhoneAsync(Factory, SeedUsers.BasicPhone);

        var refreshed = await RefreshAsync(login.RefreshToken);

        Assert.NotEqual(login.RefreshToken, refreshed.RefreshToken);
        Assert.NotEqual(login.AccessToken, refreshed.AccessToken);
        Assert.Equal(login.SessionId, refreshed.SessionId);
    }

    [Fact]
    public async Task Refresh_EmailLoginToken_UsesIssuingClient()
    {
        var login = await IamTestClient.LoginByEmailAsync(Factory, SeedUsers.StaffEmail, SeedUsers.StaffPassword);

        var refreshed = await RefreshAsync(login.RefreshToken);

        Assert.Equal(login.SessionId, refreshed.SessionId);
    }

    [Fact]
    public async Task Refresh_ReplayedToken_Returns401AndKillsTheSession()
    {
        var login = await IamTestClient.LoginByPhoneAsync(Factory, SeedUsers.BasicPhone);
        var rotated = await RefreshAsync(login.RefreshToken);

        // Replay of the rotated-away token: Keycloak's reuse detection revokes the whole session.
        using var replay = await RefreshRawAsync(login.RefreshToken);
        Assert.Equal(HttpStatusCode.Unauthorized, replay.StatusCode);

        using var afterReplay = await RefreshRawAsync(rotated.RefreshToken);
        Assert.Equal(HttpStatusCode.Unauthorized, afterReplay.StatusCode);
    }

    [Fact]
    public async Task Refresh_GarbageToken_Returns401()
    {
        using var response = await RefreshRawAsync("not-a-jwt");

        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
    }

    [Fact]
    public async Task Refresh_EmptyToken_Returns400()
    {
        using var response = await RefreshRawAsync(string.Empty);

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    private async Task<TokenPair> RefreshAsync(string refreshToken)
    {
        using var response = await RefreshRawAsync(refreshToken);
        return await IamTestClient.ReadTokensAsync(response);
    }

    private Task<HttpResponseMessage> RefreshRawAsync(string refreshToken)
    {
        var client = Factory.CreateClient();
        return client.PostAsJsonAsync(new Uri("/tokens/refresh", UriKind.Relative), new Request { RefreshToken = refreshToken });
    }
}
