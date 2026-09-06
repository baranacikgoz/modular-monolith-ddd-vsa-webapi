using Common.Domain.StronglyTypedIds;
using Common.InterModuleRequests.Contracts;
using Common.InterModuleRequests.IAM;
using Common.InterModuleRequests.Notifications;
using Common.Tests;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
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
}
