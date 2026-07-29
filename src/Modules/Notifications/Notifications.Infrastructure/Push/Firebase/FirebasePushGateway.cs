using System.Diagnostics;
using System.Text.Json;
using Common.Application.Options;
using Common.Domain.ResultMonad;
using Common.Infrastructure.Extensions;
using FirebaseAdmin;
using FirebaseAdmin.Messaging;
using Google.Apis.Auth.OAuth2;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Notifications.Application.Push;
using Notifications.Infrastructure.Telemetry;

namespace Notifications.Infrastructure.Push.Firebase;

/// <summary>
/// FCM implementation of <see cref="IPushGateway"/> via the official FirebaseAdmin SDK. Treats the
/// provider as untrusted: never throws, never propagates its response text to callers. Owns a
/// single named <see cref="FirebaseApp"/> instance — <b>never</b> <see cref="FirebaseApp.DefaultInstance"/>,
/// which is process-wide global state that has already corrupted parallel test factories elsewhere
/// in this repo (see the OTel ActivitySource/TracerProvider rule in CLAUDE.md).
/// </summary>
internal sealed partial class FirebasePushGateway : IPushGateway, IDisposable
{
    private const int MaxTokensPerMulticast = 500;

    private readonly FirebaseApp _app;
    private readonly FirebaseMessaging _messaging;
    private readonly TimeSpan _sendTimeout;
    private readonly ILogger<FirebasePushGateway> _logger;

    public FirebasePushGateway(IOptions<PushOptions> optionsProvider, ILogger<FirebasePushGateway> logger)
    {
        var options = optionsProvider.Value;
        var serviceAccount = options.ServiceAccount!;
        var credentialJson = JsonSerializer.Serialize(new
        {
            type = serviceAccount.Type,
            project_id = serviceAccount.ProjectId,
            private_key_id = serviceAccount.PrivateKeyId,
            private_key = serviceAccount.PrivateKey,
            client_email = serviceAccount.ClientEmail,
            client_id = serviceAccount.ClientId,
            token_uri = serviceAccount.TokenUri,
        });

        // The single-argument GoogleCredential.FromJson overload is obsolete (security risk in its
        // JSON-type dispatch). CredentialFactory resolves the concrete credential type instead, which
        // then converts back to the type FirebaseApp.Create expects.
        var serviceAccountCredential = CredentialFactory.FromJson<ServiceAccountCredential>(credentialJson);
        _app = FirebaseApp.Create(
            new AppOptions { Credential = serviceAccountCredential.ToGoogleCredential() },
            nameof(FirebasePushGateway));
        _messaging = FirebaseMessaging.GetMessaging(_app);
        _sendTimeout = TimeSpan.FromSeconds(options.SendTimeoutSeconds);
        _logger = logger;
    }

    public async Task<Result> SendAsync(PushMessage message, CancellationToken cancellationToken)
    {
        using var activity = NotificationsTelemetry.ActivitySource.StartActivityForCaller();
        return await SendCoreAsync(message, cancellationToken).TapActivityAsync(activity);
    }

    private async Task<Result> SendCoreAsync(PushMessage message, CancellationToken cancellationToken)
    {
        if (message.Tokens.Count == 0)
        {
            return Result.Success;
        }

        var anySucceeded = false;

        foreach (var chunk in Chunk(message.Tokens, MaxTokensPerMulticast))
        {
            // MulticastMessage.Tokens is marked [Obsolete] in favor of Fids (Firebase Installation
            // IDs) — a different client-side registration mechanism entirely. Our clients register
            // FCM registration tokens (firebase_messaging's getToken()), not installation IDs, so
            // Tokens is the correct member here despite the deprecation warning.
#pragma warning disable CS0618
            var multicastMessage = new MulticastMessage
            {
                Tokens = chunk,
                Notification = new Notification { Title = message.Title, Body = message.Body },
                Data = message.Data,
            };
#pragma warning restore CS0618

            using var timeoutCts = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);
            timeoutCts.CancelAfter(_sendTimeout);

            BatchResponse batchResponse;
            try
            {
                batchResponse = await _messaging.SendEachForMulticastAsync(multicastMessage, timeoutCts.Token);
            }
            catch (FirebaseMessagingException ex)
            {
                LogRequestFailed(_logger, chunk.Count, ex);
                NotificationsTelemetry.RecordPushSent("ProviderUnavailable");
                continue;
            }
            catch (OperationCanceledException) when (!cancellationToken.IsCancellationRequested)
            {
                LogRequestTimedOut(_logger, chunk.Count);
                NotificationsTelemetry.RecordPushSent("ProviderUnavailable");
                continue;
            }

            foreach (var response in batchResponse.Responses)
            {
                if (response.IsSuccess)
                {
                    anySucceeded = true;
                    NotificationsTelemetry.RecordPushSent("Sent");
                    continue;
                }

                // ponytail: dead-token pruning deferred. Unregistered/InvalidArgument responses are
                // only counted here, not cleared from Session.PushToken — a reinstall re-registers
                // on next login, so stale rows self-heal. Add a ClearPushTokens InterModuleRequest
                // if the rejected-token rate ever climbs enough to matter.
                var code = response.Exception?.MessagingErrorCode?.ToString() ?? "Unknown";
                NotificationsTelemetry.RecordPushSent("Rejected");
                NotificationsTelemetry.RecordPushTokenRejected(code);
            }
        }

        Activity.Current?.SetTag("push.token_count", message.Tokens.Count);

        return anySucceeded ? Result.Success : PushErrors.ProviderUnavailable;
    }

    public void Dispose() => _app.Delete();

    private static IEnumerable<IReadOnlyList<string>> Chunk(IReadOnlyList<string> tokens, int size)
    {
        for (var offset = 0; offset < tokens.Count; offset += size)
        {
            yield return tokens.Skip(offset).Take(size).ToList();
        }
    }

    [LoggerMessage(Level = LogLevel.Error, Message = "FCM multicast send to {TokenCount} token(s) failed")]
    private static partial void LogRequestFailed(ILogger logger, int tokenCount, Exception ex);

    [LoggerMessage(Level = LogLevel.Error, Message = "FCM multicast send to {TokenCount} token(s) timed out")]
    private static partial void LogRequestTimedOut(ILogger logger, int tokenCount);
}
