using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Notifications.Infrastructure.Devices;

internal static class Setup
{
    public static void MapDevicesEndpoints(this RouteGroupBuilder notificationsApiGroup)
    {
        var devicesApiGroup = notificationsApiGroup
            .MapGroup("/devices")
            .WithTags("Devices");

        UpdateCurrentPushToken.Endpoint.MapEndpoint(devicesApiGroup);
    }
}
