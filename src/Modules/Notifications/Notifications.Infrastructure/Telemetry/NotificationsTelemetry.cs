using System.Diagnostics;
using System.Diagnostics.Metrics;

namespace Notifications.Infrastructure.Telemetry;

internal static class NotificationsTelemetry
{
    private const string Prefix = "ModularMonolith";

    public const string ActivitySourceName = Prefix + "." + nameof(Notifications);
    public const string MeterName = Prefix + "." + nameof(Notifications);

    public static readonly ActivitySource ActivitySource = new(ActivitySourceName);
    public static readonly Meter Meter = new(MeterName);

    public static readonly UpDownCounter<long> ActiveConnections =
        Meter.CreateUpDownCounter<long>("notifications.signalr.connections.active", "connections",
            "Current number of active SignalR hub connections.");

    public static readonly Counter<long> NotificationsSent =
        Meter.CreateCounter<long>("notifications.signalr.sent", "notifications",
            "Total real-time notifications dispatched via SignalR.");
}
