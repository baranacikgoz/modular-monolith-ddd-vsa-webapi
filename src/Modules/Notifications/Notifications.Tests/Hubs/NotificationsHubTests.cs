using System.Security.Claims;
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
        _context.User.Returns(new ClaimsPrincipal(new ClaimsIdentity()));

        await _hub.OnConnectedAsync();

        await _groups.Received(1).AddToGroupAsync(connId, $"notifications:user:{userId}", Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task OnConnectedAsync_UserWithRoles_AddsToRoleGroups()
    {
        const string userId = "user-456";
        const string connId = "conn-2";
        _context.UserIdentifier.Returns(userId);
        _context.ConnectionId.Returns(connId);
        _context.User.Returns(new ClaimsPrincipal(new ClaimsIdentity(
        [
            new Claim(ClaimTypes.Role, "Admin"),
            new Claim(ClaimTypes.Role, "Manager")
        ])));

        await _hub.OnConnectedAsync();

        await _groups.Received(1).AddToGroupAsync(connId, "notifications:role:Admin", Arg.Any<CancellationToken>());
        await _groups.Received(1).AddToGroupAsync(connId, "notifications:role:Manager", Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task OnConnectedAsync_UnauthenticatedUser_ThrowsHubException()
    {
        _context.UserIdentifier.Returns((string?)null);

        await Assert.ThrowsAsync<HubException>(() => _hub.OnConnectedAsync());
    }

    [Fact]
    public async Task SubscribeAsync_UserGroupPrefix_ThrowsHubException()
    {
        await Assert.ThrowsAsync<HubException>(() => _hub.SubscribeAsync("notifications:user:123"));
    }

    [Fact]
    public async Task SubscribeAsync_RoleGroupPrefix_ThrowsHubException()
    {
        await Assert.ThrowsAsync<HubException>(() => _hub.SubscribeAsync("notifications:role:Admin"));
    }

    [Fact]
    public async Task SubscribeAsync_ResourceGroup_AddsToGroup()
    {
        const string connId = "conn-3";
        _context.ConnectionId.Returns(connId);

        await _hub.SubscribeAsync("notifications:product:abc");

        await _groups.Received(1).AddToGroupAsync(connId, "notifications:product:abc", Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task UnsubscribeAsync_UserGroupPrefix_ThrowsHubException()
    {
        await Assert.ThrowsAsync<HubException>(() => _hub.UnsubscribeAsync("notifications:user:123"));
    }

    [Fact]
    public async Task UnsubscribeAsync_RoleGroupPrefix_ThrowsHubException()
    {
        await Assert.ThrowsAsync<HubException>(() => _hub.UnsubscribeAsync("notifications:role:Admin"));
    }

    [Fact]
    public async Task UnsubscribeAsync_ResourceGroup_RemovesFromGroup()
    {
        const string connId = "conn-4";
        _context.ConnectionId.Returns(connId);

        await _hub.UnsubscribeAsync("notifications:product:abc");

        await _groups.Received(1).RemoveFromGroupAsync(connId, "notifications:product:abc", Arg.Any<CancellationToken>());
    }
}
