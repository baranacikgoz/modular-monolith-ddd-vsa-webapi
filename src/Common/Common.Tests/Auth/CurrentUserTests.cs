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
    public void Authenticated_ReadsKeycloakClaims()
    {
        var subject = Guid.NewGuid();
        var identity = new ClaimsIdentity(
        [
            new Claim(JwtClaimNames.Subject, subject.ToString()),
            new Claim(JwtClaimNames.SessionId, "BiSvNj4XKcfnudoJmp30KZbd"),
            new Claim(JwtClaimNames.Roles, KeycloakRoles.SystemAdmin),
            new Claim(JwtClaimNames.Roles, KeycloakRoles.Staff)
        ], "Test");
        var currentUser = CreateCurrentUser(new ClaimsPrincipal(identity));

        Assert.Equal(subject, currentUser.Id.Value);
        Assert.Equal(subject.ToString(), currentUser.IdAsString);
        Assert.Equal("BiSvNj4XKcfnudoJmp30KZbd", currentUser.SessionId);
        Assert.Equal([KeycloakRoles.SystemAdmin, KeycloakRoles.Staff], currentUser.Roles);
    }

    [Fact]
    public void ServiceAccount_NoSidClaim_SessionIdIsNull()
    {
        var identity = new ClaimsIdentity(
        [
            new Claim(JwtClaimNames.Subject, Guid.NewGuid().ToString())
        ], "Test");
        var currentUser = CreateCurrentUser(new ClaimsPrincipal(identity));

        Assert.Null(currentUser.SessionId);
        Assert.Empty(currentUser.Roles);
        Assert.False(currentUser.Id.IsEmpty);
    }

    [Fact]
    public void Unauthenticated_EmptyIdentity()
    {
        var identity = new ClaimsIdentity(
        [
            new Claim(JwtClaimNames.Subject, Guid.NewGuid().ToString()),
            new Claim(JwtClaimNames.Roles, KeycloakRoles.Basic)
        ]); // no authenticationType => IsAuthenticated == false
        var currentUser = CreateCurrentUser(new ClaimsPrincipal(identity));

        Assert.True(currentUser.Id.IsEmpty);
        Assert.Equal(string.Empty, currentUser.IdAsString);
        Assert.Null(currentUser.SessionId);
        Assert.Empty(currentUser.Roles);
    }

    [Fact]
    public void MalformedSubject_IdIsEmpty()
    {
        var identity = new ClaimsIdentity([new Claim(JwtClaimNames.Subject, "not-a-guid")], "Test");
        var currentUser = CreateCurrentUser(new ClaimsPrincipal(identity));

        Assert.True(currentUser.Id.IsEmpty);
    }
}
