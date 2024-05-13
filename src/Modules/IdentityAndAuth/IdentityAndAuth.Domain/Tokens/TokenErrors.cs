using Common.Domain.ResultMonad;

namespace IdentityAndAuth.Domain.Tokens;

public static class TokenErrors
{
    public static readonly Error InvalidToken = new() { Key = nameof(InvalidToken) };
    public static readonly Error InvalidRefreshToken = new() { Key = nameof(InvalidRefreshToken) };
    public static readonly Error RefreshTokenExpired = new() { Key = nameof(RefreshTokenExpired) };
}
