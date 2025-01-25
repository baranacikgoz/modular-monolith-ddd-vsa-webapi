using Common.Domain.StronglyTypedIds;

namespace IAM.Endpoints.Users.VersionNeutral.SelfRegister;

public sealed record Response
{
    public required ApplicationUserId Id { get; init; }
}
