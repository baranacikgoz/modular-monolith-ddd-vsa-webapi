using System.Net;
using Common.Application.Auth;
using Common.Application.Caching;
using Common.Domain.StronglyTypedIds;
using Common.Tests;
using IAM.Application.Keycloak;
using Microsoft.Extensions.DependencyInjection;
using Xunit;
using ZiggyCreatures.Caching.Fusion;

namespace IAM.Tests.Endpoints.Auth;

/// <summary>
///     Keycloak's decisions for the stock and warehouse permissions of the Inventory module, against the real realm import:
///     the <c>store-owner</c> role is what lets a caller connect a store, the administration scope is held by the admin role
///     alone, and the own-data scope by every authenticated user.
/// </summary>
[Collection("IntegrationTestCollection")]
public class StoreOwnerPermissionDecisionTests(IntegrationTestWebAppFactory factory) : BaseIntegrationTest(factory)
{
    private static string PolicyOf(string scope) => KeycloakPermission.FromScope(scope).PolicyName();

    private async Task<bool> DecideAsync(TokenPair tokens, string scope)
    {
        var permissionClient = Scope.ServiceProvider.GetRequiredService<IKeycloakPermissionClient>();
        return await permissionClient.DecideAsync(tokens.AccessToken, PolicyOf(scope), CancellationToken.None);
    }

    private async Task AssignStoreOwnerRoleAsync(TokenPair tokens)
    {
        var adminClient = Scope.ServiceProvider.GetRequiredService<IKeycloakAdminClient>();
        var assigned = await adminClient.AssignRealmRoleAsync(
            new ApplicationUserId(Guid.Parse(tokens.Subject)), KeycloakRoles.StoreOwner, CancellationToken.None);
        Assert.False(assigned.IsFailure);
    }

    private async Task<string[]> CachedPermissionsOfAsync(TokenPair tokens)
    {
        var cache = Scope.ServiceProvider.GetRequiredService<IFusionCache>();
        var cached = await cache.TryGetAsync<string[]>(CacheKeys.For.AuthorizationPermissions(tokens.Jti));
        Assert.True(cached.HasValue);
        return cached.Value;
    }

    private async Task WarmPermissionCacheAsync(TokenPair tokens)
    {
        using var response = await IamTestClient.Authorized(Factory, tokens).GetAsync(new Uri("/users/me", UriKind.Relative));
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task WarehouseConnectionsConnect_UserWithoutTheStoreOwnerRole_IsDenied()
    {
        var phone = await IamTestClient.RegisterFreshUserAsync(Factory);
        var tokens = await IamTestClient.LoginByPhoneAsync(Factory, phone);

        Assert.False(await DecideAsync(tokens, KeycloakScopes.WarehouseConnections.Connect));
    }

    [Fact]
    public async Task WarehouseConnectionsConnect_AfterTheStoreOwnerRoleIsAssigned_IsGrantedToANewLogin()
    {
        var phone = await IamTestClient.RegisterFreshUserAsync(Factory);
        var beforeApproval = await IamTestClient.LoginByPhoneAsync(Factory, phone);
        Assert.False(await DecideAsync(beforeApproval, KeycloakScopes.WarehouseConnections.Connect));

        await AssignStoreOwnerRoleAsync(beforeApproval);

        var afterApproval = await IamTestClient.LoginByPhoneAsync(Factory, phone, clientId: "mobile-app-2");
        Assert.True(await DecideAsync(afterApproval, KeycloakScopes.WarehouseConnections.Connect));
    }

    // The role grants the permission, not identity: an administrator who is not a store owner cannot use the owner's route,
    // and the owner role does not widen anything else (the administration permissions stay with the admin role).
    [Fact]
    public async Task WarehouseConnectionsConnect_AdministratorWithoutTheStoreOwnerRole_IsDenied()
    {
        var tokens = await IamTestClient.LoginByPhoneAsync(Factory, SeedUsers.AdminPhone);

        Assert.False(await DecideAsync(tokens, KeycloakScopes.WarehouseConnections.Connect));
        Assert.True(await DecideAsync(tokens, KeycloakScopes.WarehouseConnections.Create));
    }

    [Fact]
    public async Task StoreOwnerRole_DoesNotGrantTheAdministrationScopes()
    {
        var phone = await IamTestClient.RegisterFreshUserAsync(Factory);
        var first = await IamTestClient.LoginByPhoneAsync(Factory, phone);
        await AssignStoreOwnerRoleAsync(first);
        var owner = await IamTestClient.LoginByPhoneAsync(Factory, phone, clientId: "mobile-app-2");

        Assert.True(await DecideAsync(owner, KeycloakScopes.WarehouseConnections.Connect));
        Assert.False(await DecideAsync(owner, KeycloakScopes.WarehouseConnections.Create));
        Assert.False(await DecideAsync(owner, KeycloakScopes.StockLevels.ViewAll));
    }

    // The API keeps the permissions Keycloak granted a token for the token's life (see KeycloakPermissionAuthorizationHandler), so
    // a store owner approved after they signed in is only recognised by a token issued after the role was assigned.
    [Fact]
    public async Task StoreOwnerRoleAssignedAfterSignIn_IsOnlySeenByTheCachedPermissionsOfANewToken()
    {
        var phone = await IamTestClient.RegisterFreshUserAsync(Factory);
        var signedInBeforeApproval = await IamTestClient.LoginByPhoneAsync(Factory, phone);
        await WarmPermissionCacheAsync(signedInBeforeApproval);

        await AssignStoreOwnerRoleAsync(signedInBeforeApproval);

        Assert.DoesNotContain(PolicyOf(KeycloakScopes.WarehouseConnections.Connect), await CachedPermissionsOfAsync(signedInBeforeApproval));
        var signedInAfterApproval = await IamTestClient.LoginByPhoneAsync(Factory, phone, clientId: "mobile-app-2");
        await WarmPermissionCacheAsync(signedInAfterApproval);
        Assert.Contains(PolicyOf(KeycloakScopes.WarehouseConnections.Connect), await CachedPermissionsOfAsync(signedInAfterApproval));
    }

    // Reading any store's stock data is administration; reading one's own is open to every authenticated role (system-admin
    // holds no basic role, so this also proves the own-data permission applies "Any Authenticated User Policy").
    [Theory]
    [InlineData(SeedUsers.AdminPhone, true)]
    [InlineData(SeedUsers.BasicPhone, false)]
    public async Task StockLevelsViewAll_IsGrantedToTheAdministratorOnly(string phone, bool expected)
    {
        var tokens = await IamTestClient.LoginByPhoneAsync(Factory, phone);

        Assert.Equal(expected, await DecideAsync(tokens, KeycloakScopes.StockLevels.ViewAll));
    }

    [Theory]
    [InlineData(SeedUsers.AdminPhone)]
    [InlineData(SeedUsers.BasicPhone)]
    public async Task StockLevelsViewOwn_IsGrantedToEveryAuthenticatedRole(string phone)
    {
        var tokens = await IamTestClient.LoginByPhoneAsync(Factory, phone);

        Assert.True(await DecideAsync(tokens, KeycloakScopes.StockLevels.ViewOwn));
    }
}
