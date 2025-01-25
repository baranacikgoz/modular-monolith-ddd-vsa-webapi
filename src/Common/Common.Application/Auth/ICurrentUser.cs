using Common.Domain.StronglyTypedIds;

namespace Common.Application.Auth;

public interface ICurrentUser
{
    string? IpAddress { get; }
    ApplicationUserId Id { get; }
    string? IdAsString { get; }
    ICollection<string> Roles { get; }
    bool HasPermission(string permission);
}
