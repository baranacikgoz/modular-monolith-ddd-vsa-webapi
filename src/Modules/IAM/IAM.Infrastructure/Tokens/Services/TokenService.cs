using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Common.Infrastructure.Options;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using IAM.Application.Tokens.Services;
using Common.Domain.StronglyTypedIds;

namespace IAM.Infrastructure.Tokens.Services;

internal class TokenService(IOptions<JwtOptions> jwtOptionsProvider) : ITokenService
{
    public (string accessToken, DateTimeOffset expiresAt) GenerateAccessToken(DateTimeOffset now, ApplicationUserId userId, ICollection<string> roles)
    {
        var jwtOptions = jwtOptionsProvider.Value;

        var claims = new List<Claim>(2 + roles.Count)
        {
            new(ClaimTypes.NameIdentifier, userId.ToString()),
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

        var accessTokenExpiresAt = now.AddMinutes(jwtOptions.AccessTokenExpirationInMinutes);
        var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.Secret));
        var signingCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);

        var rawAccessToken = new JwtSecurityToken(
            audience: jwtOptions.Audience,
            issuer: jwtOptions.Issuer,
            claims: claims,
            expires: accessTokenExpiresAt.UtcDateTime,
            signingCredentials: signingCredentials);

        var accessToken = new JwtSecurityTokenHandler().WriteToken(rawAccessToken);

        return (accessToken, accessTokenExpiresAt);
    }

    public (byte[] refreshTokenBytes, DateTimeOffset expiresAt) GenerateRefreshToken(DateTimeOffset now)
    {
        var jwtOptions = jwtOptionsProvider.Value;

        var refreshTokenExpiresAt = now.AddDays(jwtOptions.RefreshTokenExpirationInDays);
        var randomNumber = new byte[32];
        using (var rng = RandomNumberGenerator.Create())
        {
            rng.GetBytes(randomNumber);
        }

        return (randomNumber, refreshTokenExpiresAt);
    }
}
