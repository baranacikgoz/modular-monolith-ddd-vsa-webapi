using System.Diagnostics;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
using Common.Application.Options;
using Common.Domain.ResultMonad;
using Common.Infrastructure.Extensions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Notifications.Application.Sms;
using Notifications.Infrastructure.Telemetry;

namespace Notifications.Infrastructure.Sms.NetGsm;

/// <summary>
/// NetGSM REST v2 (<c>/sms/rest/v2/send</c>) implementation of <see cref="ISmsGateway"/>. Treats the
/// provider as untrusted: never throws, never propagates its response text to callers, never retries a
/// send (retrying a timed-out send risks a duplicate SMS). Resilience (timeout, response size cap) is
/// supplied by the typed HttpClient pipeline configured in <see cref="Setup"/>.
/// </summary>
internal sealed partial class NetGsmSmsGateway(
    HttpClient httpClient,
    IOptions<SmsOptions> optionsProvider,
    ILogger<NetGsmSmsGateway> logger
) : ISmsGateway
{
    private const string SendPath = "sms/rest/v2/send";

    private static readonly JsonSerializerOptions SerializerOptions = new(JsonSerializerDefaults.Web)
    {
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
    };

    // NetGSM /sms/rest/v2/send error codes — https://www.netgsm.com.tr/dokuman/#api-dokumani
    private const string CodeSuccess = "00";
    private const string CodeBadMessage = "20";
    private const string CodeBadCredentialsOrIp = "30";
    private const string CodeUnknownHeader = "40";
    private const string CodeIysNotAllowed = "50";
    private const string CodeIysBrandMissing = "51";
    private const string CodeInvalidQuery = "70";
    private const string CodeSendLimitExceeded = "80";
    private const string CodeDuplicateLimitExceeded = "85";
    private const string CodeSystemError = "100";
    private const string CodeSystemError2 = "101";

    public async Task<Result> SendAsync(SmsMessage message, CancellationToken cancellationToken)
    {
        using var activity = NotificationsTelemetry.ActivitySource.StartActivityForCaller();

        var options = optionsProvider.Value;
        var body = new SendRequestBody
        {
            MsgHeader = options.MsgHeader ?? string.Empty,
            Messages = [new SendMessageBody { Msg = message.Text, No = NormalizePhoneNumber(message.PhoneNumber) }],
            Encoding = RequiresTurkishEncoding(message.Text) ? "tr" : null,
            IysFilter = ToIysFilter(message.Category),
            AppName = options.AppName,
        };

        var result = await SendCoreAsync(body, cancellationToken).TapActivityAsync(activity);
        NotificationsTelemetry.RecordSmsSent(message.Category.ToString(), result.IsFailure ? result.Error!.Key : "Sent");
        return result;
    }

    private async Task<Result> SendCoreAsync(SendRequestBody body, CancellationToken cancellationToken)
    {
        HttpResponseMessage httpResponse;
        try
        {
            httpResponse = await httpClient.PostAsJsonAsync(
                new Uri(SendPath, UriKind.Relative), body, SerializerOptions, cancellationToken);
        }
        catch (HttpRequestException ex)
        {
            LogRequestFailed(logger, MaskPhoneNumber(body.Messages[0].No), ex);
            return SmsErrors.ProviderUnavailable;
        }
        catch (TaskCanceledException ex) when (!cancellationToken.IsCancellationRequested)
        {
            LogRequestFailed(logger, MaskPhoneNumber(body.Messages[0].No), ex);
            return SmsErrors.ProviderUnavailable;
        }

        using (httpResponse)
        {
            SendResponseBody? parsed;
            try
            {
                parsed = await httpResponse.Content.ReadFromJsonAsync<SendResponseBody>(SerializerOptions, cancellationToken);
            }
            catch (JsonException ex)
            {
                LogDeserializationFailed(logger, (int)httpResponse.StatusCode, ex);
                return SmsErrors.ProviderUnavailable;
            }
            catch (NotSupportedException ex)
            {
                LogDeserializationFailed(logger, (int)httpResponse.StatusCode, ex);
                return SmsErrors.ProviderUnavailable;
            }
            catch (HttpRequestException ex)
            {
                // Thrown by the buffered reader when the body exceeds MaxResponseContentBufferSize.
                LogDeserializationFailed(logger, (int)httpResponse.StatusCode, ex);
                return SmsErrors.ProviderUnavailable;
            }

            if (parsed?.Code is null)
            {
                LogDeserializationFailed(logger, (int)httpResponse.StatusCode, null);
                return SmsErrors.ProviderUnavailable;
            }

            if (parsed.Code == CodeSuccess)
            {
                Activity.Current?.SetTag("netgsm.jobid", parsed.JobId);
            }

            return MapCode(parsed.Code, parsed.Description);
        }
    }

    private Result MapCode(string code, string? description)
    {
        if (code == CodeSuccess)
        {
            return Result.Success;
        }

        NotificationsTelemetry.RecordSmsProviderError(code);

        switch (code)
        {
            case CodeBadMessage:
            case CodeInvalidQuery:
                LogRejected(logger, code, description);
                return SmsErrors.Rejected;
            case CodeBadCredentialsOrIp:
            case CodeUnknownHeader:
            case CodeIysNotAllowed:
            case CodeIysBrandMissing:
                LogRejectedPageWorthy(logger, code, description);
                return SmsErrors.Rejected;
            case CodeSendLimitExceeded:
            case CodeDuplicateLimitExceeded:
                return SmsErrors.Throttled;
            case CodeSystemError:
            case CodeSystemError2:
                LogProviderSystemError(logger, code, description);
                return SmsErrors.ProviderUnavailable;
            default:
                LogUnknownCode(logger, code, description);
                return SmsErrors.ProviderUnavailable;
        }
    }

    private static string NormalizePhoneNumber(string phoneNumber) =>
        phoneNumber.StartsWith("90", StringComparison.Ordinal) && phoneNumber.Length == 12
            ? phoneNumber[2..]
            : phoneNumber;

    private static bool RequiresTurkishEncoding(string text) => text.Any(c => c > 127);

    private static string ToIysFilter(SmsCategory category) => category switch
    {
        SmsCategory.Transactional => "0",
        SmsCategory.CommercialIndividual => "11",
        SmsCategory.CommercialMerchant => "12",
        _ => throw new ArgumentOutOfRangeException(nameof(category), category, "Unknown SmsCategory.")
    };

    private static string MaskPhoneNumber(string phoneNumber) =>
        phoneNumber.Length > 4 ? $"***{phoneNumber[^4..]}" : "***";

    [LoggerMessage(Level = LogLevel.Error, Message = "NetGSM send request to {PhoneNumber} failed")]
    private static partial void LogRequestFailed(ILogger logger, string phoneNumber, Exception ex);

    [LoggerMessage(Level = LogLevel.Error, Message = "NetGSM response could not be parsed (HTTP {StatusCode})")]
    private static partial void LogDeserializationFailed(ILogger logger, int statusCode, Exception? ex);

    [LoggerMessage(Level = LogLevel.Warning, Message = "NetGSM rejected the request: code={Code} description={Description}")]
    private static partial void LogRejected(ILogger logger, string code, string? description);

    [LoggerMessage(Level = LogLevel.Error, Message = "NetGSM rejected the request (account/config problem): code={Code} description={Description}")]
    private static partial void LogRejectedPageWorthy(ILogger logger, string code, string? description);

    [LoggerMessage(Level = LogLevel.Error, Message = "NetGSM reported a system error: code={Code} description={Description}")]
    private static partial void LogProviderSystemError(ILogger logger, string code, string? description);

    [LoggerMessage(Level = LogLevel.Error, Message = "NetGSM returned an unrecognized code: code={Code} description={Description}")]
    private static partial void LogUnknownCode(ILogger logger, string code, string? description);

    private sealed class SendRequestBody
    {
        [JsonPropertyName("msgheader")] public required string MsgHeader { get; init; }
        [JsonPropertyName("messages")] public required IReadOnlyList<SendMessageBody> Messages { get; init; }
        [JsonPropertyName("encoding")] public string? Encoding { get; init; }
        [JsonPropertyName("iysfilter")] public required string IysFilter { get; init; }
        [JsonPropertyName("appname")] public string? AppName { get; init; }
    }

    private sealed class SendMessageBody
    {
        [JsonPropertyName("msg")] public required string Msg { get; init; }
        [JsonPropertyName("no")] public required string No { get; init; }
    }

    private sealed class SendResponseBody
    {
        [JsonPropertyName("code")] public string? Code { get; init; }
        [JsonPropertyName("jobid")] public string? JobId { get; init; }
        [JsonPropertyName("description")] public string? Description { get; init; }
    }
}
