using System.Security.Cryptography;
using Common.Application.Extensions;
using Common.Application.Options;
using Common.Domain.ResultMonad;
using Common.Infrastructure.Extensions;
using Common.Infrastructure.Persistence.Extensions;
using Common.InterModuleRequests.Contracts;
using Common.InterModuleRequests.Notifications;
using IAM.Application.Persistence;
using IAM.Application.Tokens.Services;
using IAM.Domain.Identity;
using IAM.Endpoints.Otp;
using IAM.Infrastructure.Telemetry;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Constants = IAM.Infrastructure.RateLimiting.Constants;

namespace IAM.Endpoints.Tokens.VersionNeutral.Create;

internal static class Endpoint
{
    internal static void MapEndpoint(RouteGroupBuilder tokensApiGroup)
    {
        tokensApiGroup
            .MapPost("", CreateTokens)
            .WithDescription("Create tokens.")
            .AllowAnonymous()
            .RequireRateLimiting(Constants.TokenCreate)
            .Produces<Response>()
            .TransformResultTo<Response>();
    }

    private static async Task<Result<Response>> CreateTokens(
        Request request,
        IIAMDbContext dbContext,
        ITokenService tokenService,
        IInterModuleRequestClient<VerifyPhoneOtpRequest, VerifyPhoneOtpResponse> otpClient,
        IOptions<JwtOptions> jwtOptionsProvider,
        HttpContext httpContext,
        TimeProvider timeProvider,
        CancellationToken cancellationToken)
    {
        using var activity = IamTelemetry.ActivitySource.StartActivityForCaller();

        var verifyOtpResponse = await otpClient.SendAsync(
            new VerifyPhoneOtpRequest(request.PhoneNumber, request.Otp, OtpPurposes.Login),
            cancellationToken);

        return await verifyOtpResponse
            .ToResult()
            .BindAsync(() => dbContext
                .Users
                .Include(u => u.Sessions.Where(s => s.DeviceId == request.DeviceId && s.ClientId == request.ClientId))
                .ThenInclude(s => s.RefreshTokens.Where(rt => rt.ConsumedAt == null))
                .TagWith(nameof(CreateTokens), request.PhoneNumber)
                .Where(x => x.PhoneNumber == request.PhoneNumber)
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
                .SingleAsResultAsync(nameof(ApplicationUser), cancellationToken))
            .BindAsync(async userObj =>
            {
                var utcNow = timeProvider.GetUtcNow();
                var (refreshTokenBytes, refreshTokenExpiresAt) = tokenService.GenerateRefreshToken(utcNow);
                var sessionAbsoluteExpiresAt = utcNow.AddDays(jwtOptionsProvider.Value.SessionAbsoluteExpirationInDays);
                var ip = httpContext.Connection.RemoteIpAddress?.ToString();
                var userAgent = httpContext.Request.Headers.UserAgent.ToString();

                var existingSession = userObj.User.Sessions.SingleOrDefault();

                var refreshToken = userObj.User.IssueSessionAndToken(
                    existingSession, request.DeviceId, request.ClientId, request.DeviceName, ip, userAgent,
                    SHA256.HashData(refreshTokenBytes), utcNow, refreshTokenExpiresAt, sessionAbsoluteExpiresAt);

                var (accessToken, accessTokenExpiresAt) = tokenService.GenerateAccessToken(
                    utcNow, userObj.User.Id, refreshToken.SessionId, userObj.Roles);

                await dbContext.SaveChangesAsync(cancellationToken);

                if (existingSession is null)
                {
                    IamTelemetry.SessionsCreated.Add(1);
                }

                activity?.SetTag("session.id", refreshToken.SessionId.ToString());

                return Result<Response>.Success(new Response
                {
                    AccessToken = accessToken,
                    AccessTokenExpiresAt = accessTokenExpiresAt,
                    RefreshToken = Convert.ToBase64String(refreshTokenBytes),
                    RefreshTokenExpiresAt = refreshTokenExpiresAt
                });
            })
            .TapAsync(_ => IamTelemetry.Logins.Add(1))
            .TapActivityAsync(activity);
    }
}
