using Common.Application.Auth;
using Xunit;

#pragma warning disable CA1515, CA1707

namespace Common.Tests.Auth;

public sealed class KeycloakPermissionTests
{
    [Fact]
    public void PolicyName_RoundTrips()
    {
        var name = KeycloakPermission.FromScope(KeycloakScopes.Stores.CreateOwn).PolicyName();

        Assert.Equal("stores#stores:create-own", name);
        Assert.True(KeycloakPermission.TryParse(name, out var permission));
        Assert.Equal(new KeycloakPermission("stores", "stores:create-own"), permission);
        Assert.Equal(name, permission.PolicyName());
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("stores")]
    [InlineData("#stores:view")]
    [InlineData("stores#")]
    [InlineData("stores#a#b")]
    public void TryParse_RejectsNonPermissionPolicyNames(string? policyName)
    {
        Assert.False(KeycloakPermission.TryParse(policyName, out _));
    }
}
