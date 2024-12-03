namespace IAM.Application.Tokens.VersionNeutral.Create;

public sealed record Response
{
    public required string AccessToken { get; init; }
    public required DateTimeOffset AccessTokenExpiresAt { get; init; }
    public required string RefreshToken { get; init; }
    public required DateTimeOffset RefreshTokenExpiresAt { get; init; }
}
