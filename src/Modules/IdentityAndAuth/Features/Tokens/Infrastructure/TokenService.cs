using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Common.Core.Contracts.Results;
using Common.Options;
using IdentityAndAuth.Features.Auth.Domain;
using IdentityAndAuth.Features.Identity.Domain;
using IdentityAndAuth.Features.Tokens.Domain;
using IdentityAndAuth.Features.Tokens.Domain.Errors;
using IdentityAndAuth.Features.Tokens.Domain.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace IdentityAndAuth.Features.Tokens.Infrastructure;

internal class TokenService(
    IOptions<JwtOptions> jwtOptionsProvider,
    IUserService userService,
    UserManager<ApplicationUser> userManager)
     : ITokenService
{
    private readonly JwtOptions _jwtOptions = jwtOptionsProvider.Value;

    public async Task<Result<TokenDto>> GenerateTokensAndUpdateUserAsync(ApplicationUser user)
    {
        var tokens = GenerateTokens(user);
        user.UpdateRefreshToken(tokens.RefreshToken, tokens.RefreshTokenExpiresAt);

        return await userService
                        .UpdateAsync(user)
                        .MapAsync(() => tokens);
    }
    public TokenDto GenerateTokens(ApplicationUser user)
    {
        var (accessToken, accessTokenExpiresAt) = GenerateJwt(user);
        var (refreshToken, refreshTokenExpiresAt) = GenerateRefreshToken();

        return new TokenDto(accessToken, accessTokenExpiresAt, refreshToken, refreshTokenExpiresAt);
    }

    public async Task<Result> ValidateRefreshTokenAsync(string refreshToken, string phoneNumber)
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
        => new()
        {
            new(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new(CustomClaims.Fullname, $"{user.Name} {user.LastName}"),
            new(ClaimTypes.Name, user.UserName!),
            new(ClaimTypes.Surname, user.LastName ?? string.Empty),
            new(CustomClaims.ImageUrl, user.ImageUrl?.ToString() ?? string.Empty),
            new(ClaimTypes.MobilePhone, user.PhoneNumber ?? string.Empty),
        };

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
}
