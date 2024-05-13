namespace IdentityAndAuth.Application.Tokens.Services;

public sealed record TokenDto(
    string AccessToken,
    DateTime AccessTokenExpiresAt,
    string RefreshToken,
    DateTime RefreshTokenExpiresAt);
