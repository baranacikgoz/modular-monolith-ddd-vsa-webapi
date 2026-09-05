using System.Diagnostics;
using System.Diagnostics.Metrics;

namespace IAM.Infrastructure.Telemetry;

/// <summary>
///     Centralized telemetry definitions for the IAM module.
///     ActivitySource and Meter are thread-safe singletons by design.
/// </summary>
public static class IamTelemetry
{
    private const string Prefix = "ModularMonolith";

    /// <summary>ActivitySource name: "ModularMonolith.IAM"</summary>
    public const string ActivitySourceName = Prefix + "." + nameof(IAM);

    /// <summary>Meter name: "ModularMonolith.IAM"</summary>
    public const string MeterName = Prefix + "." + nameof(IAM);

    public static readonly ActivitySource ActivitySource = new(ActivitySourceName);

    public static readonly Meter Meter = new(MeterName);

    public static readonly Counter<long> Logins =
        Meter.CreateCounter<long>("iam.logins.total", description: "Total successful logins (including auto-login after registration), tagged by method");

    public static readonly Counter<long> UsersRegistered =
        Meter.CreateCounter<long>("iam.users_registered.total", description: "Total users registered");

    public static readonly Counter<long> SessionsRevoked =
        Meter.CreateCounter<long>("iam.sessions_revoked.total", description: "Total Keycloak sessions revoked through this API, tagged by reason");

    public static readonly Counter<long> AuthorizationDecisions =
        Meter.CreateCounter<long>("iam.authorization_decisions.total", description: "Permission decisions, tagged by result and whether they came from cache");

    public static readonly Counter<long> RefreshTokenReuseDetected =
        Meter.CreateCounter<long>("iam.refresh_token_reuse_detected.total", description: "Refresh tokens replayed beyond the realm's reuse tolerance (theft signal); the session is revoked in response");

    public static void RecordLogin(string method) =>
        Logins.Add(1, new KeyValuePair<string, object?>("login.method", method));

    public static void RecordSessionRevoked(string reason, int count = 1) =>
        SessionsRevoked.Add(count, new KeyValuePair<string, object?>("session.revoked_reason", reason));

    public static void RecordRefreshTokenReuseDetected() => RefreshTokenReuseDetected.Add(1);

    public static void RecordAuthorizationDecision(bool granted, bool fromCache) =>
        AuthorizationDecisions.Add(1,
            new KeyValuePair<string, object?>("authorization.granted", granted),
            new KeyValuePair<string, object?>("authorization.from_cache", fromCache));
}

public static class LoginMethods
{
    public const string PhoneOtp = "phone_otp";
    public const string EmailPassword = "email_password";
    public const string Registration = "registration";
}

public static class SessionRevokedReasons
{
    public const string UserSignedOut = "user_signed_out";
    public const string RevokedByUser = "revoked_by_user";
    public const string RevokedAllByUser = "revoked_all_by_user";
    public const string SupersededByNewLogin = "superseded_by_new_login";
    public const string TokenReuseDetected = "token_reuse_detected";
}
