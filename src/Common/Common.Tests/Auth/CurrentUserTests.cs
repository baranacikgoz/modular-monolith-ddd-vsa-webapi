using System.Security.Claims;
using Common.Application.Auth;
using Common.Infrastructure.Auth.Services;
using Microsoft.AspNetCore.Http;
using Xunit;

#pragma warning disable CA1515, CA1707

namespace Common.Tests.Auth;

public sealed class CurrentUserTests
{
    // CurrentUser reads HttpContext.User lazily through IHttpContextAccessor (not a constructor
    // snapshot), so tests build the same accessor wiring production DI uses.
    private static CurrentUser CreateCurrentUser(ClaimsPrincipal principal)
    {
        var accessor = new HttpContextAccessor { HttpContext = new DefaultHttpContext { User = principal } };
        return new CurrentUser(accessor);
    }

    [Fact]
    public void HasPermission_RoleGrantsIt_ReturnsTrue()
    {
        var permission = CustomPermission.NameFor(CustomActions.Read, CustomResources.ApplicationUsers);
        var identity = new ClaimsIdentity(
        [
            new Claim(ClaimTypes.NameIdentifier, Guid.NewGuid().ToString()),
            new Claim(JwtClaimNames.Roles, CustomRoles.SystemAdmin)
        ], "Test");
        var currentUser = CreateCurrentUser(new ClaimsPrincipal(identity));

        Assert.True(currentUser.HasPermission(CustomPermission.NameFor(CustomActions.Manage, CustomResources.Hangfire)));
        Assert.True(currentUser.HasPermission(permission));
    }

    [Fact]
    public void HasPermission_DirectPermissionClaim_ReturnsTrue()
    {
        // API-key principals carry no role: permission comes straight from a "permission" claim.
        var permission = CustomPermission.NameFor(CustomActions.Manage, CustomResources.Hangfire);
        var identity = new ClaimsIdentity(
        [
            new Claim(JwtClaimNames.Permission, permission)
        ], "Test");
        var currentUser = CreateCurrentUser(new ClaimsPrincipal(identity));

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
        var currentUser = CreateCurrentUser(new ClaimsPrincipal(identity));

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
        var currentUser = CreateCurrentUser(new ClaimsPrincipal(identity));

        Assert.False(currentUser.HasPermission(permission));
    }
}
