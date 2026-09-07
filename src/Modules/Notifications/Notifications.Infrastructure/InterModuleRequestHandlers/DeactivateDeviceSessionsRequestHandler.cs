using Common.Application.Extensions;
using Common.Infrastructure.Persistence.Extensions;
using Common.InterModuleRequests.Contracts;
using Common.InterModuleRequests.Notifications;
using Microsoft.EntityFrameworkCore;
using Notifications.Application.Persistence;

namespace Notifications.Infrastructure.InterModuleRequestHandlers;

public class DeactivateDeviceSessionsRequestHandler(INotificationsDbContext dbContext)
    : InterModuleRequestHandler<DeactivateDeviceSessionsRequest, DeactivateDeviceSessionsResponse>
{
    public override async Task<DeactivateDeviceSessionsResponse> HandleAsync(
        DeactivateDeviceSessionsRequest request, CancellationToken cancellationToken)
    {
        var registrations = await dbContext.DeviceRegistrations
            .TagWith(nameof(DeactivateDeviceSessionsRequestHandler), request.UserId)
            .Where(r => r.UserId == request.UserId && r.IsActive)
            .WhereIf(r => request.SessionIds!.Contains(r.SessionId), request.SessionIds is not null)
            .ToListAsync(cancellationToken);

        foreach (var registration in registrations)
        {
            registration.Deactivate();
        }

        await dbContext.SaveChangesAsync(cancellationToken);

        return new DeactivateDeviceSessionsResponse();
    }
}
