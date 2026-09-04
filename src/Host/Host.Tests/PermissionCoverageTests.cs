using System.Text.Json;
using Common.Tests;
using Common.Application.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Host.Tests;

// Guards against endpoints requiring a resource#scope that the realm does not declare (or that no permission
// covers), which otherwise 403s every caller silently. The realm JSON is the source of truth for both.
[Collection("Host")]
public class PermissionCoverageTests
{
    [Fact]
    public async Task AllEndpointScopes_AreDeclaredAndCoveredByAPermissionInTheRealm()
    {
        await using var factory = new HostTestFactory();
        await factory.InitializeAsync();
        _ = factory.CreateClient();

        var realm = LoadRealmAuthorization();

        var dataSource = factory.Services.GetRequiredService<EndpointDataSource>();
        var required = dataSource.Endpoints
            .SelectMany(e => e.Metadata.GetOrderedMetadata<IAuthorizeData>())
            .Select(a => a.Policy)
            .Distinct()
            .Select(policy => KeycloakPermission.TryParse(policy, out var permission) ? permission : (KeycloakPermission?)null)
            .OfType<KeycloakPermission>()
            .ToList();

        Assert.NotEmpty(required);
        Assert.All(required, permission =>
        {
            Assert.True(realm.ScopesByResource.TryGetValue(permission.Resource, out var scopes),
                $"Resource '{permission.Resource}' is not declared on backend-api.");
            Assert.Contains(permission.Scope, scopes);
            Assert.Contains(permission.Scope, realm.ScopesWithAPermission);
        });
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
}
