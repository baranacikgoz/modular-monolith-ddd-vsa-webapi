using System.Net;
using Common.Domain.ResultMonad;

namespace IAM.Domain.Errors;

public static class TokenErrors
{
    /// <summary>Expired, revoked, replayed after rotation, or issued by an unknown client. One message for all.</summary>
    public static readonly Error InvalidRefreshToken = new()
    {
        Key = nameof(InvalidRefreshToken),
        StatusCode = HttpStatusCode.Unauthorized
    };

    public static readonly Error SessionNotFound = new()
    {
        Key = nameof(SessionNotFound),
        StatusCode = HttpStatusCode.NotFound
    };
}
