using System.Security.Claims;
using Common.Application.Auth;
using Common.Infrastructure.Auth.Services;
using Xunit;

#pragma warning disable CA1515, CA1707

namespace Common.Tests.Auth;

public sealed class CurrentUserTests
{
    [Fact]
    public void HasPermission_RoleGrantsIt_ReturnsTrue()
    {
        var permission = CustomPermission.NameFor(CustomActions.Read, CustomResources.ApplicationUsers);
        var identity = new ClaimsIdentity(
        [
            new Claim(ClaimTypes.NameIdentifier, Guid.NewGuid().ToString()),
            new Claim(JwtClaimNames.Roles, CustomRoles.SystemAdmin)
        ], "Test");
        var currentUser = new CurrentUser(new ClaimsPrincipal(identity));

        Assert.True(currentUser.HasPermission(CustomPermission.NameFor(CustomActions.Manage, CustomResources.Hangfire)));
        Assert.True(currentUser.HasPermission(permission));
    }

    [Fact]
    public void HasPermission_DirectPermissionClaim_ReturnsTrue()
    {
        // API-key principals carry no role — permission comes straight from a "permission" claim.
        var permission = CustomPermission.NameFor(CustomActions.Manage, CustomResources.Hangfire);
        var identity = new ClaimsIdentity(
        [
            new Claim(JwtClaimNames.Permission, permission)
        ], "Test");
        var currentUser = new CurrentUser(new ClaimsPrincipal(identity));

        Assert.True(currentUser.HasPermission(permission));
        Assert.Empty(currentUser.Roles);
    }

    [Fact]
    public void HasPermission_NeitherRoleNorDirectClaimGrantsIt_ReturnsFalse()
    {
        var identity = new ClaimsIdentity(
        [
            new Claim(ClaimTypes.NameIdentifier, Guid.NewGuid().ToString()),
            new Claim(JwtClaimNames.Roles, CustomRoles.Basic)
        ], "Test");
        var currentUser = new CurrentUser(new ClaimsPrincipal(identity));

        Assert.False(currentUser.HasPermission(CustomPermission.NameFor(CustomActions.Manage, CustomResources.Hangfire)));
    }

    [Fact]
    public void HasPermission_Unauthenticated_ReturnsFalse()
    {
        var permission = CustomPermission.NameFor(CustomActions.Manage, CustomResources.Hangfire);
        var identity = new ClaimsIdentity(
        [
            new Claim(JwtClaimNames.Permission, permission)
        ]); // no authenticationType => IsAuthenticated == false
        var currentUser = new CurrentUser(new ClaimsPrincipal(identity));

        Assert.False(currentUser.HasPermission(permission));
    }
}
