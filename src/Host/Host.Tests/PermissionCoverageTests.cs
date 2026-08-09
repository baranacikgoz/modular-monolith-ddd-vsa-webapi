using Common.Application.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Host.Tests;

// Guards against endpoints requiring a Permissions.* policy that no role actually grants
// (CustomPermissions._basic / _systemAdmin), which otherwise 403s every caller silently.
[Collection("Host")]
public class PermissionCoverageTests
{
    [Fact]
    public async Task AllEndpointPermissionPolicies_AreGrantedBySomeRole()
    {
        await using var factory = new HostTestFactory();
        await factory.InitializeAsync();
        _ = factory.CreateClient();

        var grantedPermissions = CustomPermissions.ForRoles(CustomRoles.All);

        var dataSource = factory.Services.GetRequiredService<EndpointDataSource>();
        var requiredPermissions = dataSource.Endpoints
            .SelectMany(e => e.Metadata.GetOrderedMetadata<IAuthorizeData>())
            .Select(a => a.Policy)
            .Where(policy => policy is not null && policy.StartsWith("Permissions.", StringComparison.Ordinal))
            .Distinct()
            .ToList();

        Assert.NotEmpty(requiredPermissions);
        Assert.All(requiredPermissions, policy => Assert.Contains(policy!, grantedPermissions));
    }
}
