using Common.Application.DTOs;
using Common.Domain.StronglyTypedIds;

namespace IAM.Endpoints.Users.VersionNeutral.Me.Get;

public sealed record Response : AuditableEntityResponse<ApplicationUserId>
{
    public required string FullName { get; init; }
    public required string PhoneNumber { get; init; }
    public required DateOnly BirthDate { get; init; }

    /// <summary>Roles assigned to the user — UX hint only; the server enforces authorization independently.</summary>
    public required IReadOnlyCollection<string> Roles { get; init; }

    /// <summary>Effective permissions derived from the user's roles — for FE rendering/gating, not a security boundary.</summary>
    public required IReadOnlyCollection<string> Permissions { get; init; }
}
