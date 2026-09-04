using System.Net;
using Common.Domain.ResultMonad;

namespace IAM.Domain.Errors;

public static class IdentityErrors
{
    public static readonly Error PhoneNumberAlreadyRegistered = new()
    {
        Key = nameof(PhoneNumberAlreadyRegistered),
        StatusCode = HttpStatusCode.Conflict
    };

    /// <summary>Wrong email/password, unknown user, disabled or temporarily locked account. One message for all: never leak which.</summary>
    public static readonly Error InvalidCredentials = new()
    {
        Key = nameof(InvalidCredentials),
        StatusCode = HttpStatusCode.Unauthorized
    };

    /// <summary>Keycloak rejected the user representation (user profile validation).</summary>
    public static readonly Error IdentityProviderRejectedUser = new()
    {
        Key = nameof(IdentityProviderRejectedUser),
        StatusCode = HttpStatusCode.BadRequest
    };

    /// <summary>Keycloak is unreachable, timed out, the circuit is open, or it answered with something unexpected.</summary>
    public static readonly Error IdentityProviderUnavailable = new()
    {
        Key = nameof(IdentityProviderUnavailable),
        StatusCode = HttpStatusCode.BadGateway
    };
}
