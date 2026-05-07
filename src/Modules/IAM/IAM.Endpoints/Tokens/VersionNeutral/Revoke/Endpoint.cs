using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Common.Application.Auth;
using Common.Application.Caching;
using Common.Application.Extensions;
using Common.Application.Options;
using Common.Domain.ResultMonad;
using Common.Infrastructure.Extensions;
using Common.Infrastructure.Persistence.Extensions;
using IAM.Application.Persistence;
using IAM.Domain.Identity;
using IAM.Infrastructure.Telemetry;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Options;

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
        ICacheService cacheService,
        IOptions<JwtOptions> jwtOptionsProvider,
        HttpContext httpContext,
        CancellationToken cancellationToken)
    {
        using var activity = IamTelemetry.ActivitySource.StartActivityForCaller();

        var jti = httpContext.User.FindFirstValue(JwtRegisteredClaimNames.Jti);

        return await dbContext
            .Users
            .TagWith(nameof(RevokeToken), currentUser.Id)
            .Where(u => u.Id == currentUser.Id)
            .SingleAsResultAsync(nameof(ApplicationUser), cancellationToken)
            .TapAsync(user => user.RevokeRefreshToken())
            .TapAsync(async _ => await dbContext.SaveChangesAsync(cancellationToken))
            .TapWhenAsync(
                async _ => await cacheService.SetAsync<bool?>(
                    $"blacklisted_jti:{jti}",
                    true,
                    absoluteExpirationRelativeToNow: TimeSpan.FromMinutes(jwtOptionsProvider.Value.AccessTokenExpirationInMinutes),
                    cancellationToken: cancellationToken),
                when: () => !string.IsNullOrEmpty(jti))
            .TapActivityAsync(activity);
    }
}
