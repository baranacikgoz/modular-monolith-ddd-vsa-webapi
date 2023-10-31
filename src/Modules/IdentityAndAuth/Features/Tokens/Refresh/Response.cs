namespace IdentityAndAuth.Features.Tokens.Refresh;

public sealed record Response(string AccessToken, DateTime AccessTokenExpiresAt, string RefreshToken, DateTime RefreshTokenExpiresAt);
