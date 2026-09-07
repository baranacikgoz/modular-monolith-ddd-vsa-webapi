using Common.Infrastructure.Persistence.Extensions;
using Common.InterModuleRequests.Contracts;
using Common.InterModuleRequests.Notifications;
using Microsoft.EntityFrameworkCore;
using Notifications.Application.Persistence;

namespace Notifications.Infrastructure.InterModuleRequestHandlers;

public class GetDeviceSessionsRequestHandler(INotificationsDbContext dbContext)
    : InterModuleRequestHandler<GetDeviceSessionsRequest, GetDeviceSessionsResponse>
{
    public override async Task<GetDeviceSessionsResponse> HandleAsync(
        GetDeviceSessionsRequest request, CancellationToken cancellationToken)
    {
        var sessions = await dbContext.DeviceRegistrations
            .AsNoTracking()
            .TagWith(nameof(GetDeviceSessionsRequestHandler), request.UserId)
            .Where(r => r.UserId == request.UserId && r.IsActive)
            .Select(r => new DeviceSession(r.SessionId, r.ClientId, r.DeviceName))
            .ToListAsync(cancellationToken);

        return new GetDeviceSessionsResponse(sessions);
    }
}
