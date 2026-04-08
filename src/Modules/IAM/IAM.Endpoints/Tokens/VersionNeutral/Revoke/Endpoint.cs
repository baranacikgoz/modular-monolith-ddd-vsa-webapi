using Common.Application.Auth;
using Common.Application.Extensions;
using Common.Domain.ResultMonad;
using Common.Infrastructure.Extensions;
using Common.Infrastructure.Persistence.Extensions;
using IAM.Application.Persistence;
using IAM.Domain.Identity;
using IAM.Infrastructure.Telemetry;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace IAM.Endpoints.Tokens.VersionNeutral.Revoke;

internal static class Endpoint
{
    internal static void MapEndpoint(RouteGroupBuilder tokensApiGroup)
    {
        tokensApiGroup
            .MapPost("revoke", RevokeToken)
            .WithDescription("Revoke (invalidate) the current refresh token. Use this to log out.")
            .MustHavePermission(CustomActions.ReadMy, CustomResources.ApplicationUsers)
            .Produces(StatusCodes.Status204NoContent)
            .TransformResultToNoContentResponse();
    }

    private static async Task<Result> RevokeToken(
        ICurrentUser currentUser,
        IIAMDbContext dbContext,
        CancellationToken cancellationToken)
    {
        using var activity = IamTelemetry.ActivitySource.StartActivityForCaller();

        return await dbContext
            .Users
            .TagWith(nameof(RevokeToken), currentUser.Id)
            .Where(u => u.Id == currentUser.Id)
            .SingleAsResultAsync(nameof(ApplicationUser), cancellationToken)
            .TapAsync(user => user.RevokeRefreshToken())
            .TapAsync(async _ => await dbContext.SaveChangesAsync(cancellationToken))
            .TapActivityAsync(activity);
    }
}
