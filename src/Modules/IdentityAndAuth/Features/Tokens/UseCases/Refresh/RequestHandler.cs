using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Common.Core;
using Common.Core.Contracts.Results;
using Common.Options;
using IdentityAndAuth.Features.Auth.Extensions;
using IdentityAndAuth.Features.Auth.Infrastructure.Jwt;
using IdentityAndAuth.Features.Identity.Domain;
using IdentityAndAuth.Features.Identity.Domain.Errors;
using IdentityAndAuth.Features.Tokens.Domain.Errors;
using IdentityAndAuth.Features.Tokens.Domain.Services;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using NimbleMediator.Contracts;

namespace IdentityAndAuth.Features.Tokens.UseCases.Refresh;

internal sealed class RequestHandler(
       IOptions<JwtOptions> jwtOptionsProvider,
       ITokenService tokenService,
       IUserService userService
   ) : IRequestHandler<Request, Result<Response>>
{
    private readonly JwtOptions _jwtOptions = jwtOptionsProvider.Value;

    public async ValueTask<Result<Response>> HandleAsync(Request request, CancellationToken cancellationToken)
    => await GetPrincipalFromExpiredToken(request.ExpiredAccessToken, _jwtOptions)
        .Bind(claimsPrincipal => claimsPrincipal.GetPhoneNumber())
        .Bind(phoneNumber => StringExt.EnsureNotNullOrEmpty(phoneNumber, ifNull: TokenErrors.InvalidToken))
        .BindAsync(async phoneNumber => await tokenService.ValidateRefreshTokenAsync(request.RefreshToken, phoneNumber))
        .BindAsync(async phoneNumber => await userService.GetByPhoneNumberAsync(phoneNumber, cancellationToken))
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
}
