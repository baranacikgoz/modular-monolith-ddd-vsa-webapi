namespace IdentityAndAuth.Features.Tokens.UseCases.Create;

public sealed record Response(string AccessToken, DateTime AccessTokenExpiresAt, string RefreshToken, DateTime RefreshTokenExpiresAt);
