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
            "Total real-time notifications dispatched via SignalR, tagged by notification type.");

    public static void RecordNotificationSent(string notificationType) =>
        NotificationsSent.Add(1, new KeyValuePair<string, object?>("notification.type", notificationType));

    public static readonly Counter<long> OtpSent =
        Meter.CreateCounter<long>("notifications.otp.sent", "otps",
            "Total OTP SMS sent via the gateway, tagged by purpose.");

    public static void RecordOtpSent(string purpose) =>
        OtpSent.Add(1, new KeyValuePair<string, object?>("otp.purpose", purpose));

    public static readonly Counter<long> OtpVerifications =
        Meter.CreateCounter<long>("notifications.otp.verifications", "verifications",
            "Total OTP verification attempts, tagged by purpose and outcome — sent vs verified is the OTP funnel; failure spikes flag delivery issues or brute force.");

    public static void RecordOtpVerification(string purpose, string outcome) =>
        OtpVerifications.Add(1,
            new KeyValuePair<string, object?>("otp.purpose", purpose),
            new KeyValuePair<string, object?>("otp.outcome", outcome));
}
