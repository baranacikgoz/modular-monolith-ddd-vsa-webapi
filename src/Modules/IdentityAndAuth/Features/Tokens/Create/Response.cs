namespace IdentityAndAuth.Features.Tokens.Create;

public sealed record Response(string AccessToken, DateTime AccessTokenExpiresAt, string RefreshToken, DateTime RefreshTokenExpiresAt);
