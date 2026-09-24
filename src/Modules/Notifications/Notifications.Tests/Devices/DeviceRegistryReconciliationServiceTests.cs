using Common.Domain.StronglyTypedIds;
using Common.InterModuleRequests.Contracts;
using Common.InterModuleRequests.IAM;
using Common.InterModuleRequests.Notifications;
using Common.Application.Options;
using Common.Tests;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Options;
using Notifications.Application.Persistence;
using Notifications.Infrastructure.Devices;
using NSubstitute;
using Xunit;

namespace Notifications.Tests.Devices;

[Collection("IntegrationTestCollection")]
public sealed class DeviceRegistryReconciliationServiceTests(NotificationsTestFactory factory) : BaseIntegrationTest(factory)
{
    [Fact]
    public async Task ReconcileAsync_SessionMissingFromKeycloak_DeactivatesOnlyThatRegistration()
    {
        var userId = ApplicationUserId.New();
        var bindClient = Scope.ServiceProvider
            .GetRequiredService<IInterModuleRequestClient<BindDeviceSessionRequest, BindDeviceSessionResponse>>();
        await bindClient.SendAsync(
            new BindDeviceSessionRequest(userId, "sid-live", Guid.NewGuid(), "mobile-app-1", null, null), CancellationToken.None);
        await bindClient.SendAsync(
            new BindDeviceSessionRequest(userId, "sid-stale", Guid.NewGuid(), "mobile-app-2", null, null), CancellationToken.None);

        var sessionsClient = Scope.ServiceProvider
            .GetRequiredService<IInterModuleRequestClient<GetActiveSessionIdsRequest, GetActiveSessionIdsResponse>>();
        sessionsClient.SendAsync(Arg.Any<GetActiveSessionIdsRequest>(), Arg.Any<CancellationToken>())
            .Returns(new GetActiveSessionIdsResponse([new UserSessionIds(userId, ["sid-live"])]));

        var reconciler = Scope.ServiceProvider.GetRequiredService<DeviceRegistryReconciliationService>();
        await reconciler.ReconcileAsync(CancellationToken.None);

        var notificationsDb = Scope.ServiceProvider.GetRequiredService<INotificationsDbContext>();
        var registrations = await notificationsDb.DeviceRegistrations
            .AsNoTracking()
            .Where(r => r.UserId == userId)
            .ToDictionaryAsync(r => r.SessionId, r => r.IsActive);

        Assert.True(registrations["sid-live"]);
        Assert.False(registrations["sid-stale"]);
    }

    [Fact]
    public async Task ReconcileAsync_BatchSmallerThanRegistry_StampsCheckedRowsAndWalksLeastRecentlyCheckedFirst()
    {
        var first = ApplicationUserId.New();
        var second = ApplicationUserId.New();
        var bindClient = Scope.ServiceProvider
            .GetRequiredService<IInterModuleRequestClient<BindDeviceSessionRequest, BindDeviceSessionResponse>>();
        await bindClient.SendAsync(
            new BindDeviceSessionRequest(first, "sid-first", Guid.NewGuid(), "mobile-app-1", null, null), CancellationToken.None);

        var sessionsClient = Substitute.For<IInterModuleRequestClient<GetActiveSessionIdsRequest, GetActiveSessionIdsResponse>>();
        sessionsClient.SendAsync(Arg.Any<GetActiveSessionIdsRequest>(), Arg.Any<CancellationToken>())
            .Returns(call => new GetActiveSessionIdsResponse(
                call.Arg<GetActiveSessionIdsRequest>().UserIds.Select(id => new UserSessionIds(id, ["sid-first", "sid-second"])).ToList()));

        var batchOfOne = Options.Create(new DevicesOptions
        {
            AllowedClientIds = ["mobile-app-1"], ReconcileCron = "* * * * *", ReconcileBatchSize = 1
        });
        var notificationsDb = Scope.ServiceProvider.GetRequiredService<INotificationsDbContext>();
        var reconciler = new DeviceRegistryReconciliationService(notificationsDb, sessionsClient, batchOfOne,
            TimeProvider.System, NullLogger<DeviceRegistryReconciliationService>.Instance);

        // Run 1: only "first" exists, it gets stamped.
        await reconciler.ReconcileAsync(CancellationToken.None);
        var firstStamp = await notificationsDb.DeviceRegistrations.AsNoTracking()
            .Where(r => r.UserId == first).Select(r => r.LastReconciledOn).SingleAsync();
        Assert.NotNull(firstStamp);

        // Run 2: "second" was never checked, so a batch of one must pick it over the already-stamped "first".
        await bindClient.SendAsync(
            new BindDeviceSessionRequest(second, "sid-second", Guid.NewGuid(), "mobile-app-1", null, null), CancellationToken.None);
        sessionsClient.ClearReceivedCalls();
        await reconciler.ReconcileAsync(CancellationToken.None);
        await sessionsClient.Received(1).SendAsync(
            Arg.Is<GetActiveSessionIdsRequest>(r => r.UserIds.Single() == second), Arg.Any<CancellationToken>());

        // Run 3: both stamped, the older stamp ("first") goes first.
        sessionsClient.ClearReceivedCalls();
        await reconciler.ReconcileAsync(CancellationToken.None);
        await sessionsClient.Received(1).SendAsync(
            Arg.Is<GetActiveSessionIdsRequest>(r => r.UserIds.Single() == first), Arg.Any<CancellationToken>());
    }
}
