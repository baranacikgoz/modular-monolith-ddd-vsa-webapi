using Common.Application.DTOs;
using Common.Domain.StronglyTypedIds;

namespace IAM.Application.Identity.VersionNeutral.Users.Me.Get;

public sealed record Response : AuditableEntityDto<ApplicationUserId>
{
    public required string Name { get; init; }
    public required string LastName { get; init; }
    public required string PhoneNumber { get; init; }
    public required string NationalIdentityNumber { get; init; }
    public required DateOnly BirthDate { get; init; }
}
