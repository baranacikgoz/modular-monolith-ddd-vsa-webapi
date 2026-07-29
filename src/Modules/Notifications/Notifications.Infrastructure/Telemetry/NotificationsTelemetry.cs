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

    public static readonly Counter<long> SmsSent =
        Meter.CreateCounter<long>("notifications.sms.sent", "sms",
            "Total SMS send attempts via ISmsGateway, tagged by category and outcome.");

    public static void RecordSmsSent(string category, string outcome) =>
        SmsSent.Add(1,
            new KeyValuePair<string, object?>("sms.category", category),
            new KeyValuePair<string, object?>("sms.outcome", outcome));

    public static readonly Counter<long> SmsProviderErrors =
        Meter.CreateCounter<long>("notifications.sms.provider_errors", "errors",
            "Total NetGSM error codes returned on send, tagged by the raw provider code.");

    public static void RecordSmsProviderError(string netGsmCode) =>
        SmsProviderErrors.Add(1, new KeyValuePair<string, object?>("netgsm.code", netGsmCode));

    public static readonly Counter<long> SmsThrottled =
        Meter.CreateCounter<long>("notifications.sms.throttled", "sms",
            "Total SMS sends rejected by our own spend guard before reaching the provider, tagged by which cap was hit.");

    public static void RecordSmsThrottled(string reason) =>
        SmsThrottled.Add(1, new KeyValuePair<string, object?>("throttle.reason", reason));

    public static readonly Counter<long> PushSent =
        Meter.CreateCounter<long>("notifications.push.sent", "push",
            "Total push send attempts per device token via IPushGateway, tagged by outcome.");

    public static void RecordPushSent(string outcome) =>
        PushSent.Add(1, new KeyValuePair<string, object?>("push.outcome", outcome));

    public static readonly Counter<long> PushTokensRejected =
        Meter.CreateCounter<long>("notifications.push.tokens_rejected", "tokens",
            "Total device tokens FCM rejected on send, tagged by the raw MessagingErrorCode.");

    public static void RecordPushTokenRejected(string fcmCode) =>
        PushTokensRejected.Add(1, new KeyValuePair<string, object?>("fcm.code", fcmCode));
}
