using Common.Core.Contracts.Results;

namespace IdentityAndAuth.Features.Tokens.Errors;

internal static class TokenErrors
{
    public static readonly Error InvalidToken = new(nameof(InvalidToken));
    public static readonly Error InvalidRefreshToken = new(nameof(InvalidRefreshToken));
    public static readonly Error RefreshTokenExpired = new(nameof(RefreshTokenExpired));
}
