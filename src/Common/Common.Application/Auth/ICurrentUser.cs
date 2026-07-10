using Common.Domain.StronglyTypedIds;

namespace Common.Application.Auth;

public interface ICurrentUser
{
    ApplicationUserId Id { get; }
    string? IdAsString { get; }
    ICollection<string> Roles { get; }
    Guid? SessionId { get; }
    bool HasPermission(string permission);
}
