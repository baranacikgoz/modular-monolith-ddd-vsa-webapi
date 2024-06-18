namespace IAM.Application.Tokens.VersionNeutral.Create;

public sealed record Response(
    string AccessToken,
    DateTimeOffset AccessTokenExpiresAt,
    string RefreshToken,
    DateTimeOffset RefreshTokenExpiresAt);
