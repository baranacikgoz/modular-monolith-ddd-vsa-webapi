namespace IAM.Endpoints.Tokens.VersionNeutral.Refresh;

internal sealed record Response
{
    public required string AccessToken { get; init; }
    public required DateTimeOffset AccessTokenExpiresAt { get; init; }
}
