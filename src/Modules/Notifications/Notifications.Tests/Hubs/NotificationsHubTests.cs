using System.Security.Claims;
using Common.Application.Auth;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using Notifications.Application.Hubs;
using Notifications.Infrastructure.Hubs;
using NSubstitute;
using Xunit;

namespace Notifications.Tests.Hubs;

public sealed class NotificationsHubTests : IDisposable
{
    private readonly NotificationsHub _hub;
    private readonly IGroupManager _groups;
    private readonly HubCallerContext _context;

    public NotificationsHubTests()
    {
        var logger = Substitute.For<ILogger<NotificationsHub>>();
        _hub = new NotificationsHub(logger);
        _groups = Substitute.For<IGroupManager>();
        _context = Substitute.For<HubCallerContext>();
        _hub.Groups = _groups;
        _hub.Context = _context;
        _hub.Clients = Substitute.For<IHubCallerClients<INotificationsClient>>();
    }

    public void Dispose() => _hub.Dispose();

    [Fact]
    public async Task OnConnectedAsync_AuthenticatedUser_AddsToUserGroup()
    {
        const string userId = "user-123";
        const string connId = "conn-1";
        _context.UserIdentifier.Returns(userId);
        _context.ConnectionId.Returns(connId);

        await _hub.OnConnectedAsync();

        await _groups.Received(1).AddToGroupAsync(connId, $"notifications:user:{userId}", Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task OnConnectedAsync_UnauthenticatedUser_ThrowsHubException()
    {
        _context.UserIdentifier.Returns((string?)null);

        await Assert.ThrowsAsync<HubException>(() => _hub.OnConnectedAsync());
    }

    private static ClaimsPrincipal PrincipalWithRoles(params string[] roles)
    {
        var identity = new ClaimsIdentity(
            roles.Select(role => new Claim(JwtClaimNames.Roles, role)), "test", nameType: null, roleType: JwtClaimNames.Roles);
        return new ClaimsPrincipal(identity);
    }

    /// <summary>Critical alerts addressed to a role are pushed to that role's group, so a connection joins one group per role it holds.</summary>
    [Fact]
    public async Task OnConnectedAsync_UserWithRoles_AlsoJoinsOneGroupPerRole()
    {
        _context.UserIdentifier.Returns("user-1");
        _context.ConnectionId.Returns("conn-1");
        _context.User.Returns(PrincipalWithRoles(KeycloakRoles.Staff, KeycloakRoles.StoreOwner, KeycloakRoles.Staff));

        await _hub.OnConnectedAsync();

        await _groups.Received(1).AddToGroupAsync("conn-1", NotificationGroupName.ForRole(KeycloakRoles.Staff), Arg.Any<CancellationToken>());
        await _groups.Received(1).AddToGroupAsync("conn-1", NotificationGroupName.ForRole(KeycloakRoles.StoreOwner), Arg.Any<CancellationToken>());
        await _groups.Received(3).AddToGroupAsync("conn-1", Arg.Any<string>(), Arg.Any<CancellationToken>());
    }

    /// <summary>The roles claim of a user Keycloak created also carries the realm's built-in roles; nobody addresses an alert to them.</summary>
    [Fact]
    public async Task OnConnectedAsync_KeycloakBuiltInRoles_JoinNoGroup()
    {
        _context.UserIdentifier.Returns("user-3");
        _context.ConnectionId.Returns("conn-3");
        _context.User.Returns(PrincipalWithRoles(
            "offline_access", "uma_authorization", KeycloakRoles.Basic, "default-roles-modular-monolith", KeycloakRoles.Staff));

        await _hub.OnConnectedAsync();

        await _groups.Received(1).AddToGroupAsync("conn-3", NotificationGroupName.ForUser("user-3"), Arg.Any<CancellationToken>());
        await _groups.Received(1).AddToGroupAsync("conn-3", NotificationGroupName.ForRole(KeycloakRoles.Basic), Arg.Any<CancellationToken>());
        await _groups.Received(1).AddToGroupAsync("conn-3", NotificationGroupName.ForRole(KeycloakRoles.Staff), Arg.Any<CancellationToken>());
        await _groups.Received(3).AddToGroupAsync("conn-3", Arg.Any<string>(), Arg.Any<CancellationToken>());
    }

    [Theory]
    [InlineData("offline_access")]
    [InlineData("uma_authorization")]
    [InlineData("default-roles-modular-monolith")]
    [InlineData("default-roles-another-realm")]
    public void IsAddressableRole_KeycloakBuiltInRole_IsFalse(string role)
    {
        Assert.False(NotificationGroupName.IsAddressableRole(role));
    }

    [Theory]
    [InlineData(KeycloakRoles.Basic)]
    [InlineData(KeycloakRoles.Staff)]
    [InlineData(KeycloakRoles.SystemAdmin)]
    [InlineData(KeycloakRoles.StoreOwner)]
    [InlineData("a-role-an-operator-added")]
    [InlineData("my-default-roles-team")]
    public void IsAddressableRole_ApplicationOrOperatorRole_IsTrue(string role)
    {
        Assert.True(NotificationGroupName.IsAddressableRole(role));
    }

    [Fact]
    public async Task OnConnectedAsync_UserWithoutRoles_JoinsOnlyTheUserGroup()
    {
        _context.UserIdentifier.Returns("user-2");
        _context.ConnectionId.Returns("conn-2");
        _context.User.Returns(PrincipalWithRoles());

        await _hub.OnConnectedAsync();

        await _groups.Received(1).AddToGroupAsync("conn-2", Arg.Any<string>(), Arg.Any<CancellationToken>());
    }
}
