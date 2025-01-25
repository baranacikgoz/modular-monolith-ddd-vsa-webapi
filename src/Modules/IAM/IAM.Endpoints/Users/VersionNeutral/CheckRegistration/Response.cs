namespace IAM.Endpoints.Users.VersionNeutral.CheckRegistration;

internal sealed record Response
{
    public bool IsRegistered { get; init; }
}
