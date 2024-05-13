using Common.Domain.StronglyTypedIds;

namespace Common.Application.Auth;

public interface ICurrentUser
{
    string? IpAddress { get; }
    ApplicationUserId Id { get; }
    string? IdAsString { get; }
}
