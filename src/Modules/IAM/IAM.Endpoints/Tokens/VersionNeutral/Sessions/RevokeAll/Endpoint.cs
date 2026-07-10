using Common.Application.Auth;
using Common.Application.Extensions;
using Common.Domain.ResultMonad;
using Common.Infrastructure.Persistence.Extensions;
using IAM.Application.Persistence;
using IAM.Domain.Identity;
using IAM.Domain.Identity.Sessions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;

namespace IAM.Endpoints.Tokens.VersionNeutral.Sessions.RevokeAll;

internal static class Endpoint
{
    internal static void MapEndpoint(RouteGroupBuilder sessionsApiGroup)
    {
        sessionsApiGroup
            .MapDelete("", RevokeAllSessions)
            .WithDescription("Sign out everywhere — revoke every session for the caller (e.g. lost/stolen device).")
            .MustHavePermission(CustomActions.DeleteMy, CustomResources.ApplicationUsers)
            .Produces(StatusCodes.Status204NoContent)
            .TransformResultToNoContentResponse();
    }

    private static async Task<Result> RevokeAllSessions(
        ICurrentUser currentUser,
        IIAMDbContext dbContext,
        TimeProvider timeProvider,
        CancellationToken cancellationToken)
    {
        return await dbContext
            .Users
            .Include(u => u.Sessions)
            .TagWith(nameof(RevokeAllSessions), currentUser.Id)
            .Where(u => u.Id == currentUser.Id)
            .SingleAsResultAsync(nameof(ApplicationUser), cancellationToken)
            .TapAsync(user => user.RevokeAllSessions(SessionRevokedReason.SignedOutEverywhere, timeProvider.GetUtcNow()))
            .TapAsync(async _ => await dbContext.SaveChangesAsync(cancellationToken));
    }
}
