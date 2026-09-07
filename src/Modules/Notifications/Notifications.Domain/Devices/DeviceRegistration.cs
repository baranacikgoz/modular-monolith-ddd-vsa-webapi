using Common.Domain.Entities;
using Common.Domain.StronglyTypedIds;

namespace Notifications.Domain.Devices;

public readonly record struct DeviceRegistrationId(DefaultIdType Value) : IStronglyTypedId
{
    public static DeviceRegistrationId New()
    {
        return new DeviceRegistrationId(DefaultIdType.CreateVersion7());
    }

    public override string ToString()
    {
        return Value.ToString();
    }

    public static bool TryParse(string str, out DeviceRegistrationId id)
    {
        return StronglyTypedIdHelper.TryDeserialize(str, out id);
    }
}

/// <summary>
///     One (user, device, client app) triple and the Keycloak session it currently holds. Keycloak owns the
///     session itself (lifetime, refresh, revocation); this row only carries what Keycloak does not know:
///     the device identity, its friendly name and its push token.
/// </summary>
public sealed class DeviceRegistration : AuditableEntity<DeviceRegistrationId>
{
    private DeviceRegistration() : base(new DeviceRegistrationId(DefaultIdType.Empty))
    {
    } // EF Core needs a parameterless ctor

    public ApplicationUserId UserId { get; private init; }
    public Guid DeviceId { get; private init; }
    public string ClientId { get; private init; } = string.Empty;

    /// <summary>Keycloak session id (<c>sid</c> claim) this device signed in with most recently.</summary>
    public string SessionId { get; private set; } = string.Empty;

    public string? DeviceName { get; private set; }
    public string? PushToken { get; private set; }
    public DateTimeOffset? PushTokenUpdatedOn { get; private set; }

    /// <summary>False once the session was revoked; the row stays so a re-login on the same device reuses it.</summary>
    public bool IsActive { get; private set; }

    public static DeviceRegistration Create(
        ApplicationUserId userId, Guid deviceId, string clientId, string sessionId,
        string? deviceName, string? pushToken, DateTimeOffset now)
    {
        var registration = new DeviceRegistration
        {
            Id = DeviceRegistrationId.New(),
            UserId = userId,
            DeviceId = deviceId,
            ClientId = clientId,
            SessionId = sessionId,
            DeviceName = deviceName,
            IsActive = true
        };

        if (pushToken is not null)
        {
            registration.SetPushToken(pushToken, now);
        }

        return registration;
    }

    /// <summary>
    ///     Re-login on the same device: points the row at the new Keycloak session and returns the session it
    ///     replaced while that one was still active, so the caller can revoke it in Keycloak.
    /// </summary>
    public string? Rebind(string sessionId, string? deviceName, string? pushToken, DateTimeOffset now)
    {
        var superseded = IsActive && SessionId != sessionId ? SessionId : null;

        SessionId = sessionId;
        IsActive = true;

        // Only overwrite the friendly label when a new one is actually supplied: a re-login that omits
        // DeviceName must not silently wipe a name the user set on a prior login.
        if (!string.IsNullOrWhiteSpace(deviceName))
        {
            DeviceName = deviceName;
        }

        if (pushToken is not null)
        {
            SetPushToken(pushToken, now);
        }

        return superseded;
    }

    /// <summary>Sets or rotates the FCM token. Pass null to clear it (the client unregistered locally).</summary>
    public void SetPushToken(string? token, DateTimeOffset now)
    {
        PushToken = token;
        PushTokenUpdatedOn = now;
    }

    public void Deactivate()
    {
        IsActive = false;
    }
}
