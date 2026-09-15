using System.Diagnostics;
using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
using Common.Application.Options;
using Common.Domain.ResultMonad;
using Common.Infrastructure.Extensions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Notifications.Application.Email;
using Notifications.Infrastructure.Telemetry;

namespace Notifications.Infrastructure.Email.Brevo;

/// <summary>
/// Brevo transactional email API (<c>POST /v3/smtp/email</c>) implementation of <see cref="IEmailGateway"/>.
/// Treats the provider as untrusted: never throws, never propagates its response text to callers, never
/// retries a send (retrying a timed-out send risks a duplicate email). Resilience (timeout, response size
/// cap) is supplied by the typed HttpClient pipeline configured in <see cref="Setup"/>.
/// </summary>
internal sealed partial class BrevoEmailGateway(
    HttpClient httpClient,
    IOptions<EmailOptions> optionsProvider,
    ILogger<BrevoEmailGateway> logger
) : IEmailGateway
{
    private const string SendPath = "v3/smtp/email";

    private static readonly JsonSerializerOptions SerializerOptions = new(JsonSerializerDefaults.Web)
    {
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
    };

    public async Task<Result> SendAsync(EmailMessage message, CancellationToken cancellationToken)
    {
        using var activity = NotificationsTelemetry.ActivitySource.StartActivityForCaller();

        var options = optionsProvider.Value;
        var body = new SendRequestBody
        {
            Sender = new SendContact { Email = options.SenderEmail!, Name = options.SenderName },
            To = [new SendContact { Email = message.To }],
            Subject = message.Subject,
            HtmlContent = message.HtmlBody,
            TextContent = message.TextBody,
        };

        var result = await SendCoreAsync(body, cancellationToken).TapActivityAsync(activity);
        NotificationsTelemetry.RecordEmailSent(result.IsFailure ? result.Error!.Key : "Sent");
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
            LogRequestFailed(logger, MaskEmail(body.To[0].Email), ex);
            return EmailErrors.ProviderUnavailable;
        }
        catch (TaskCanceledException ex) when (!cancellationToken.IsCancellationRequested)
        {
            LogRequestFailed(logger, MaskEmail(body.To[0].Email), ex);
            return EmailErrors.ProviderUnavailable;
        }

        using (httpResponse)
        {
            if (httpResponse.StatusCode is HttpStatusCode.Created or HttpStatusCode.OK)
            {
                var success = await TryReadMessageIdAsync(httpResponse, cancellationToken);
                Activity.Current?.SetTag("brevo.message_id", success);
                return Result.Success;
            }

            SendErrorBody? error;
            try
            {
                error = await httpResponse.Content.ReadFromJsonAsync<SendErrorBody>(SerializerOptions, cancellationToken);
            }
            catch (JsonException ex)
            {
                LogDeserializationFailed(logger, (int)httpResponse.StatusCode, ex);
                return EmailErrors.ProviderUnavailable;
            }
            catch (NotSupportedException ex)
            {
                LogDeserializationFailed(logger, (int)httpResponse.StatusCode, ex);
                return EmailErrors.ProviderUnavailable;
            }
            catch (HttpRequestException ex)
            {
                // Thrown by the buffered reader when the body exceeds MaxResponseContentBufferSize.
                LogDeserializationFailed(logger, (int)httpResponse.StatusCode, ex);
                return EmailErrors.ProviderUnavailable;
            }

            return MapStatusCode(httpResponse.StatusCode, error?.Code, error?.Message);
        }
    }

    private static async Task<string?> TryReadMessageIdAsync(HttpResponseMessage httpResponse, CancellationToken cancellationToken)
    {
        try
        {
            var parsed = await httpResponse.Content.ReadFromJsonAsync<SendResponseBody>(SerializerOptions, cancellationToken);
            return parsed?.MessageId;
        }
        catch (JsonException)
        {
            return null;
        }
    }

    private Result MapStatusCode(HttpStatusCode statusCode, string? code, string? message)
    {
        NotificationsTelemetry.RecordEmailSent($"HttpError_{(int)statusCode}");

        switch (statusCode)
        {
            case HttpStatusCode.BadRequest:
            case HttpStatusCode.UnprocessableEntity:
                LogRejected(logger, code, message);
                return EmailErrors.Rejected;
            case HttpStatusCode.Unauthorized:
            case HttpStatusCode.Forbidden:
                LogRejectedPageWorthy(logger, code, message);
                return EmailErrors.Rejected;
            case HttpStatusCode.TooManyRequests:
                return EmailErrors.Throttled;
            default:
                LogProviderSystemError(logger, (int)statusCode, code, message);
                return EmailErrors.ProviderUnavailable;
        }
    }

    private static string MaskEmail(string email)
    {
        var at = email.IndexOf('@', StringComparison.Ordinal);
        return at > 1 ? $"{email[0]}***{email[at..]}" : "***";
    }

    [LoggerMessage(Level = LogLevel.Error, Message = "Brevo send request to {Email} failed")]
    private static partial void LogRequestFailed(ILogger logger, string email, Exception ex);

    [LoggerMessage(Level = LogLevel.Error, Message = "Brevo response could not be parsed (HTTP {StatusCode})")]
    private static partial void LogDeserializationFailed(ILogger logger, int statusCode, Exception? ex);

    [LoggerMessage(Level = LogLevel.Warning, Message = "Brevo rejected the request: code={Code} message={Message}")]
    private static partial void LogRejected(ILogger logger, string? code, string? message);

    [LoggerMessage(Level = LogLevel.Error, Message = "Brevo rejected the request (account/config problem): code={Code} message={Message}")]
    private static partial void LogRejectedPageWorthy(ILogger logger, string? code, string? message);

    [LoggerMessage(Level = LogLevel.Error, Message = "Brevo reported a system error: status={StatusCode} code={Code} message={Message}")]
    private static partial void LogProviderSystemError(ILogger logger, int statusCode, string? code, string? message);

    private sealed class SendContact
    {
        [JsonPropertyName("email")] public required string Email { get; init; }
        [JsonPropertyName("name")] public string? Name { get; init; }
    }

    private sealed class SendRequestBody
    {
        [JsonPropertyName("sender")] public required SendContact Sender { get; init; }
        [JsonPropertyName("to")] public required IReadOnlyList<SendContact> To { get; init; }
        [JsonPropertyName("subject")] public required string Subject { get; init; }
        [JsonPropertyName("htmlContent")] public required string HtmlContent { get; init; }
        [JsonPropertyName("textContent")] public string? TextContent { get; init; }
    }

    private sealed class SendResponseBody
    {
        [JsonPropertyName("messageId")] public string? MessageId { get; init; }
    }

    private sealed class SendErrorBody
    {
        [JsonPropertyName("code")] public string? Code { get; init; }
        [JsonPropertyName("message")] public string? Message { get; init; }
    }
}
