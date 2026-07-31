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
}
