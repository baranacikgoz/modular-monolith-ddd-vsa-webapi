using Common.Domain.StronglyTypedIds;

namespace IAM.Endpoints.Users.VersionNeutral.Me.Get;

public sealed record Response
{
    public required ApplicationUserId Id { get; init; }
    public required string Username { get; init; }
    public string? FirstName { get; init; }
    public string? LastName { get; init; }
    public string? Email { get; init; }
    public string? PhoneNumber { get; init; }
    public DateOnly? BirthDate { get; init; }
    public required DateTimeOffset CreatedOn { get; init; }

    /// <summary>Realm roles from the access token.</summary>
    public required IReadOnlyCollection<string> Roles { get; init; }

    /// <summary>Every <c>resource:action</c> scope Keycloak grants this token, e.g. <c>stores:create-own</c>.</summary>
    public required IReadOnlyCollection<string> Permissions { get; init; }
}
