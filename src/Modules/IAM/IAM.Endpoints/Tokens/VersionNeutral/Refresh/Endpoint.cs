using System.Security.Cryptography;
using Common.Application.Extensions;
using Common.Application.Persistence;
using Common.Domain.ResultMonad;
using IAM.Application.Persistence;
using IAM.Application.Tokens.Services;
using IAM.Domain.Errors;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
            .Produces(StatusCodes.Status204NoContent)
            .TransformResultTo<Response>();
    }

    private static async Task<Result<Response>> RefreshToken(
        [FromBody] Request request,
        [FromServices] IIAMDbContext dbContext,
        [FromServices] TimeProvider timeProvider,
        [FromServices] ITokenService tokenService,
        CancellationToken cancellationToken)
    {
        var providedRefreshToken = Convert.FromBase64String(request.RefreshToken);
        var providedRefreshTokenHash = SHA256.HashData(providedRefreshToken);

        var userResult = await dbContext
            .Users
            .AsNoTracking()
            .TagWith(nameof(RefreshToken))
            .Where(x => x.RefreshTokenHash == providedRefreshTokenHash)
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
            .SingleAsResultAsync(cancellationToken);

        if (userResult.IsFailure)
        {
            return Result<Response>.Failure(userResult.Error!);
        }
        var user = userResult.Value!;

        if (user.RefreshTokenExpiresAt < timeProvider.GetUtcNow())
        {
            return TokenErrors.RefreshTokenExpired;
        }

        var utcNow = timeProvider.GetUtcNow();

        var (accessToken, accessTokenExpiresAt) = tokenService.GenerateAccessToken(utcNow, user.Id, user.Roles);

        return new Response
        {
            AccessToken = accessToken,
            AccessTokenExpiresAt = accessTokenExpiresAt
        };
    }
}
