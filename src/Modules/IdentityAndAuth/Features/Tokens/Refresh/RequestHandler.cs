using System.Formats.Asn1;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Common.Core.Contracts;
using Common.Core.Contracts.Results;
using Common.Options;
using IdentityAndAuth;
using IdentityAndAuth.Auth;
using IdentityAndAuth.Auth.Jwt;
using IdentityAndAuth.Features.Tokens.Errors;
using IdentityAndAuth.Features.Tokens.Services;
using IdentityAndAuth.Features.Users.Domain;
using IdentityAndAuth.Features.Users.Domain.Errors;
using IdentityAndAuth.Features.Users.Services;
using IdentityAndAuth.Identity;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Connections.Features;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using NimbleMediator.Contracts;

namespace IdentityAndAuth.Features.Tokens.Refresh;

internal sealed class RequestHandler(
       IOptions<JwtOptions> jwtOptionsProvider,
       ITokenService tokenService,
       IUserService userService
   ) : IRequestHandler<Request, Result<Response>>
{
    private readonly JwtOptions _jwtOptions = jwtOptionsProvider.Value;
    public async ValueTask<Result<Response>> HandleAsync(Request request, CancellationToken cancellationToken)
    {
        return await GetPrincipalFromExpiredToken(request.ExpiredAccessToken, _jwtOptions)
            .BindAsync(async principal =>
            {
                var phoneNumber = principal.GetPhoneNumber();
                if (string.IsNullOrWhiteSpace(phoneNumber))
                {
                    return UserErrors.NotFound;
                }
                return await userService.GetByPhoneNumberAsync(phoneNumber, cancellationToken);
            })
            .BindAsync(user => ValidateRefreshTokenAndGenerateTokens(user, request.RefreshToken));
    }

    private async Task<Result<Response>> ValidateRefreshTokenAndGenerateTokens(ApplicationUser user, string refreshToken)
    {
        var validateResult = await tokenService.ValidateRefreshTokenAsync(refreshToken, user.PhoneNumber ?? string.Empty);
        if (!validateResult.IsSuccess)
        {
            return validateResult.Error!;
        }

        var tokenDtoResult = await tokenService.GenerateTokensAndUpdateUserAsync(user);
        if (!tokenDtoResult.IsSuccess)
        {
            return tokenDtoResult.Error!;
        }

        var tokenDto = tokenDtoResult.Value!;
        return new Response(
            tokenDto.AccessToken,
            tokenDto.AccessTokenExpiresAt,
            tokenDto.RefreshToken,
            tokenDto.RefreshTokenExpiresAt);
    }

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
}
