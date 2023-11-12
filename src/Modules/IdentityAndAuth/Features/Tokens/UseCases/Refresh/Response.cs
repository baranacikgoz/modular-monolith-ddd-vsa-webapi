namespace IdentityAndAuth.Features.Tokens.UseCases.Refresh;

public sealed record Response(string AccessToken, DateTime AccessTokenExpiresAt, string RefreshToken, DateTime RefreshTokenExpiresAt);
