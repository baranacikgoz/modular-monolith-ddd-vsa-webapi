using System.Net;
using Common.Domain.ResultMonad;

namespace IAM.Domain.Errors;

public static class TokenErrors
{
    public static readonly Error RefreshTokenExpired = new()
    {
        Key = nameof(RefreshTokenExpired),
        StatusCode = HttpStatusCode.Unauthorized
    };

    public static readonly Error InvalidRefreshToken = new()
    {
        Key = nameof(InvalidRefreshToken),
        StatusCode = HttpStatusCode.Unauthorized
    };

    public static readonly Error SessionRevoked = new()
    {
        Key = nameof(SessionRevoked),
        StatusCode = HttpStatusCode.Unauthorized
    };

    public static readonly Error SessionNotFound = new()
    {
        Key = nameof(SessionNotFound),
        StatusCode = HttpStatusCode.NotFound
    };
}
