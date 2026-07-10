namespace IAM.Endpoints.Tokens.VersionNeutral.Sessions.List;

internal sealed record Response
{
    public required Guid Id { get; init; }
    public required string ClientId { get; init; }
    public string? DeviceName { get; init; }
    public required DateTimeOffset CreatedAt { get; init; }
    public required DateTimeOffset LastUsedAt { get; init; }
    public required bool IsCurrent { get; init; }
}
