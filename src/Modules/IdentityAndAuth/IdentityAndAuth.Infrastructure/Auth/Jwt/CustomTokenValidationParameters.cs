using System.Security.Claims;
using System.Text;
using Common.Infrastructure.Options;
using Microsoft.IdentityModel.Tokens;

namespace IdentityAndAuth.Infrastructure.Auth.Jwt;

internal static class CustomTokenValidationParameters
{
    public static TokenValidationParameters Get(JwtOptions jwtOptions)
        => new()
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtOptions.Secret)),

            ValidateIssuer = true,
            ValidIssuer = jwtOptions.Issuer,

            ValidateLifetime = true,

            ValidAudience = jwtOptions.Audience,
            ValidateAudience = true,

            RoleClaimType = ClaimTypes.Role,

            ClockSkew = TimeSpan.Zero
        };
}
