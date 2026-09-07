using Common.Domain.StronglyTypedIds;

namespace IAM.Endpoints.Users.VersionNeutral.Get;

public sealed record Response
{
    public required ApplicationUserId Id { get; init; }
    public required string Username { get; init; }
    public string? FirstName { get; init; }
    public string? LastName { get; init; }
    public string? Email { get; init; }
    public string? PhoneNumber { get; init; }
    public DateOnly? BirthDate { get; init; }
    public required bool Enabled { get; init; }
    public required DateTimeOffset CreatedOn { get; init; }
}
