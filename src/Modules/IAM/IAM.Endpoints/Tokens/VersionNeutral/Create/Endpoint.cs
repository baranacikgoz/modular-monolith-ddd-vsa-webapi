using System.Security.Cryptography;
using Common.Application.Extensions;
using Common.Domain.ResultMonad;
using Common.Infrastructure.Extensions;
using Common.Infrastructure.Persistence.Extensions;
using IAM.Application.Otp.Services;
using IAM.Application.Persistence;
using IAM.Application.Tokens.Services;
using IAM.Domain.Identity;
using IAM.Infrastructure.Telemetry;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace IAM.Endpoints.Tokens.VersionNeutral.Create;

internal static class Endpoint
{
    internal static void MapEndpoint(RouteGroupBuilder tokensApiGroup)
    {
        tokensApiGroup
            .MapPost("", CreateTokens)
            .WithDescription("Create tokens.")
            .AllowAnonymous()
            .Produces<Response>()
            .TransformResultTo<Response>();
    }

    private static async Task<Result<Response>> CreateTokens(
        Request request,
        IIAMDbContext dbContext,
        ITokenService tokenService,
        IOtpService otpService,
        TimeProvider timeProvider,
        CancellationToken cancellationToken)
    {
        using var activity = IamTelemetry.ActivitySource.StartActivityForCaller();

        return await otpService
            .VerifyThenRemoveOtpAsync(request.PhoneNumber, request.Otp, cancellationToken)
            .BindAsync(() => dbContext
                .Users
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
                .SingleAsResultAsync(resourceName: nameof(ApplicationUser), cancellationToken))
            .BindAsync(async userObj =>
            {
                var utcNow = timeProvider.GetUtcNow();
                var (accessToken, accessTokenExpiresAt) =
                    tokenService.GenerateAccessToken(utcNow, userObj.User.Id, userObj.Roles);
                var (refreshTokenBytes, refreshTokenExpiresAt) = tokenService.GenerateRefreshToken(utcNow);

                userObj.User.UpdateRefreshToken(SHA256.HashData(refreshTokenBytes), refreshTokenExpiresAt);
                await dbContext.SaveChangesAsync(cancellationToken);

                return Result<Response>.Success(new Response
                {
                    AccessToken = accessToken,
                    AccessTokenExpiresAt = accessTokenExpiresAt,
                    RefreshToken = Convert.ToBase64String(refreshTokenBytes),
                    RefreshTokenExpiresAt = refreshTokenExpiresAt
                });
            })
            .TapAsync(_ => IamTelemetry.TokensIssued.Add(1))
            .TapActivityAsync(activity);
    }
}
