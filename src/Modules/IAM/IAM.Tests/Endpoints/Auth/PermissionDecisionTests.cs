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

    [Fact]
    public async Task RevokeSession_PurgesCachedDecisionAndDeniesTheStillValidToken()
    {
        var phone = await IamTestClient.RegisterFreshUserAsync(Factory);
        var victim = await IamTestClient.LoginByPhoneAsync(Factory, phone);
        var other = await IamTestClient.LoginByPhoneAsync(Factory, phone, clientId: "mobile-app-2");
        var cache = Scope.ServiceProvider.GetRequiredService<IFusionCache>();
        var key = CacheKeys.For.AuthorizationDecision(victim.Jti,
            KeycloakPermission.FromScope(KeycloakScopes.Users.ViewOwn).PolicyName());

        using var before = await IamTestClient.Authorized(Factory, victim).GetAsync(new Uri("/users/me", UriKind.Relative));
        Assert.Equal(HttpStatusCode.OK, before.StatusCode);
        Assert.True((await cache.TryGetAsync<bool>(key)).HasValue);

        using var revoke = await IamTestClient.Authorized(Factory, other)
            .DeleteAsync(new Uri($"/tokens/sessions/{victim.SessionId}", UriKind.Relative));
        Assert.Equal(HttpStatusCode.NoContent, revoke.StatusCode);

        // The JWT is still signed and unexpired, so authentication passes; authorization must re-ask Keycloak.
        Assert.False((await cache.TryGetAsync<bool>(key)).HasValue);
        using var after = await IamTestClient.Authorized(Factory, victim).GetAsync(new Uri("/users/me", UriKind.Relative));
        Assert.Equal(HttpStatusCode.Forbidden, after.StatusCode);
    }

    [Fact]
    public async Task RevokeAll_PurgesCachedDecisionsOfEverySession()
    {
        var phone = await IamTestClient.RegisterFreshUserAsync(Factory);
        var phoneLogin = await IamTestClient.LoginByPhoneAsync(Factory, phone);
        var tabletLogin = await IamTestClient.LoginByPhoneAsync(Factory, phone, clientId: "mobile-app-2");
        var cache = Scope.ServiceProvider.GetRequiredService<IFusionCache>();
        var policy = KeycloakPermission.FromScope(KeycloakScopes.Users.ViewOwn).PolicyName();

        foreach (var login in new[] { phoneLogin, tabletLogin })
        {
            using var warm = await IamTestClient.Authorized(Factory, login).GetAsync(new Uri("/users/me", UriKind.Relative));
            Assert.Equal(HttpStatusCode.OK, warm.StatusCode);
        }

        using var revokeAll = await IamTestClient.Authorized(Factory, tabletLogin)
            .PostAsync(new Uri("/tokens/sessions/revoke-all", UriKind.Relative), content: null);
        Assert.Equal(HttpStatusCode.NoContent, revokeAll.StatusCode);

        foreach (var login in new[] { phoneLogin, tabletLogin })
        {
            Assert.False((await cache.TryGetAsync<bool>(CacheKeys.For.AuthorizationDecision(login.Jti, policy))).HasValue);
            using var after = await IamTestClient.Authorized(Factory, login).GetAsync(new Uri("/users/me", UriKind.Relative));
            Assert.Equal(HttpStatusCode.Forbidden, after.StatusCode);
        }
    }
}
