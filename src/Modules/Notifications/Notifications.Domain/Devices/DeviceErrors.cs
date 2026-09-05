using System.Net;
using Common.Domain.ResultMonad;

namespace Notifications.Domain.Devices;

public static class DeviceErrors
{
    /// <summary>The caller's Keycloak session has no active device registration (e.g. a service account, or a login that skipped device binding).</summary>
    public static readonly Error RegistrationNotFound = new()
    {
        Key = nameof(RegistrationNotFound),
        StatusCode = HttpStatusCode.NotFound
    };
}
