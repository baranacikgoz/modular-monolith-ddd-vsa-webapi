﻿using System.Security.Claims;
using System.Text;
using Common.Options;
using Microsoft.IdentityModel.Tokens;

namespace IdentityAndAuth.Features.Auth.Infrastructure.Jwt;

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
