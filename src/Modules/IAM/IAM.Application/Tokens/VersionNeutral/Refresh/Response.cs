namespace IAM.Application.Tokens.VersionNeutral.Refresh;

public sealed record Response(string AccessToken, DateTime AccessTokenExpiresAt, string RefreshToken, DateTime RefreshTokenExpiresAt);
