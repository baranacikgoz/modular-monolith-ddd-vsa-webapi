using Common.Application.Options;
using Common.InterModuleRequests.Contracts;
using Common.InterModuleRequests.IAM;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Notifications.Application.Persistence;
using Notifications.Infrastructure.Telemetry;

namespace Notifications.Infrastructure.Devices;

/// <summary>
///     Keycloak can revoke a session without telling Notifications (idle expiry, admin console, reuse
///     detection), leaving <see cref="Notifications.Domain.Devices.DeviceRegistration" /> active with a live
///     push token. Periodically asks IAM which sessions are still live and deactivates the rest (PR #149 #4).
///     Each run takes the <c>ReconcileBatchSize</c> users whose active rows were checked least recently
///     (<c>LastReconciledOn</c>, never-checked first) and stamps them, so consecutive runs walk the whole registry
///     instead of re-checking the same first batch forever.
/// </summary>
public sealed partial class DeviceRegistryReconciliationService(
    INotificationsDbContext dbContext,
    IInterModuleRequestClient<GetActiveSessionIdsRequest, GetActiveSessionIdsResponse> sessionsClient,
    IOptions<DevicesOptions> devicesOptionsProvider,
    TimeProvider timeProvider,
    ILogger<DeviceRegistryReconciliationService> logger)
{
    public async Task ReconcileAsync(CancellationToken cancellationToken = default)
    {
        var batchSize = devicesOptionsProvider.Value.ReconcileBatchSize;
        var userIds = await dbContext.DeviceRegistrations
            .AsNoTracking()
            .TagWith(nameof(DeviceRegistryReconciliationService))
            .Where(r => r.IsActive)
            .GroupBy(r => r.UserId)
            // COALESCE puts never-reconciled users ahead of everyone else (Postgres sorts NULL last on ASC).
            .OrderBy(g => g.Min(r => r.LastReconciledOn) ?? DateTimeOffset.MinValue)
            .Select(g => g.Key)
            .Take(batchSize)
            .ToListAsync(cancellationToken);

        if (userIds.Count == 0)
        {
            return;
        }

        var liveSessions = await sessionsClient.SendAsync(new GetActiveSessionIdsRequest(userIds), cancellationToken);
        var liveSessionIdsByUser = liveSessions.Users
            .ToDictionary(u => u.UserId, u => u.SessionIds.ToHashSet(StringComparer.Ordinal));

        var registrations = await dbContext.DeviceRegistrations
            .TagWith(nameof(DeviceRegistryReconciliationService))
            .Where(r => r.IsActive && userIds.Contains(r.UserId))
            .ToListAsync(cancellationToken);

        var now = timeProvider.GetUtcNow();
        var deactivated = 0;
        foreach (var registration in registrations)
        {
            registration.MarkReconciled(now);

            var liveSessionIds = liveSessionIdsByUser.GetValueOrDefault(registration.UserId, []);
            if (liveSessionIds.Contains(registration.SessionId))
            {
                continue;
            }

            registration.Deactivate();
            deactivated++;
        }

        await dbContext.SaveChangesAsync(cancellationToken);

        NotificationsTelemetry.RecordDeviceRegistrationsReconciled(deactivated);
        LogReconcileComplete(logger, userIds.Count, deactivated);
    }

    [LoggerMessage(Level = LogLevel.Information,
        Message = "Device registry reconcile checked {UserCount} users against Keycloak, deactivated {DeactivatedCount} stale registrations.")]
    private static partial void LogReconcileComplete(ILogger logger, int userCount, int deactivatedCount);
}
