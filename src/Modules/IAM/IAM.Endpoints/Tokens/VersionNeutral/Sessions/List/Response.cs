namespace IAM.Endpoints.Tokens.VersionNeutral.Sessions.List;

internal sealed record Response
{
    /// <summary>Keycloak session id (opaque string).</summary>
    public required string Id { get; init; }

    /// <summary>Client app the device registered with, when the session was created through a device login.</summary>
    public string? ClientId { get; init; }

    public string? DeviceName { get; init; }
    public string? IpAddress { get; init; }
    public required DateTimeOffset StartedAt { get; init; }
    public required DateTimeOffset LastAccessAt { get; init; }
    public required bool IsCurrent { get; init; }
}
