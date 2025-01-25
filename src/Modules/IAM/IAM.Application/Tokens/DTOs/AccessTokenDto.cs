namespace IAM.Application.Tokens.DTOs;

public sealed record AccessTokenDto
{
    public required string AccessToken { get; init; }
    public required DateTimeOffset AccessTokenExpiresAt { get; init; }
}
