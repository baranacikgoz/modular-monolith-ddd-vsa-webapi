namespace Common.Core.Auth;

public interface ICurrentUser
{
    string? UserName { get; }
    string? IpAddress { get; }
    Guid Id { get; }
    string? IdAsString { get; }
    string? FullName { get; }
    string? PhoneNumber { get; }
}
