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
/// </summary>
public sealed partial class DeviceRegistryReconciliationService(
    INotificationsDbContext dbContext,
    IInterModuleRequestClient<GetActiveSessionIdsRequest, GetActiveSessionIdsResponse> sessionsClient,
    IOptions<DevicesOptions> devicesOptionsProvider,
    ILogger<DeviceRegistryReconciliationService> logger)
{
    public async Task ReconcileAsync(CancellationToken cancellationToken = default)
    {
        // ponytail: one batch of users per run, oldest-registration-first via default ordering; one Keycloak
        // call per user. Upgrade to Keycloak's client-level session enumeration if the active registry outgrows
        // a single ReconcileBatchSize run before the next scheduled tick catches up.
        var batchSize = devicesOptionsProvider.Value.ReconcileBatchSize;
        var userIds = await dbContext.DeviceRegistrations
            .AsNoTracking()
            .TagWith(nameof(DeviceRegistryReconciliationService))
            .Where(r => r.IsActive)
            .Select(r => r.UserId)
            .Distinct()
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

        var deactivated = 0;
        foreach (var registration in registrations)
        {
            var liveSessionIds = liveSessionIdsByUser.GetValueOrDefault(registration.UserId, []);
            if (liveSessionIds.Contains(registration.SessionId))
            {
                continue;
            }

            registration.Deactivate();
            deactivated++;
        }

        if (deactivated > 0)
        {
            await dbContext.SaveChangesAsync(cancellationToken);
        }

        NotificationsTelemetry.RecordDeviceRegistrationsReconciled(deactivated);
        LogReconcileComplete(logger, userIds.Count, deactivated);
    }

    [LoggerMessage(Level = LogLevel.Information,
        Message = "Device registry reconcile checked {UserCount} users against Keycloak, deactivated {DeactivatedCount} stale registrations.")]
    private static partial void LogReconcileComplete(ILogger logger, int userCount, int deactivatedCount);
}
