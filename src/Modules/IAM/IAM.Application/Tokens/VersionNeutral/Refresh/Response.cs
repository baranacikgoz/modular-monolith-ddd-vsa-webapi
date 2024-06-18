namespace IAM.Application.Tokens.VersionNeutral.Refresh;

public sealed record Response(string AccessToken, DateTimeOffset AccessTokenExpiresAt, string RefreshToken, DateTimeOffset RefreshTokenExpiresAt);
