using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Common.Core.Contracts.Results;
using Common.Options;
using IdentityAndAuth.Auth;
using IdentityAndAuth.Auth.Jwt;
using IdentityAndAuth.Features.Tokens.Errors;
using IdentityAndAuth.Features.Tokens.Services;
using IdentityAndAuth.Features.Users.Domain;
using IdentityAndAuth.Features.Users.Domain.Errors;
using IdentityAndAuth.Features.Users.Services;
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
        => await GetPrincipalFromExpiredToken(request.ExpiredAccessToken, _jwtOptions)
            .Bind(claimsPrincipal =>
            {
                var phoneNumber = claimsPrincipal.GetPhoneNumber();
                return string.IsNullOrWhiteSpace(phoneNumber)
                    ? UserErrors.NotFound
                    : Result<ClaimsPrincipal>.Success(claimsPrincipal);
            })
            .BindAsync(async claimsPrincipal =>
            {
                var phoneNumber = claimsPrincipal.GetPhoneNumber() ?? string.Empty;
                var tokenValidationResult = await tokenService.ValidateRefreshTokenAsync(request.RefreshToken, phoneNumber);
                return tokenValidationResult.IsSuccess
                    ? Result<string>.Success(phoneNumber)
                    : Result<string>.Failure(tokenValidationResult.Error!);
            })
            .BindAsync(phoneNumber => userService.GetByPhoneNumberAsync(phoneNumber, cancellationToken))
            .BindAsync(tokenService.GenerateTokensAndUpdateUserAsync)
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
