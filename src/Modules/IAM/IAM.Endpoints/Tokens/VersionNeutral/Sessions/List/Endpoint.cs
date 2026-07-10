using Common.Application.Auth;
using Common.Application.Extensions;
using Common.Domain.ResultMonad;
using Common.Infrastructure.Persistence.Extensions;
using IAM.Application.Persistence;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;

namespace IAM.Endpoints.Tokens.VersionNeutral.Sessions.List;

internal static class Endpoint
{
    internal static void MapEndpoint(RouteGroupBuilder sessionsApiGroup)
    {
        sessionsApiGroup
            .MapGet("", ListSessions)
            .WithDescription("List the caller's active sessions (devices/apps currently signed in).")
            .MustHavePermission(CustomActions.ReadMy, CustomResources.ApplicationUsers)
            .Produces<IReadOnlyCollection<Response>>()
            .TransformResultTo<IReadOnlyCollection<Response>>();
    }

    private static async Task<Result<IReadOnlyCollection<Response>>> ListSessions(
        ICurrentUser currentUser,
        IIAMDbContext dbContext,
        CancellationToken cancellationToken)
    {
        var sessions = await dbContext
            .Sessions
            .AsNoTracking()
            .TagWith(nameof(ListSessions), currentUser.Id)
            .Where(s => s.UserId == currentUser.Id && s.RevokedAt == null)
            .OrderByDescending(s => s.LastUsedAt)
            .Select(s => new Response
            {
                Id = s.Id.Value,
                ClientId = s.ClientId,
                DeviceName = s.DeviceName,
                CreatedAt = s.CreatedOn,
                LastUsedAt = s.LastUsedAt,
                IsCurrent = currentUser.SessionId == s.Id.Value
            })
            .ToListAsync(cancellationToken);

        return Result<IReadOnlyCollection<Response>>.Success(sessions);
    }
}
