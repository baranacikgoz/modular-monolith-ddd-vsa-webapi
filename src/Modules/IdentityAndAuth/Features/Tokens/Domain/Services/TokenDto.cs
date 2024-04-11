namespace IdentityAndAuth.Features.Tokens.Domain.Services;

public sealed record TokenDto(
    string AccessToken,
    DateTime AccessTokenExpiresAt,
    string RefreshToken,
    DateTime RefreshTokenExpiresAt);
