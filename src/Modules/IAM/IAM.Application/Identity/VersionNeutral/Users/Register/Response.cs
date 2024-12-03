using Common.Domain.StronglyTypedIds;

namespace IAM.Application.Identity.VersionNeutral.Users.Register;

public sealed record Response
{
    public required ApplicationUserId Id { get; init; }
}
