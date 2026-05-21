using Common.Domain.StronglyTypedIds;
using Microsoft.AspNetCore.SignalR;
using Notifications.Application.Hubs;
using Notifications.Infrastructure.Hubs;
using NSubstitute;
using Xunit;

namespace Notifications.Tests.Hubs;

public sealed class NotificationDispatcherTests
{
    private readonly IHubContext<NotificationsHub, INotificationsClient> _hubContext;
    private readonly IHubClients<INotificationsClient> _clients;
    private readonly INotificationsClient _proxy;
    private readonly SignalRNotificationDispatcher _dispatcher;

    public NotificationDispatcherTests()
    {
        _hubContext = Substitute.For<IHubContext<NotificationsHub, INotificationsClient>>();
        _clients = Substitute.For<IHubClients<INotificationsClient>>();
        _proxy = Substitute.For<INotificationsClient>();

        _hubContext.Clients.Returns(_clients);
        _clients.Group(Arg.Any<string>()).Returns(_proxy);
        _clients.All.Returns(_proxy);
        _clients.AllExcept(Arg.Any<IReadOnlyList<string>>()).Returns(_proxy);

        _dispatcher = new SignalRNotificationDispatcher(_hubContext);
    }

    [Fact]
    public async Task SendToUserAsync_CallsCorrectGroup()
    {
        var userId = ApplicationUserId.New();
        var payload = new NotificationPayload("product.price.updated", "prod-1");
        var expectedGroup = $"notifications:user:{userId}";

        await _dispatcher.SendToUserAsync(userId, payload, CancellationToken.None);

        _clients.Received(1).Group(expectedGroup);
        await _proxy.Received(1).ReceiveNotification(payload);
    }

    [Fact]
    public async Task SendToGroupAsync_CallsCorrectGroup()
    {
        const string groupName = "notifications:product:abc";
        var payload = new NotificationPayload("product.price.updated", "abc");

        await _dispatcher.SendToGroupAsync(groupName, payload, CancellationToken.None);

        _clients.Received(1).Group(groupName);
        await _proxy.Received(1).ReceiveNotification(payload);
    }

    [Fact]
    public async Task SendToAllAsync_CallsAllClients()
    {
        var payload = new NotificationPayload("system.maintenance");

        await _dispatcher.SendToAllAsync(payload, CancellationToken.None);

        _ = _clients.Received(1).All;
        await _proxy.Received(1).ReceiveNotification(payload);
    }

    [Fact]
    public async Task SendToAllExceptAsync_ExcludesSpecifiedConnections()
    {
        var excluded = new List<string> { "conn-1", "conn-2" };
        var payload = new NotificationPayload("system.alert");

        await _dispatcher.SendToAllExceptAsync(excluded, payload, CancellationToken.None);

        _clients.Received(1).AllExcept(Arg.Is<IReadOnlyList<string>>(l =>
            l.Count == 2 && l.Contains("conn-1") && l.Contains("conn-2")));
        await _proxy.Received(1).ReceiveNotification(payload);
    }
}
