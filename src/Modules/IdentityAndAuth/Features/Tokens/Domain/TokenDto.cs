namespace IdentityAndAuth.Features.Tokens.Domain;

public sealed record TokenDto(string AccessToken, DateTime AccessTokenExpiresAt, string RefreshToken, DateTime RefreshTokenExpiresAt);
