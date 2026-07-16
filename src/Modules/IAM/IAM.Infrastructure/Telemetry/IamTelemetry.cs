using System.Diagnostics;
using System.Diagnostics.Metrics;
using IAM.Domain.Identity.Sessions;

namespace IAM.Infrastructure.Telemetry;

/// <summary>
///     Centralized telemetry definitions for the IAM module.
///     ActivitySource and Meter are thread-safe singletons by design.
///     Names derived from nameof() — no hardcoded magic strings.
/// </summary>
public static class IamTelemetry
{
    private const string Prefix = "ModularMonolith";

    /// <summary>
    ///     ActivitySource name: "ModularMonolith.IAM"
    /// </summary>
    public const string ActivitySourceName = Prefix + "." + nameof(IAM);

    /// <summary>
    ///     Meter name: "ModularMonolith.IAM"
    /// </summary>
    public const string MeterName = Prefix + "." + nameof(IAM);

    // ── Tracing ──────────────────────────────────────────────────────
    public static readonly ActivitySource ActivitySource = new(ActivitySourceName);

    // ── Metrics ──────────────────────────────────────────────────────
    public static readonly Meter Meter = new(MeterName);

    // ── Counters ─────────────────────────────────────────────────────
    public static readonly Counter<long> Logins =
        Meter.CreateCounter<long>("iam.logins.total", description: "Total successful logins (including auto-login after registration)");

    public static readonly Counter<long> UsersRegistered =
        Meter.CreateCounter<long>("iam.users_registered.total", description: "Total users registered");

    public static readonly Counter<long> SessionsCreated =
        Meter.CreateCounter<long>("iam.sessions_created.total", description: "Total sessions created");

    public static readonly Counter<long> SessionsRevoked =
        Meter.CreateCounter<long>("iam.sessions_revoked.total", description: "Total sessions revoked, tagged by reason");

    public static void RecordSessionRevoked(SessionRevokedReason reason, int count = 1) =>
        SessionsRevoked.Add(count, new KeyValuePair<string, object?>("session.revoked_reason", reason.ToString()));

    public static readonly Counter<long> TokenReuseDetected =
        Meter.CreateCounter<long>("iam.token_reuse_detected.total", description: "Total refresh token reuse (theft signal) detections");
}
