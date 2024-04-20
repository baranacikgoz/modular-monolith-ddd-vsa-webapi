using Common.Core.Contracts.Identity;

namespace Common.Core.Auth;

public interface ICurrentUser
{
    string? UserName { get; }
    string? IpAddress { get; }
    ApplicationUserId Id { get; }
    string? IdAsString { get; }
    string? FullName { get; }
    string? PhoneNumber { get; }
}
