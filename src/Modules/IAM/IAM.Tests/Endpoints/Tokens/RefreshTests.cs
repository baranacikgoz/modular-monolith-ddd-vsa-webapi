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
    public async Task Refresh_SameTokenTwice_IsToleratedAsLostResponseRetry()
    {
        var login = await IamTestClient.LoginByPhoneAsync(Factory, SeedUsers.BasicPhone);
        var first = await RefreshAsync(login.RefreshToken);

        // refreshTokenMaxReuse = 1: a client whose response got lost may retry the same token once.
        var retry = await RefreshAsync(login.RefreshToken);

        Assert.Equal(login.SessionId, retry.SessionId);
        Assert.NotEqual(first.RefreshToken, retry.RefreshToken);
    }

    [Fact]
    public async Task Refresh_TokenReplayedBeyondTolerance_Returns401AndRevokesTheSession()
    {
        var phone = await IamTestClient.RegisterFreshUserAsync(Factory);
        var login = await IamTestClient.LoginByPhoneAsync(Factory, phone);
        _ = await RefreshAsync(login.RefreshToken);
        var latest = await RefreshAsync(login.RefreshToken);

        // Third use of the same token exceeds the tolerance: Keycloak reports reuse, the API treats it as theft.
        using var replay = await RefreshRawAsync(login.RefreshToken);
        Assert.Equal(HttpStatusCode.Unauthorized, replay.StatusCode);

        // The thief's (or victim's) most recent token is dead too, and the session is gone from Keycloak.
        using var afterReplay = await RefreshRawAsync(latest.RefreshToken);
        Assert.Equal(HttpStatusCode.Unauthorized, afterReplay.StatusCode);

        var observer = await IamTestClient.LoginByPhoneAsync(Factory, phone, clientId: "mobile-app-2");
        var sessions = await IamTestClient.Authorized(Factory, observer)
            .GetFromJsonAsync<List<SessionView>>(new Uri("/tokens/sessions", UriKind.Relative));
        Assert.NotNull(sessions);
        Assert.DoesNotContain(sessions, s => s.Id == login.SessionId);
    }

    private sealed record SessionView(string Id);

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
