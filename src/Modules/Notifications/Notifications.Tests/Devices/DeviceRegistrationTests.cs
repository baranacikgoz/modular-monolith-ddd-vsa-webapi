using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using Common.Domain.StronglyTypedIds;
using Common.InterModuleRequests.Contracts;
using Common.InterModuleRequests.Notifications;
using Common.Tests;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Notifications.Application.Persistence;
using Notifications.Infrastructure.Devices.UpdateCurrentPushToken;
using Xunit;

namespace Notifications.Tests.Devices;

[Collection("IntegrationTestCollection")]
public sealed class DeviceRegistrationTests(NotificationsTestFactory factory) : BaseIntegrationTest(factory)
{
    private IInterModuleRequestClient<BindDeviceSessionRequest, BindDeviceSessionResponse> BindClient =>
        Scope.ServiceProvider.GetRequiredService<IInterModuleRequestClient<BindDeviceSessionRequest, BindDeviceSessionResponse>>();

    private IInterModuleRequestClient<DeactivateDeviceSessionsRequest, DeactivateDeviceSessionsResponse> DeactivateClient =>
        Scope.ServiceProvider.GetRequiredService<IInterModuleRequestClient<DeactivateDeviceSessionsRequest, DeactivateDeviceSessionsResponse>>();

    private IInterModuleRequestClient<GetDeviceSessionsRequest, GetDeviceSessionsResponse> GetClient =>
        Scope.ServiceProvider.GetRequiredService<IInterModuleRequestClient<GetDeviceSessionsRequest, GetDeviceSessionsResponse>>();

    [Fact]
    public async Task Bind_FirstLoginOnDevice_CreatesActiveRegistration()
    {
        var userId = ApplicationUserId.New();
        var deviceId = Guid.NewGuid();

        var response = await BindClient.SendAsync(
            new BindDeviceSessionRequest(userId, "sid-1", deviceId, "mobile-app-1", "Pixel", "fcm-1"),
            CancellationToken.None);

        Assert.Null(response.SupersededSessionId);

        using var scope = Factory.Services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<INotificationsDbContext>();
        var registration = await db.DeviceRegistrations.SingleAsync(r => r.UserId == userId);
        Assert.Equal("sid-1", registration.SessionId);
        Assert.Equal(deviceId, registration.DeviceId);
        Assert.Equal("mobile-app-1", registration.ClientId);
        Assert.Equal("Pixel", registration.DeviceName);
        Assert.Equal("fcm-1", registration.PushToken);
        Assert.True(registration.IsActive);
    }

    [Fact]
    public async Task Bind_SameDeviceAgain_RebindsAndReportsSupersededSession()
    {
        var userId = ApplicationUserId.New();
        var deviceId = Guid.NewGuid();
        await BindClient.SendAsync(
            new BindDeviceSessionRequest(userId, "sid-old", deviceId, "mobile-app-1", "Pixel", "fcm-1"),
            CancellationToken.None);

        var response = await BindClient.SendAsync(
            new BindDeviceSessionRequest(userId, "sid-new", deviceId, "mobile-app-1", DeviceName: null, PushToken: null),
            CancellationToken.None);

        Assert.Equal("sid-old", response.SupersededSessionId);

        using var scope = Factory.Services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<INotificationsDbContext>();
        var registration = await db.DeviceRegistrations.SingleAsync(r => r.UserId == userId);
        Assert.Equal("sid-new", registration.SessionId);
        Assert.Equal("Pixel", registration.DeviceName); // omitted name must not wipe the stored one
        Assert.Equal("fcm-1", registration.PushToken);
        Assert.True(registration.IsActive);
    }

    [Fact]
    public async Task Bind_AfterDeactivation_DoesNotReportSupersededSession()
    {
        var userId = ApplicationUserId.New();
        var deviceId = Guid.NewGuid();
        await BindClient.SendAsync(
            new BindDeviceSessionRequest(userId, "sid-old", deviceId, "mobile-app-1", null, null), CancellationToken.None);
        await DeactivateClient.SendAsync(
            new DeactivateDeviceSessionsRequest(userId, ["sid-old"]), CancellationToken.None);

        var response = await BindClient.SendAsync(
            new BindDeviceSessionRequest(userId, "sid-new", deviceId, "mobile-app-1", null, null), CancellationToken.None);

        Assert.Null(response.SupersededSessionId);
    }

