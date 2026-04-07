using System.Diagnostics;
using System.Diagnostics.Metrics;

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
    public static readonly Counter<long> TokensIssued =
        Meter.CreateCounter<long>("iam.tokens_issued.total", description: "Total tokens issued");

    public static readonly Counter<long> UsersRegistered =
        Meter.CreateCounter<long>("iam.users_registered.total", description: "Total users registered");
}
