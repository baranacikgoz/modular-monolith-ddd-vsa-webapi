using System.Security.Cryptography;
using Common.Application.Extensions;
using Common.Application.Options;
using Common.Domain.ResultMonad;
using Common.Domain.StronglyTypedIds;
using Common.Infrastructure.Extensions;
using Common.Infrastructure.Persistence.Extensions;
using EntityFramework.Exceptions.Common;
using IAM.Application.Persistence;
using IAM.Application.Tokens.Services;
using IAM.Domain.Errors;
using IAM.Domain.Identity;
using IAM.Domain.Identity.Sessions;
using IAM.Infrastructure.Telemetry;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Constants = IAM.Infrastructure.RateLimiting.Constants;

namespace IAM.Endpoints.Tokens.VersionNeutral.Refresh;

internal static partial class Endpoint
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

    private static async Task<Result<Response>> RefreshToken(
        Request request,
        IIAMDbContext dbContext,
        TimeProvider timeProvider,
        ITokenService tokenService,
        HttpContext httpContext,
        ILogger<ApplicationUser> logger,
        IOptions<JwtOptions> jwtOptions,
        CancellationToken cancellationToken)
    {
        using var activity = IamTelemetry.ActivitySource.StartActivityForCaller();

        return await TryDecodeBase64(request.RefreshToken)
            .BindAsync(async providedRefreshToken =>
            {
                var hash = SHA256.HashData(providedRefreshToken);

                // A concurrent refresh of the same token can consume it between the snapshot read
                // and the tracked write, or win the optimistic-concurrency race at SaveChanges
                // (rotation raises an event, bumping the user aggregate's Version). One internal
                // retry re-reads fresh state so the loser resolves through the reuse grace window
                // below — a client timeout retry racing its own slow first attempt must succeed,
                // not lose its session to reuse detection.
                var refreshResult = await TryRefreshOnceAsync(hash);
                if (refreshResult is null)
                {
                    dbContext.ChangeTracker.Clear();
                    refreshResult = await TryRefreshOnceAsync(hash);
                }

                return refreshResult ?? Result<Response>.Failure(TokenErrors.InvalidRefreshToken);
            })
            .TapActivityAsync(activity);

        // Single refresh attempt. Returns null when it lost a same-token race and a retry against
        // fresh state is warranted; the caller retries at most once.
        async Task<Result<Response>?> TryRefreshOnceAsync(byte[] hash)
        {
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
                        rt.Id, rt.ConsumedAt, rt.ExpiresAt, rt.ReplacedByTokenId, s.UserId, SessionId = s.Id,
                        s.RevokedAt, s.AbsoluteExpiresAt
                    })
                .SingleOrDefaultAsync(cancellationToken);

            if (lookup is null)
            {
                return Result<Response>.Failure(TokenErrors.InvalidRefreshToken);
            }

            var utcNowForExpiryChecks = timeProvider.GetUtcNow();

            if (lookup.ExpiresAt < utcNowForExpiryChecks)
            {
                return Result<Response>.Failure(TokenErrors.RefreshTokenExpired);
            }

            // Hard cap on session lifetime regardless of rotation — an absolute-expired session's
            // dead tokens are just expired, not a theft signal, so this must be checked first.
            if (lookup.AbsoluteExpiresAt < utcNowForExpiryChecks)
            {
                return Result<Response>.Failure(TokenErrors.RefreshTokenExpired);
            }

            var effectiveTokenId = lookup.Id;

            if (lookup.ConsumedAt is not null)
            {
                // Consumed with no successor link = superseded by a newer login on the same
                // (DeviceId, ClientId), not rotated away. A stale token on a reinstalled device is
                // normal life, not a theft signal worth killing the live session over — reuse
                // detection below applies only to the rotation chain.
                if (lookup.ReplacedByTokenId is not { } successorId)
                {
                    return Result<Response>.Failure(TokenErrors.InvalidRefreshToken);
                }

                // Lost-response retry: the client rotated successfully but never saw the response
                // (e.g. process killed mid-flight) and retries with the now-dead token. If its
                // immediate successor is still live and we're within the grace window, rotate that
                // successor instead of flagging theft — replaying anything older than one hop back,
                // or past the window, still falls through to reuse detection below.
                RefreshTokenId? graceSuccessorId = null;
                if (utcNowForExpiryChecks - lookup.ConsumedAt.Value <=
                    TimeSpan.FromSeconds(jwtOptions.Value.RefreshTokenReuseGraceWindowInSeconds))
                {
                    graceSuccessorId = await dbContext
                        .RefreshTokens
                        .AsNoTracking()
                        .TagWith(nameof(RefreshToken), successorId)
                        .Where(rt => rt.Id == successorId && rt.ConsumedAt == null &&
                                     rt.ExpiresAt > utcNowForExpiryChecks)
                        .Select(rt => (RefreshTokenId?)rt.Id)
                        .SingleOrDefaultAsync(cancellationToken);
                }

                if (graceSuccessorId is null)
                {
                    return await RevokeAsReuseAsync(lookup.UserId, lookup.SessionId);
                }

                effectiveTokenId = graceSuccessorId.Value;
            }

            if (lookup.RevokedAt is not null)
            {
                return Result<Response>.Failure(TokenErrors.SessionRevoked);
            }

            var userLoad = await dbContext
                .Users
                .Include(u => u.Sessions.Where(s => s.Id == lookup.SessionId))
                .ThenInclude(s => s.RefreshTokens.Where(rt => rt.Id == effectiveTokenId))
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
                .SingleAsResultAsync(nameof(ApplicationUser), cancellationToken);

            if (userLoad.IsFailure)
            {
                return Result<Response>.Failure(userLoad.Error!);
            }

            var user = userLoad.Value!.User;
            var session = user.Sessions.Single();
            var currentToken = session.RefreshTokens.Single();

            // Re-check freshly-loaded (tracked) state — the earlier no-tracking `lookup` snapshot
            // can be stale under concurrent refresh requests for the same token: another request
            // may have consumed it between that read and this one. Signal a retry so the fresh read
            // resolves it through the grace window above — never a theft verdict from stale state.
            if (currentToken.ConsumedAt is not null)
            {
                return null;
            }

            if (session.RevokedAt is not null)
            {
                return Result<Response>.Failure(TokenErrors.SessionRevoked);
            }

            var utcNow = timeProvider.GetUtcNow();
            var (accessToken, accessTokenExpiresAt) =
                tokenService.GenerateAccessToken(utcNow, user.Id, lookup.SessionId, userLoad.Value.Roles);
            var (newRefreshTokenBytes, newRefreshTokenExpiresAt) =
                tokenService.GenerateRefreshToken(utcNow);
            var ip = httpContext.Connection.RemoteIpAddress?.ToString();
            var userAgent = httpContext.Request.Headers.UserAgent.ToString();

            user.RotateRefreshToken(
                session, currentToken, SHA256.HashData(newRefreshTokenBytes), ip, userAgent, utcNow,
                newRefreshTokenExpiresAt);
            try
            {
                await dbContext.SaveChangesAsync(cancellationToken);
            }
            // Lost the write race to a concurrent refresh — surfaces either as EF's Version check
            // (DbUpdateConcurrencyException) or, when the racer's insert lands first, as the
            // AuditLog (AggregateId, Version) primary key (UniqueConstraintException). Must not
            // return a token that was never persisted — signal a retry so this request re-resolves
            // via the grace window.
            catch (Exception ex) when (ex is DbUpdateConcurrencyException or UniqueConstraintException)
            {
                return null;
            }

            activity?.SetTag("session.id", lookup.SessionId.ToString());

            return Result<Response>.Success(new Response
            {
                AccessToken = accessToken,
                AccessTokenExpiresAt = accessTokenExpiresAt,
                RefreshToken = Convert.ToBase64String(newRefreshTokenBytes),
                RefreshTokenExpiresAt = newRefreshTokenExpiresAt
            });
        }

        async Task<Result<Response>> RevokeAsReuseAsync(ApplicationUserId userId, SessionId sessionId)
        {
            // Theft signal: a rotated-away token was replayed past its grace window. Revoke only
            // this session and return the same generic error — don't leak detection to the caller.
            LogTokenReuseDetected(logger, userId, sessionId.ToString());
            IamTelemetry.TokenReuseDetected.Add(1);

            try
            {
                await dbContext
                    .Users
                    .Include(u => u.Sessions.Where(s => s.Id == sessionId))
                    .TagWith(nameof(RefreshToken), userId)
                    .Where(u => u.Id == userId)
                    .SingleAsResultAsync(nameof(ApplicationUser), cancellationToken)
                    .TapAsync(user => user.RevokeSession(
                        user.Sessions.Single(), SessionRevokedReason.TokenReuseDetected, timeProvider.GetUtcNow()))
                    .TapAsync(_ => dbContext.SaveChangesAsync(cancellationToken));
            }
            catch (Exception ex) when (ex is DbUpdateConcurrencyException or UniqueConstraintException)
            {
                // A concurrent request mutated the user aggregate first (e.g. revoked the session
                // already); the reuse verdict stands and the caller still gets the generic error.
            }

            return Result<Response>.Failure(TokenErrors.InvalidRefreshToken);
        }
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

    [LoggerMessage(
        Level = LogLevel.Warning,
        Message = "Refresh token reuse detected for user {UserId}, session {SessionId} — revoking session.")]
    private static partial void LogTokenReuseDetected(ILogger logger, ApplicationUserId userId, string sessionId);
}