    [Fact]
    public async Task Deactivate_NullSessionIds_DeactivatesEveryRegistrationOfUser()
    {
        var userId = ApplicationUserId.New();
        await BindClient.SendAsync(
            new BindDeviceSessionRequest(userId, "sid-a", Guid.NewGuid(), "mobile-app-1", null, null), CancellationToken.None);
        await BindClient.SendAsync(
            new BindDeviceSessionRequest(userId, "sid-b", Guid.NewGuid(), "web-app-1", null, null), CancellationToken.None);

        await DeactivateClient.SendAsync(new DeactivateDeviceSessionsRequest(userId, SessionIds: null), CancellationToken.None);

        var sessions = await GetClient.SendAsync(new GetDeviceSessionsRequest(userId), CancellationToken.None);
        Assert.Empty(sessions.Sessions);
    }

    [Fact]
    public async Task GetDeviceSessions_ReturnsOnlyActiveRegistrationsOfThatUser()
    {
        var userId = ApplicationUserId.New();
        var otherUserId = ApplicationUserId.New();
        await BindClient.SendAsync(
            new BindDeviceSessionRequest(userId, "sid-a", Guid.NewGuid(), "mobile-app-1", "Phone", null), CancellationToken.None);
        await BindClient.SendAsync(
            new BindDeviceSessionRequest(userId, "sid-b", Guid.NewGuid(), "web-app-1", null, null), CancellationToken.None);
        await BindClient.SendAsync(
            new BindDeviceSessionRequest(otherUserId, "sid-c", Guid.NewGuid(), "mobile-app-1", null, null), CancellationToken.None);
        await DeactivateClient.SendAsync(new DeactivateDeviceSessionsRequest(userId, ["sid-b"]), CancellationToken.None);

        var sessions = await GetClient.SendAsync(new GetDeviceSessionsRequest(userId), CancellationToken.None);

        var single = Assert.Single(sessions.Sessions);
        Assert.Equal(new DeviceSession("sid-a", "mobile-app-1", "Phone"), single);
    }

    [Fact]
    public async Task UpdatePushToken_CurrentSessionRegistered_UpdatesToken()
    {
        var userId = ApplicationUserId.New();
        await BindClient.SendAsync(
            new BindDeviceSessionRequest(userId, "sid-current", Guid.NewGuid(), "mobile-app-1", null, "fcm-old"),
            CancellationToken.None);

        var client = CreateClientFor(userId, "sid-current");
        var response = await client.PutAsJsonAsync(
            new Uri("/notifications/devices/current/push-token", UriKind.Relative),
            new Request { PushToken = "fcm-new" });

        Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);

        using var scope = Factory.Services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<INotificationsDbContext>();
        var registration = await db.DeviceRegistrations.SingleAsync(r => r.UserId == userId);
        Assert.Equal("fcm-new", registration.PushToken);
        Assert.NotNull(registration.PushTokenUpdatedOn);
    }

    [Fact]
    public async Task UpdatePushToken_NoRegistrationForSession_Returns404()
    {
        var client = CreateClientFor(ApplicationUserId.New(), "sid-unknown");

        var response = await client.PutAsJsonAsync(
            new Uri("/notifications/devices/current/push-token", UriKind.Relative),
            new Request { PushToken = "fcm" });

        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]
    public async Task UpdatePushToken_Anonymous_Returns401()
    {
        var client = Factory.CreateClient();

        var response = await client.PutAsJsonAsync(
            new Uri("/notifications/devices/current/push-token", UriKind.Relative),
            new Request { PushToken = "fcm" });

        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
    }

    private HttpClient CreateClientFor(ApplicationUserId userId, string sessionId)
    {
        var client = Factory.CreateClient();
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "test");
        client.DefaultRequestHeaders.Add("X-Test-User-Id", userId.ToString());
        client.DefaultRequestHeaders.Add("X-Test-Session-Id", sessionId);
        return client;
    }
}
