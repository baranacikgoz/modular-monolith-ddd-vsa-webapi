using System.Security.Cryptography;
using Common.Application.Extensions;
using Common.Application.Options;
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
using Microsoft.Extensions.Options;
using Constants = IAM.Infrastructure.RateLimiting.Constants;

namespace IAM.Endpoints.Tokens.VersionNeutral.Refresh;

internal static class Endpoint
{
    internal static void MapEndpoint(RouteGroupBuilder tokensApiGroup)
    {
        tokensApiGroup
            .MapPost("refresh", RefreshToken)
            .WithDescription("Refresh token.")
            .AllowAnonymous()
            .RequireRateLimiting(Constants.TokenRefresh)
            .Produces<Response>()
            .TransformResultTo<Response>();
    }

    // The refresh token is deliberately NOT rotated. Rotation + reuse detection punishes
    // legitimate clients for every lost response, client-side timeout retry, or process kill
    // between the server-side rotate and the client persisting the new token — each one ends in
    // a spuriously revoked session and a forced re-login. And rotation buys nothing here: to be
    // fully correct it would have to keep accepting the predecessor token indefinitely, which is
    // equivalent to not rotating at all. Instead the same opaque 256-bit secret stays valid with
    // a sliding expiry, capped by the session's AbsoluteExpiresAt; theft response is session
    // revocation (sessions list / revoke endpoints). Refreshing is therefore idempotent and
    // trivially safe under concurrency — two racing refreshes both succeed.
    private static async Task<Result<Response>> RefreshToken(
        Request request,
        IIAMDbContext dbContext,
        TimeProvider timeProvider,
        ITokenService tokenService,
        HttpContext httpContext,
        IOptions<JwtOptions> jwtOptions,
        CancellationToken cancellationToken)
    {
        using var activity = IamTelemetry.ActivitySource.StartActivityForCaller();

        return await TryDecodeBase64(request.RefreshToken)
            .BindAsync(async providedRefreshToken =>
            {
                var hash = SHA256.HashData(providedRefreshToken);

                var lookup = await dbContext
                    .RefreshTokens
                    .AsNoTracking()
                    .TagWith(nameof(RefreshToken))
                    .Where(rt => rt.TokenHash == hash)
                    .Join(dbContext.Sessions.AsNoTracking(),
                        rt => rt.SessionId,
                        s => s.Id,
                        (rt, s) => new
                        {
                            rt.Id, rt.ConsumedAt, rt.ExpiresAt, s.UserId, SessionId = s.Id,
                            s.RevokedAt, s.AbsoluteExpiresAt
                        })
                    .SingleOrDefaultAsync(cancellationToken);

                if (lookup is null)
                {
                    return Result<Response>.Failure(TokenErrors.InvalidRefreshToken);
                }

                // Superseded by a newer login on the same (DeviceId, ClientId) — only the token
                // issued by that login is live for this session.
                if (lookup.ConsumedAt is not null)
                {
                    return Result<Response>.Failure(TokenErrors.InvalidRefreshToken);
                }

                var utcNow = timeProvider.GetUtcNow();

                if (lookup.ExpiresAt < utcNow)
                {
                    return Result<Response>.Failure(TokenErrors.RefreshTokenExpired);
                }

                // Hard cap on session lifetime regardless of sliding token expiry.
                if (lookup.AbsoluteExpiresAt < utcNow)
                {
                    return Result<Response>.Failure(TokenErrors.RefreshTokenExpired);
                }

                if (lookup.RevokedAt is not null)
                {
                    return Result<Response>.Failure(TokenErrors.SessionRevoked);
                }

                return await dbContext
                    .Users
                    .Include(u => u.Sessions.Where(s => s.Id == lookup.SessionId))
                    .ThenInclude(s => s.RefreshTokens.Where(rt => rt.Id == lookup.Id))
                    .TagWith(nameof(RefreshToken), lookup.UserId)
                    .Where(u => u.Id == lookup.UserId)
                    .Select(u => new
                    {
                        User = u,
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
                    .SingleAsResultAsync(nameof(ApplicationUser), cancellationToken)
                    .BindAsync(async userObj =>
                    {
                        var session = userObj.User.Sessions.Single();
                        var currentToken = session.RefreshTokens.Single();

                        var utcNowForIssue = timeProvider.GetUtcNow();
                        var (accessToken, accessTokenExpiresAt) =
                            tokenService.GenerateAccessToken(
                                utcNowForIssue, userObj.User.Id, lookup.SessionId, userObj.Roles);

                        var newRefreshTokenExpiresAt =
                            utcNowForIssue.AddDays(jwtOptions.Value.RefreshTokenExpirationInDays);
                        var ip = httpContext.Connection.RemoteIpAddress?.ToString();
                        var userAgent = httpContext.Request.Headers.UserAgent.ToString();

                        ApplicationUser.SlideRefreshToken(
                            session, currentToken, ip, userAgent, utcNowForIssue, newRefreshTokenExpiresAt);
                        try
                        {
                            await dbContext.SaveChangesAsync(cancellationToken);
                        }
                        catch (DbUpdateConcurrencyException)
                        {
                            // A concurrent refresh of the same session won the write race (xmin
                            // row-version on Session/RefreshToken). Both writes slide the same
                            // token to effectively the same expiry, so the losing one is
                            // redundant — answer success. Failing here would resurrect the exact
                            // spurious-sign-out class (client timeout retry racing its own slow
                            // first attempt) that this non-rotating design exists to kill.
                        }

                        activity?.SetTag("session.id", lookup.SessionId.ToString());

                        return Result<Response>.Success(new Response
                        {
                            AccessToken = accessToken,
                            AccessTokenExpiresAt = accessTokenExpiresAt,
                            RefreshToken = Convert.ToBase64String(providedRefreshToken),
                            RefreshTokenExpiresAt = newRefreshTokenExpiresAt
                        });
                    });
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
