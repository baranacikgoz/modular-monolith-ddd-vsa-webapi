using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Common.Core.Contracts.Results;
using Common.Core.Extensions;
using Common.Options;
using IdentityAndAuth.Features.Auth.Infrastructure;
using IdentityAndAuth.Features.Auth.Infrastructure.Jwt;
using IdentityAndAuth.Features.Identity.Domain;
using IdentityAndAuth.Features.Tokens.Domain.Errors;
using IdentityAndAuth.Features.Tokens.Domain.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace IdentityAndAuth.Features.Tokens.UseCases.Refresh;

internal static class Endpoint
{
    internal static void MapEndpoint(RouteGroupBuilder usersApiGroup)
    {
        usersApiGroup
            .MapPost("refresh", RefreshAsync)
            .WithDescription("Refresh token by validating expired access token and refresh token.")
            .Produces<Response>(StatusCodes.Status200OK)
            .AllowAnonymous()
            .TransformResultTo<Response>();
    }
    private static async Task<Result<Response>> RefreshAsync(
        [FromBody] Request request,
        [FromServices] IOptions<JwtOptions> jwtOptionsProvider,
        [FromServices] IUserService userService,
        [FromServices] ITokenService tokenService,
        [FromServices] UserManager<ApplicationUser> userManager,
        CancellationToken cancellationToken)
        => await GetPrincipalFromExpiredToken(request.ExpiredAccessToken, jwtOptionsProvider.Value)
            .Bind(claimsPrincipal => claimsPrincipal.GetPhoneNumber())
            .Tap(phoneNumber => StringExt.EnsureNotNullOrEmpty(phoneNumber, ifNullOrEmpty: TokenErrors.InvalidToken))
            .TapAsync(async phoneNumber => await userManager.ValidateRefreshTokenAsync(request.RefreshToken, phoneNumber!))
            .BindAsync(async phoneNumber => await userService.GetByPhoneNumberAsync(phoneNumber!, cancellationToken))
            .BindAsync(async user => await tokenService.GenerateTokensAndUpdateUserAsync(user))
            .MapAsync(tokenDto => new Response(
                                        tokenDto.AccessToken,
                                        tokenDto.AccessTokenExpiresAt,
                                        tokenDto.RefreshToken,
                                        tokenDto.RefreshTokenExpiresAt));

    private static Result<ClaimsPrincipal> GetPrincipalFromExpiredToken(string token, JwtOptions jwtOptions)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        if (!tokenHandler.CanReadToken(token))
        {
            return TokenErrors.InvalidToken;
        }

        var tokenValidationParameters = CustomTokenValidationParameters.Get(jwtOptions);
        var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out var securityToken);

        if (securityToken is not JwtSecurityToken jwtSecurityToken ||
            !jwtSecurityToken.Header.Alg.Equals(
                SecurityAlgorithms.HmacSha256,
                StringComparison.OrdinalIgnoreCase))
        {
            return TokenErrors.InvalidToken;
        }

        return principal;
    }

    private static async Task<Result> ValidateRefreshTokenAsync(
        this UserManager<ApplicationUser> userManager,
        string refreshToken,
        string phoneNumber)
    {
        if (string.IsNullOrWhiteSpace(refreshToken))
        {
            return TokenErrors.InvalidRefreshToken;
        }

        var refreshTokenExpiresAt = await userManager
                                        .Users
                                        .Where(u => u.PhoneNumber == phoneNumber && u.RefreshToken == refreshToken)
                                        .Select(u => u.RefreshTokenExpiresAt)
                                        .FirstOrDefaultAsync();

        if (refreshTokenExpiresAt == default)
        {
            return TokenErrors.InvalidRefreshToken;
        }

        if (refreshTokenExpiresAt < DateTime.UtcNow)
        {
            return TokenErrors.RefreshTokenExpired;
        }

        return Result.Success;
    }
}
