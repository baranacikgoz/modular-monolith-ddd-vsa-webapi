namespace IAM.Application.Identity.VersionNeutral.Users.CheckRegistration;

internal sealed record Response
{
    public bool IsRegistered { get; init; }
}
