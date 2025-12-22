using System.Security.Cryptography;
using Common.Application.Extensions;
using Common.Application.Persistence;
using Common.Domain.ResultMonad;
using IAM.Application.Otp.Services;
using IAM.Application.Persistence;
using IAM.Application.Tokens.DTOs;
using IAM.Application.Tokens.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
            .Produces<Response>(StatusCodes.Status200OK)
            .TransformResultTo<Response>();
    }

    private static async Task<Result<Response>> CreateTokens(
        [FromBody] Request request,
        [FromServices] IIAMDbContext dbContext,
        [FromServices] ITokenService tokenService,
        [FromServices] IOtpService otpService,
        [FromServices] TimeProvider timeProvider,
        CancellationToken cancellationToken)
        => await otpService
            .VerifyThenRemoveOtpAsync(request.PhoneNumber, request.Otp, cancellationToken)
            .BindAsync(() => CreateTokensAsync(request, tokenService, dbContext, timeProvider, cancellationToken))
            .MapAsync(tokensDto => new Response
            {
                AccessToken = tokensDto.AccessToken,
                AccessTokenExpiresAt = tokensDto.AccessTokenExpiresAt,
                RefreshToken = tokensDto.RefreshToken,
                RefreshTokenExpiresAt = tokensDto.RefreshTokenExpiresAt
            });

    private static async Task<Result<TokensDto>> CreateTokensAsync(
        Request request,
        ITokenService tokenService,
        IIAMDbContext dbContext, TimeProvider timeProvider, CancellationToken cancellationToken)
    {
        var userResult = await dbContext
            .Users
            .TagWith(nameof(CreateTokensAsync), request.PhoneNumber)
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
            .SingleAsResultAsync(cancellationToken);

        if (userResult.IsFailure)
        {
            return Result<TokensDto>.Failure(userResult.Error!);
        }

        var userObj = userResult.Value!;
        var user = userObj.User;

        var utcNow = timeProvider.GetUtcNow();

        var (accessToken, accessTokenExpiresAt) = tokenService.GenerateAccessToken(utcNow, user.Id, userObj.Roles);
        var (refreshTokenBytes, refreshTokenExpiresAt) = tokenService.GenerateRefreshToken(utcNow);

        user.UpdateRefreshToken(SHA256.HashData(refreshTokenBytes), refreshTokenExpiresAt);

        await dbContext.SaveChangesAsync(cancellationToken);

        return new TokensDto
        {
            AccessToken = accessToken,
            AccessTokenExpiresAt = accessTokenExpiresAt,
            RefreshToken = Convert.ToBase64String(refreshTokenBytes),
            RefreshTokenExpiresAt = refreshTokenExpiresAt
        };
    }
}
