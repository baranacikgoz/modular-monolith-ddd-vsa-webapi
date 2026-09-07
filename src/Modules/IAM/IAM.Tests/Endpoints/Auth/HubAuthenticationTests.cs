using System.Net;
using Common.Tests;
using Xunit;

namespace IAM.Tests.Endpoints.Auth;

/// <summary>
///     The JwtBearer message handler lets SignalR clients pass the token as <c>access_token</c> in the query
///     string. That fallback must not discard a token sent the normal way (Authorization header).
/// </summary>
[Collection("IntegrationTestCollection")]
public class HubAuthenticationTests(IntegrationTestWebAppFactory factory) : BaseIntegrationTest(factory)
{
    private const string NegotiatePath = "/hubs/notifications/negotiate?negotiateVersion=1";

    [Fact]
    public async Task Negotiate_BearerHeaderWithoutQueryToken_IsAuthenticated()
    {
        var tokens = await IamTestClient.LoginByPhoneAsync(Factory, SeedUsers.BasicPhone);

        using var response = await IamTestClient.Authorized(Factory, tokens)
            .PostAsync(new Uri(NegotiatePath, UriKind.Relative), content: null);

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task Negotiate_QueryStringToken_IsAuthenticated()
    {
        var tokens = await IamTestClient.LoginByPhoneAsync(Factory, SeedUsers.BasicPhone);

        using var response = await Factory.CreateClient()
            .PostAsync(new Uri($"{NegotiatePath}&access_token={tokens.AccessToken}", UriKind.Relative), content: null);

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task Negotiate_Anonymous_Returns401()
    {
        using var response = await Factory.CreateClient()
            .PostAsync(new Uri(NegotiatePath, UriKind.Relative), content: null);

        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
    }
}
