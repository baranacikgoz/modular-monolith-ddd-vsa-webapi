using Common.Application.Auth;
using Common.Application.Extensions;
using Common.Domain.ResultMonad;
using Common.Infrastructure.Persistence.Extensions;
using IAM.Application.Persistence;
using IAM.Domain.Errors;
using IAM.Domain.Identity;
using IAM.Domain.Identity.Sessions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;

namespace IAM.Endpoints.Tokens.VersionNeutral.Sessions.UpdatePushToken;

internal static class Endpoint
{
    internal static void MapEndpoint(RouteGroupBuilder sessionsApiGroup)
    {
        sessionsApiGroup
            .MapPut("current/push-token", UpdateCurrentSessionPushToken)
            .WithDescription("Sets or rotates the FCM push token for the current session (device/app).")
            .MustHavePermission(CustomActions.UpdateMy, CustomResources.ApplicationUsers)
            .Produces(StatusCodes.Status204NoContent)
            .TransformResultToNoContentResponse();
    }

    private static async Task<Result> UpdateCurrentSessionPushToken(
        Request request,
        ICurrentUser currentUser,
        IIAMDbContext dbContext,
        TimeProvider timeProvider,
        CancellationToken cancellationToken)
    {
        if (currentUser.SessionId is not { } rawSessionId)
        {
            // Access token predates session tracking — nothing to update.
            return Result.Success;
        }

        var sessionId = new SessionId(rawSessionId);

        return await dbContext
            .Users
            .Include(u => u.Sessions.Where(s => s.Id == sessionId))
            .TagWith(nameof(UpdateCurrentSessionPushToken), currentUser.Id)
            .Where(u => u.Id == currentUser.Id)
            .SingleAsResultAsync(nameof(ApplicationUser), cancellationToken)
            .TapAsync(user => user.Sessions.SingleOrDefault() is not null
                ? Result.Success
                : TokenErrors.SessionNotFound)
            .TapAsync(user => user.UpdateSessionPushToken(
                user.Sessions.Single(), request.PushToken, timeProvider.GetUtcNow()))
            .TapAsync(async _ => await dbContext.SaveChangesAsync(cancellationToken));
    }
}
