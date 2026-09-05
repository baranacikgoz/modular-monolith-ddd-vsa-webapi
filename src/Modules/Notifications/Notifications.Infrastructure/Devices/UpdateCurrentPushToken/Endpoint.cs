using Common.Application.Auth;
using Common.Application.Extensions;
using Common.Domain.ResultMonad;
using Common.Infrastructure.Persistence.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Notifications.Application.Persistence;
using Notifications.Domain.Devices;

namespace Notifications.Infrastructure.Devices.UpdateCurrentPushToken;

internal static class Endpoint
{
    internal static void MapEndpoint(RouteGroupBuilder devicesApiGroup)
    {
        devicesApiGroup
            .MapPut("current/push-token", UpdateCurrentPushToken)
            .WithDescription("Sets or rotates the FCM push token for the device that owns the current session.")
            .RequireScope(KeycloakScopes.Devices.UpdateOwn)
            .Produces(StatusCodes.Status204NoContent)
            .TransformResultToNoContentResponse();
    }

    private static async Task<Result> UpdateCurrentPushToken(
        Request request,
        ICurrentUser currentUser,
        INotificationsDbContext dbContext,
        TimeProvider timeProvider,
        CancellationToken cancellationToken)
    {
        var sessionId = currentUser.SessionId;

        return await Result<string>.Success(sessionId ?? string.Empty)
            .Bind(sid => sid.Length > 0 ? Result<string>.Success(sid) : DeviceErrors.RegistrationNotFound)
            .BindAsync(sid => dbContext.DeviceRegistrations
                .TagWith(nameof(UpdateCurrentPushToken), currentUser.Id)
                .Where(r => r.UserId == currentUser.Id && r.SessionId == sid && r.IsActive)
                .SingleAsResultAsync(nameof(DeviceRegistration), cancellationToken))
            .TapAsync(registration => registration.SetPushToken(request.PushToken, timeProvider.GetUtcNow()))
            .TapAsync(_ => dbContext.SaveChangesAsync(cancellationToken));
    }
}
