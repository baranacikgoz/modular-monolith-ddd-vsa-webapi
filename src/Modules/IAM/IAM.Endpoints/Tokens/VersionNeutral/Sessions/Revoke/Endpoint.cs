using Common.Application.Auth;
using Common.Application.Extensions;
using Common.Domain.ResultMonad;
using Common.Infrastructure.Persistence.Extensions;
using IAM.Application.Persistence;
using IAM.Domain.Errors;
using IAM.Domain.Identity;
using IAM.Domain.Identity.Sessions;
using IAM.Infrastructure.Telemetry;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;

namespace IAM.Endpoints.Tokens.VersionNeutral.Sessions.Revoke;

internal static class Endpoint
{
    internal static void MapEndpoint(RouteGroupBuilder sessionsApiGroup)
    {
        sessionsApiGroup
            .MapDelete("{id}", RevokeSession)
            .WithDescription("Sign out one specific session (device/app).")
            .MustHavePermission(CustomActions.DeleteMy, CustomResources.ApplicationUsers)
            .Produces(StatusCodes.Status204NoContent)
            .TransformResultToNoContentResponse();
    }

    private static async Task<Result> RevokeSession(
        [AsParameters] Request request,
        ICurrentUser currentUser,
        IIAMDbContext dbContext,
        TimeProvider timeProvider,
        CancellationToken cancellationToken)
    {
        // Filtered by (Id, UserId): a session belonging to another user resolves as not-owned —
        // never leak existence of another user's session id.
        return await dbContext
            .Users
            .Include(u => u.Sessions.Where(s => s.Id == request.Id))
            .TagWith(nameof(RevokeSession), currentUser.Id, request.Id)
            .Where(u => u.Id == currentUser.Id)
            .SingleAsResultAsync(nameof(ApplicationUser), cancellationToken)
            .TapAsync(user => user.Sessions.SingleOrDefault() is not null
                ? Result.Success
                : TokenErrors.SessionNotFound)
            .TapAsync(user => user.RevokeSession(user.Sessions.Single(), SessionRevokedReason.UserSignedOut, timeProvider.GetUtcNow()))
            .TapAsync(async _ => await dbContext.SaveChangesAsync(cancellationToken))
            .TapAsync(_ => IamTelemetry.RecordSessionRevoked(SessionRevokedReason.UserSignedOut));
    }
}
