using Common.Application.EventBus;
using Common.Infrastructure.Persistence.Extensions;
using Common.IntegrationEvents;
using Common.InterModuleRequests.Contracts;
using Common.InterModuleRequests.Notifications;
using Microsoft.EntityFrameworkCore;
using Notifications.Application.Persistence;
using Notifications.Domain.Devices;

namespace Notifications.Infrastructure.InterModuleRequestHandlers;

public class BindDeviceSessionRequestHandler(
    INotificationsDbContext dbContext,
    IIntegrationEventOutbox outbox,
    TimeProvider timeProvider)
    : InterModuleRequestHandler<BindDeviceSessionRequest, BindDeviceSessionResponse>
{
    public override async Task<BindDeviceSessionResponse> HandleAsync(
        BindDeviceSessionRequest request, CancellationToken cancellationToken)
    {
        var now = timeProvider.GetUtcNow();

        var registration = await dbContext.DeviceRegistrations
            .TagWith(nameof(BindDeviceSessionRequestHandler), request.UserId)
            .SingleOrDefaultAsync(
                r => r.UserId == request.UserId && r.DeviceId == request.DeviceId && r.ClientId == request.ClientId,
                cancellationToken);

        string? supersededSessionId = null;
        if (registration is null)
        {
            registration = DeviceRegistration.Create(
                request.UserId, request.DeviceId, request.ClientId, request.SessionId,
                request.DeviceName, request.PushToken, now);
            dbContext.DeviceRegistrations.Add(registration);
        }
        else
        {
            supersededSessionId = registration.Rebind(request.SessionId, request.DeviceName, request.PushToken, now);
        }

        // Committed with the registration, so a sign-in that was bound is never missing its event.
        outbox.Collect(new UserSignedInIntegrationEvent(
            request.UserId, request.SessionId, request.DeviceId, request.ClientId));

        await dbContext.SaveChangesAsync(cancellationToken);

        return new BindDeviceSessionResponse(supersededSessionId);
    }
}
