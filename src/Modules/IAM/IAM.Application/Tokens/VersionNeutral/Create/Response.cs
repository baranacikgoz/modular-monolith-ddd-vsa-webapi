namespace IAM.Application.Tokens.VersionNeutral.Create;

public sealed record Response(
    string AccessToken,
    DateTime AccessTokenExpiresAt,
    string RefreshToken,
    DateTime RefreshTokenExpiresAt);
