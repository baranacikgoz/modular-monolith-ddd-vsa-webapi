using System.Diagnostics;
using System.Diagnostics.Metrics;

namespace BackgroundJobs.Telemetry;

internal static class BackgroundJobsTelemetry
{
    private const string Prefix = "ModularMonolith";

    public const string ActivitySourceName = Prefix + "." + nameof(BackgroundJobs);
    public const string MeterName = Prefix + "." + nameof(BackgroundJobs);

    public static readonly ActivitySource ActivitySource = new(ActivitySourceName);
    public static readonly Meter Meter = new(MeterName);
}
