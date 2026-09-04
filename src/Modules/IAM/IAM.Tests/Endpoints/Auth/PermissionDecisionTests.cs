using System.Net;
using Common.Application.Auth;
using Common.Application.Caching;
using Common.Tests;
using Microsoft.Extensions.DependencyInjection;
using Xunit;
using ZiggyCreatures.Caching.Fusion;

namespace IAM.Tests.Endpoints.Auth;

[Collection("IntegrationTestCollection")]
public class PermissionDecisionTests(IntegrationTestWebAppFactory factory) : BaseIntegrationTest(factory)
{
    [Fact]
    public async Task ProtectedEndpoint_DecisionIsCachedPerTokenAndPermission()
    {
        var tokens = await IamTestClient.LoginByPhoneAsync(Factory, SeedUsers.BasicPhone);
        var cache = Scope.ServiceProvider.GetRequiredService<IFusionCache>();
        var key = CacheKeys.For.AuthorizationDecision(tokens.Jti,
            KeycloakPermission.FromScope(KeycloakScopes.Users.ViewOwn).PolicyName());

        Assert.False((await cache.TryGetAsync<bool>(key)).HasValue);

        using var response = await IamTestClient.Authorized(Factory, tokens).GetAsync(new Uri("/users/me", UriKind.Relative));
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        var cached = await cache.TryGetAsync<bool>(key);
        Assert.True(cached.HasValue);
        Assert.True(cached.Value);
    }

    [Fact]
    public async Task ProtectedEndpoint_DeniedDecisionIsAlsoCached()
    {
        var tokens = await IamTestClient.LoginByPhoneAsync(Factory, SeedUsers.BasicPhone);
        var cache = Scope.ServiceProvider.GetRequiredService<IFusionCache>();
        var key = CacheKeys.For.AuthorizationDecision(tokens.Jti,
            KeycloakPermission.FromScope(KeycloakScopes.Users.Search).PolicyName());

        using var response = await IamTestClient.Authorized(Factory, tokens)
            .GetAsync(new Uri("/users/search?pageNumber=1&pageSize=10", UriKind.Relative));
        Assert.Equal(HttpStatusCode.Forbidden, response.StatusCode);

        var cached = await cache.TryGetAsync<bool>(key);
        Assert.True(cached.HasValue);
        Assert.False(cached.Value);
    }
}
