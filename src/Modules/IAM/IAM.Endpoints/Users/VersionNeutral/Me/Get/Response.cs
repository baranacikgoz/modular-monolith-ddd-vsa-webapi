using Common.Application.DTOs;
using Common.Domain.StronglyTypedIds;

namespace IAM.Endpoints.Users.VersionNeutral.Me.Get;

public sealed record Response : AuditableEntityResponse<ApplicationUserId>
{
    public required string Name { get; init; }
    public required string LastName { get; init; }
    public required string PhoneNumber { get; init; }
    public required DateOnly BirthDate { get; init; }
}
