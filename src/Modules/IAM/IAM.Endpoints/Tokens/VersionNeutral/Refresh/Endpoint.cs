using System.Security.Cryptography;
using Common.Application.Extensions;
using Common.Domain.ResultMonad;
using Common.Infrastructure.Extensions;
using Common.Infrastructure.Persistence.Extensions;
using IAM.Application.Persistence;
using IAM.Application.Tokens.Services;
using IAM.Domain.Errors;
using IAM.Domain.Identity;
using IAM.Infrastructure.Telemetry;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;

namespace IAM.Endpoints.Tokens.VersionNeutral.Refresh;

internal static class Endpoint
{
    internal static void MapEndpoint(RouteGroupBuilder tokensApiGroup)
    {
        tokensApiGroup
            .MapPost("refresh", RefreshToken)
            .WithDescription("Refresh token.")
            .AllowAnonymous()
            .Produces<Response>()
            .TransformResultTo<Response>();
    }

    private static async Task<Result<Response>> RefreshToken(
        Request request,
        IIAMDbContext dbContext,
        TimeProvider timeProvider,
        ITokenService tokenService,
        CancellationToken cancellationToken)
    {
        using var activity = IamTelemetry.ActivitySource.StartActivityForCaller();

        return await TryDecodeBase64(request.RefreshToken)
            .BindAsync(async providedRefreshToken =>
            {
                var hash = SHA256.HashData(providedRefreshToken);

                return await dbContext
                    .Users
                    .AsNoTracking()
                    .TagWith(nameof(RefreshToken))
                    .Where(x => x.RefreshTokenHash == hash)
                    .Select(u => new
                    {
                        u.Id,
                        u.RefreshTokenExpiresAt,
                        Roles = dbContext
                            .UserRoles
                            .Where(ur => ur.UserId == u.Id)
                            .Join(dbContext.Roles,
                                ur => ur.RoleId,
                                r => r.Id,
                                (ur, r) => r.Name)
                            .Where(name => name != null)
                            .Select(name => name!)
                            .ToList()
                    })
                    .SingleAsResultAsync(resourceName: nameof(ApplicationUser), cancellationToken);
            })
            .TapAsync(user => user.RefreshTokenExpiresAt < timeProvider.GetUtcNow()
                ? Result.Failure(TokenErrors.RefreshTokenExpired)
                : Result.Success)
            .MapAsync(user =>
            {
                var utcNow = timeProvider.GetUtcNow();
                var (accessToken, accessTokenExpiresAt) =
                    tokenService.GenerateAccessToken(utcNow, user.Id, user.Roles);
                return new Response { AccessToken = accessToken, AccessTokenExpiresAt = accessTokenExpiresAt };
            })
            .TapActivityAsync(activity);
    }

    /// <summary>
    ///     Safely decodes a Base64 string, returning a typed error instead of throwing
    ///     <see cref="FormatException" /> on malformed input.
    /// </summary>
    private static Result<byte[]> TryDecodeBase64(string base64)
    {
        try
        {
            return Convert.FromBase64String(base64);
        }
        catch (FormatException)
        {
            return TokenErrors.InvalidRefreshToken;
        }
    }
}
