using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Common.Application.Auth;
using Common.Application.Extensions;
using Common.Application.Options;
using Common.Domain.ResultMonad;
using Common.Infrastructure.Extensions;
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
using Microsoft.Extensions.Options;
using ZiggyCreatures.Caching.Fusion;

namespace IAM.Endpoints.Tokens.VersionNeutral.Revoke;

internal static class Endpoint
{
    internal static void MapEndpoint(RouteGroupBuilder tokensApiGroup)
    {
        tokensApiGroup
            .MapPost("revoke", RevokeToken)
            .WithDescription("Revoke (invalidate) the current session's refresh token. Use this to log out.")
            .MustHavePermission(CustomActions.ReadMy, CustomResources.ApplicationUsers)
            .Produces(StatusCodes.Status204NoContent)
            .TransformResultToNoContentResponse();
    }

    private static async Task<Result> RevokeToken(
        ICurrentUser currentUser,
        IIAMDbContext dbContext,
        IFusionCache cacheService,
        IOptions<JwtOptions> jwtOptionsProvider,
        TimeProvider timeProvider,
        HttpContext httpContext,
        CancellationToken cancellationToken)
    {
        using var activity = IamTelemetry.ActivitySource.StartActivityForCaller();

        var jti = httpContext.User.FindFirstValue(JwtRegisteredClaimNames.Jti);

        if (currentUser.SessionId is not { } rawSessionId)
        {
            // Access token predates session tracking — nothing to revoke, but blacklist the jti anyway.
            if (!string.IsNullOrEmpty(jti))
            {
                await cacheService.SetAsync<bool?>(
                    $"blacklisted_jti:{jti}",
                    true,
                    options: new FusionCacheEntryOptions { Duration = TimeSpan.FromMinutes(jwtOptionsProvider.Value.AccessTokenExpirationInMinutes) },
                    token: cancellationToken);
            }

            return Result.Success;
        }

        var sessionId = new SessionId(rawSessionId);

        return await dbContext
            .Users
            .Include(u => u.Sessions.Where(s => s.Id == sessionId))
            .TagWith(nameof(RevokeToken), currentUser.Id)
            .Where(u => u.Id == currentUser.Id)
            .SingleAsResultAsync(nameof(ApplicationUser), cancellationToken)
            .TapAsync(user => user.Sessions.SingleOrDefault() is not null
                ? Result.Success
                : TokenErrors.SessionNotFound)
            .TapAsync(user => user.RevokeSession(user.Sessions.Single(), SessionRevokedReason.UserSignedOut, timeProvider.GetUtcNow()))
            .TapAsync(async _ => await dbContext.SaveChangesAsync(cancellationToken))
            .TapAsync(_ => IamTelemetry.RecordSessionRevoked(SessionRevokedReason.UserSignedOut))
            .TapWhenAsync(
                async _ => await cacheService.SetAsync<bool?>(
                    $"blacklisted_jti:{jti}",
                    true,
                    options: new FusionCacheEntryOptions { Duration = TimeSpan.FromMinutes(jwtOptionsProvider.Value.AccessTokenExpirationInMinutes) },
                    token: cancellationToken),
                when: () => !string.IsNullOrEmpty(jti))
            .TapActivityAsync(activity);
    }
}
