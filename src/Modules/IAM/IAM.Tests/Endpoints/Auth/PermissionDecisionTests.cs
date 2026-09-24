using System.Net;
using Common.Application.Auth;
using Common.Application.Caching;
using Common.Tests;
using IAM.Application.Keycloak;
using Microsoft.Extensions.DependencyInjection;
using Xunit;
using ZiggyCreatures.Caching.Fusion;

namespace IAM.Tests.Endpoints.Auth;

[Collection("IntegrationTestCollection")]
public class PermissionDecisionTests(IntegrationTestWebAppFactory factory) : BaseIntegrationTest(factory)
{
    // system-admin is a composite of staff only (no basic), so this only passes when the "-own" self-service
    // permissions apply "Any Authenticated User Policy" rather than "Basic Role Policy" (PR #149 #2).
    [Theory]
    [InlineData(SeedUsers.AdminPhone)]
    [InlineData(SeedUsers.BasicPhone)]
    public async Task StoresCreateOwn_IsGrantedToEveryAuthenticatedRole(string phone)
    {
        var tokens = await IamTestClient.LoginByPhoneAsync(Factory, phone);
        var permissionClient = Scope.ServiceProvider.GetRequiredService<IKeycloakPermissionClient>();

        var granted = await permissionClient.DecideAsync(
            tokens.AccessToken,
            KeycloakPermission.FromScope(KeycloakScopes.Stores.CreateOwn).PolicyName(),
            CancellationToken.None);

        Assert.True(granted);
    }

    [Fact]
    public async Task ProtectedEndpoint_GrantedPermissionSetIsCachedPerToken()
    {
        var tokens = await IamTestClient.LoginByPhoneAsync(Factory, SeedUsers.BasicPhone);
        var cache = Scope.ServiceProvider.GetRequiredService<IFusionCache>();
        var key = CacheKeys.For.AuthorizationPermissions(tokens.Jti);

        Assert.False((await cache.TryGetAsync<string[]>(key)).HasValue);

        using var response = await IamTestClient.Authorized(Factory, tokens).GetAsync(new Uri("/users/me", UriKind.Relative));
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        var cached = await cache.TryGetAsync<string[]>(key);
        Assert.True(cached.HasValue);
        Assert.Contains(KeycloakPermission.FromScope(KeycloakScopes.Users.ViewOwn).PolicyName(), cached.Value);
    }

    [Fact]
    public async Task ProtectedEndpoint_DeniedPermissionIsAbsentFromTheCachedSet_NoSecondKeycloakCall()
    {
        var tokens = await IamTestClient.LoginByPhoneAsync(Factory, SeedUsers.BasicPhone);
        var cache = Scope.ServiceProvider.GetRequiredService<IFusionCache>();
        var key = CacheKeys.For.AuthorizationPermissions(tokens.Jti);

        using var response = await IamTestClient.Authorized(Factory, tokens)
            .GetAsync(new Uri("/users/search?pageNumber=1&pageSize=10", UriKind.Relative));
        Assert.Equal(HttpStatusCode.Forbidden, response.StatusCode);

        var cached = await cache.TryGetAsync<string[]>(key);
        Assert.True(cached.HasValue);
        Assert.DoesNotContain(KeycloakPermission.FromScope(KeycloakScopes.Users.Search).PolicyName(), cached.Value);
        // The same token keeps answering from the cached set: a granted scope still passes without a new fetch.
        using var granted = await IamTestClient.Authorized(Factory, tokens).GetAsync(new Uri("/users/me", UriKind.Relative));
        Assert.Equal(HttpStatusCode.OK, granted.StatusCode);
    }

    [Fact]
    public async Task RevokeSession_PurgesCachedDecisionAndDeniesTheStillValidToken()
    {
        var phone = await IamTestClient.RegisterFreshUserAsync(Factory);
        var victim = await IamTestClient.LoginByPhoneAsync(Factory, phone);
        var other = await IamTestClient.LoginByPhoneAsync(Factory, phone, clientId: "mobile-app-2");
        var cache = Scope.ServiceProvider.GetRequiredService<IFusionCache>();
        var key = CacheKeys.For.AuthorizationPermissions(victim.Jti);

        using var before = await IamTestClient.Authorized(Factory, victim).GetAsync(new Uri("/users/me", UriKind.Relative));
        Assert.Equal(HttpStatusCode.OK, before.StatusCode);
        Assert.True((await cache.TryGetAsync<string[]>(key)).HasValue);

        using var revoke = await IamTestClient.Authorized(Factory, other)
            .DeleteAsync(new Uri($"/tokens/sessions/{victim.SessionId}", UriKind.Relative));
        Assert.Equal(HttpStatusCode.NoContent, revoke.StatusCode);

        // The JWT is still signed and unexpired, so authentication passes; authorization must re-ask Keycloak.
        Assert.False((await cache.TryGetAsync<string[]>(key)).HasValue);
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
            Assert.False((await cache.TryGetAsync<string[]>(CacheKeys.For.AuthorizationPermissions(login.Jti))).HasValue);
            using var after = await IamTestClient.Authorized(Factory, login).GetAsync(new Uri("/users/me", UriKind.Relative));
            Assert.Equal(HttpStatusCode.Forbidden, after.StatusCode);
        }
    }
}
