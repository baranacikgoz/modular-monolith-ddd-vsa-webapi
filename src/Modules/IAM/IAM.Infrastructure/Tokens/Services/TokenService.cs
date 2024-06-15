using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Common.Domain.ResultMonad;
using Common.Infrastructure.Options;
using IAM.Domain.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Common.Domain.StronglyTypedIds;
using Microsoft.AspNetCore.Identity;
using IAM.Application.Identity.Services;
using IAM.Application.Tokens.Services;
using IAM.Domain.Tokens;
using IAM.Infrastructure.Auth;
using IAM.Infrastructure.Auth.Jwt;

namespace IAM.Infrastructure.Tokens.Services;

internal class TokenService(
    IOptions<JwtOptions> jwtOptionsProvider,
    IUserService userService)
     : ITokenService
{
    private readonly JwtOptions _jwtOptions = jwtOptionsProvider.Value;

    public async Task<Result<TokenDto>> GenerateTokensAndUpdateUserAsync(ApplicationUser user, CancellationToken cancellationToken)
    {
        var tokens = GenerateTokens(user);
        user.UpdateRefreshToken(tokens.RefreshToken, tokens.RefreshTokenExpiresAt);

        return await userService
                        .UpdateAsync(user, cancellationToken)
                        .MapAsync(() => tokens);
    }
    private TokenDto GenerateTokens(ApplicationUser user)
    {
        var (accessToken, accessTokenExpiresAt) = GenerateJwt(user);
        var (refreshToken, refreshTokenExpiresAt) = GenerateRefreshToken();

        return new TokenDto(accessToken, accessTokenExpiresAt, refreshToken, refreshTokenExpiresAt);
    }

    private (string accessToken, DateTime accessTokenExpiresAt) GenerateJwt(ApplicationUser user)
    {
        var expiresAt = DateTime.UtcNow.AddMinutes(_jwtOptions.AccessTokenExpirationInMinutes);
        var accessToken = new JwtSecurityToken(
           claims: GetClaims(user),
           expires: expiresAt,
           signingCredentials: GetSigningCredentials(),
           audience: _jwtOptions.Audience,
           issuer: _jwtOptions.Issuer);

        var tokenHandler = new JwtSecurityTokenHandler();
        return new(tokenHandler.WriteToken(accessToken), expiresAt);
    }

    private static List<Claim> GetClaims(ApplicationUser user)
        => [new(ClaimTypes.NameIdentifier, user.Id.ToString())];

    private SigningCredentials GetSigningCredentials()
    {
        var secret = Encoding.UTF8.GetBytes(_jwtOptions.Secret);
        return new SigningCredentials(new SymmetricSecurityKey(secret), SecurityAlgorithms.HmacSha256);
    }

    private (string RefreshToken, DateTime RefreshTokenExpiresAt) GenerateRefreshToken()
    {
        var randomNumber = new byte[32];
        using (var rng = RandomNumberGenerator.Create())
        {
            rng.GetBytes(randomNumber);
        }
        var refreshToken = Convert.ToBase64String(randomNumber);
        var refreshTokenExpiresAt = DateTime.UtcNow.AddDays(_jwtOptions.RefreshTokenExpirationInDays);

        return (refreshToken, refreshTokenExpiresAt);
    }

    public async Task<Result<ClaimsPrincipal>> GetClaimsPrincipalByExpiredToken(string refreshToken, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(refreshToken))
        {
            return TokenErrors.InvalidRefreshToken;
        }

        var tokenHandler = new JwtSecurityTokenHandler();
        if (!tokenHandler.CanReadToken(refreshToken))
        {
            return TokenErrors.InvalidToken;
        }

        var tokenValidationParameters = CustomTokenValidationParameters.Get(_jwtOptions);
#pragma warning disable CA1849
        var claimsPrincipal = tokenHandler.ValidateToken(refreshToken, tokenValidationParameters, out var securityToken);
#pragma warning restore CA1849

        if (securityToken is not JwtSecurityToken jwtSecurityToken ||
            !jwtSecurityToken.Header.Alg.Equals(
                SecurityAlgorithms.HmacSha256,
                StringComparison.OrdinalIgnoreCase))
        {
            return TokenErrors.InvalidToken;
        }

        var userId = claimsPrincipal.GetUserId();

        var refreshTokenExpiresAt = await userService.GetRefreshTokenExpiresAt(userId, refreshToken, cancellationToken);

        if (refreshTokenExpiresAt is null)
        {
            return TokenErrors.InvalidRefreshToken;
        }

        if (refreshTokenExpiresAt < DateTime.UtcNow)
        {
            return TokenErrors.RefreshTokenExpired;
        }

        return claimsPrincipal;
    }
}
