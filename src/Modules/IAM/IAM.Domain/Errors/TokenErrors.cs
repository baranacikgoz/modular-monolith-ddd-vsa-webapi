using Common.Domain.ResultMonad;

namespace IAM.Domain.Errors;

public static class TokenErrors
{
    public static readonly Error RefreshTokenExpired = new() { Key = nameof(RefreshTokenExpired) };
}
