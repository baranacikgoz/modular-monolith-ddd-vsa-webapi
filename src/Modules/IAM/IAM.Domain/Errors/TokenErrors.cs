using Common.Domain.ResultMonad;

namespace IAM.Domain.Errors;

public static class TokenErrors
{
    public static readonly Error RefreshTokenExpired = new() { Key = nameof(RefreshTokenExpired) };
    public static readonly Error InvalidRefreshToken = new() { Key = nameof(InvalidRefreshToken) };
}
