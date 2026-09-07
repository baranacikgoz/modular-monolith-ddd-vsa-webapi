using System.Text.Json;
using Common.Tests;
using Common.Application.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Host.Tests;

// Guards both directions between endpoints and the realm JSON (the source of truth): an endpoint requiring a
// resource#scope the realm does not declare (or no permission covers) 403s every caller silently, and a realm
// scope no endpoint requires is a dropped endpoint or dead configuration nobody notices.
[Collection("Host")]
public class PermissionCoverageTests
{
    // Enforced outside endpoint metadata (HangfireCustomAuthorizationFilter asks IAuthorizationService directly).
    private static readonly HashSet<string> ScopesEnforcedOutsideEndpoints = new(StringComparer.Ordinal)
    {
        KeycloakScopes.Hangfire.Manage
    };

    [Fact]
    public async Task AllEndpointScopes_AreDeclaredAndCoveredByAPermissionInTheRealm()
    {
        await using var factory = new HostTestFactory();
        await factory.InitializeAsync();
        _ = factory.CreateClient();

        var realm = LoadRealmAuthorization();
        var required = CollectEndpointPermissions(factory);

        Assert.NotEmpty(required);
        Assert.All(required, permission =>
        {
            Assert.True(realm.ScopesByResource.TryGetValue(permission.Resource, out var scopes),
                $"Resource '{permission.Resource}' is not declared on backend-api.");
            Assert.Contains(permission.Scope, scopes);
            Assert.Contains(permission.Scope, realm.ScopesWithAPermission);
        });
    }

    [Fact]
    public async Task AllRealmScopes_AreRequiredBySomeEndpoint()
    {
        await using var factory = new HostTestFactory();
        await factory.InitializeAsync();
        _ = factory.CreateClient();

        var realm = LoadRealmAuthorization();
        var requiredScopes = CollectEndpointPermissions(factory)
            .Select(p => p.Scope)
            .ToHashSet(StringComparer.Ordinal);

        var unused = realm.ScopesByResource.Values
            .SelectMany(scopes => scopes)
            .Where(scope => !requiredScopes.Contains(scope) && !ScopesEnforcedOutsideEndpoints.Contains(scope))
            .Order(StringComparer.Ordinal)
            .ToList();

        Assert.True(unused.Count == 0,
            $"Realm declares scopes no endpoint requires: {string.Join(", ", unused)}. Map an endpoint or drop them from the realm.");
    }

    // A "-own" scope is decided against the caller's own resource ownership in the query, not the role: every
    // role must be allowed to reach the endpoint so ownership can be checked. "System Admin Role Policy" or
    // "Basic Role Policy" alone silently 403s the other roles before ownership is ever evaluated (PR #149 #2).
    [Fact]
    public void AllOwnScopePermissions_ApplyAnyAuthenticatedUserPolicy()
    {
        var misconfigured = LoadScopePermissions()
            .Where(p => p.Scopes.Any(s => s.EndsWith("-own", StringComparison.Ordinal)))
            .Where(p => !p.ApplyPolicies.Contains("Any Authenticated User Policy", StringComparer.Ordinal))
            .Select(p => p.Name)
            .Order(StringComparer.Ordinal)
            .ToList();

        Assert.True(misconfigured.Count == 0,
            $"Permissions covering a '-own' scope must apply 'Any Authenticated User Policy': {string.Join(", ", misconfigured)}.");
    }

    private static List<KeycloakPermission> CollectEndpointPermissions(HostTestFactory factory)
    {
        var dataSource = factory.Services.GetRequiredService<EndpointDataSource>();
        return dataSource.Endpoints
            .SelectMany(e => e.Metadata.GetOrderedMetadata<IAuthorizeData>())
            .Select(a => a.Policy)
            .Distinct()
            .Select(policy => KeycloakPermission.TryParse(policy, out var permission) ? permission : (KeycloakPermission?)null)
            .OfType<KeycloakPermission>()
            .ToList();
    }

    private static (IReadOnlyDictionary<string, HashSet<string>> ScopesByResource, HashSet<string> ScopesWithAPermission)
        LoadRealmAuthorization()
    {
        using var document = JsonDocument.Parse(File.ReadAllText(TestPaths.RealmFile));
        var backendApi = document.RootElement.GetProperty("clients").EnumerateArray()
            .Single(c => c.GetProperty("clientId").GetString() == "backend-api");
        var authorization = backendApi.GetProperty("authorizationSettings");

        var scopesByResource = authorization.GetProperty("resources").EnumerateArray()
            .ToDictionary(
                r => r.GetProperty("name").GetString()!,
                r => r.GetProperty("scopes").EnumerateArray().Select(s => s.GetProperty("name").GetString()!).ToHashSet(StringComparer.Ordinal),
                StringComparer.Ordinal);

        var scopesWithAPermission = authorization.GetProperty("policies").EnumerateArray()
            .Where(p => p.GetProperty("type").GetString() == "scope")
            .SelectMany(p => JsonSerializer.Deserialize<string[]>(p.GetProperty("config").GetProperty("scopes").GetString()!)!)
            .ToHashSet(StringComparer.Ordinal);

        return (scopesByResource, scopesWithAPermission);
    }

    private static List<ScopePermission> LoadScopePermissions()
    {
        using var document = JsonDocument.Parse(File.ReadAllText(TestPaths.RealmFile));
        var backendApi = document.RootElement.GetProperty("clients").EnumerateArray()
            .Single(c => c.GetProperty("clientId").GetString() == "backend-api");
        var authorization = backendApi.GetProperty("authorizationSettings");

        return authorization.GetProperty("policies").EnumerateArray()
            .Where(p => p.GetProperty("type").GetString() == "scope")
            .Select(p => new ScopePermission(
                p.GetProperty("name").GetString()!,
                JsonSerializer.Deserialize<string[]>(p.GetProperty("config").GetProperty("scopes").GetString()!)!,
                JsonSerializer.Deserialize<string[]>(p.GetProperty("config").GetProperty("applyPolicies").GetString()!)!))
            .ToList();
    }

    private sealed record ScopePermission(string Name, string[] Scopes, string[] ApplyPolicies);
}
