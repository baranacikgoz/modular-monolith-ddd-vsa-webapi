using System.Net;
using System.Net.Http.Json;
using Common.Tests;
using IAM.Endpoints.Tokens.VersionNeutral.Refresh;
using Xunit;

namespace IAM.Tests.Endpoints.Tokens;

[Collection("IntegrationTestCollection")]
public class RevokeTests(IntegrationTestWebAppFactory factory) : BaseIntegrationTest(factory)
{
    [Fact]
    public async Task Revoke_CurrentSession_RefreshFailsAndAccessTokenIsDenied()
    {
        var login = await IamTestClient.LoginByPhoneAsync(Factory, SeedUsers.BasicPhone);
        var client = IamTestClient.Authorized(Factory, login);

        using var revoke = await client.PostAsync(new Uri("/tokens/revoke", UriKind.Relative), content: null);
        Assert.Equal(HttpStatusCode.NoContent, revoke.StatusCode);

        using var refresh = await Factory.CreateClient()
            .PostAsJsonAsync(new Uri("/tokens/refresh", UriKind.Relative), new Request { RefreshToken = login.RefreshToken });
        Assert.Equal(HttpStatusCode.Unauthorized, refresh.StatusCode);

        // The JWT is still signed and unexpired, but the first (uncached) permission decision asks Keycloak,
        // which no longer accepts a token of a revoked session.
        using var me = await client.GetAsync(new Uri("/users/me", UriKind.Relative));
        Assert.Equal(HttpStatusCode.Forbidden, me.StatusCode);
    }

    [Fact]
    public async Task Revoke_Anonymous_Returns401()
    {
        using var response = await Factory.CreateClient().PostAsync(new Uri("/tokens/revoke", UriKind.Relative), content: null);

        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
    }
}
