using Common.Application.Options;
using Common.Domain.ResultMonad;
using Microsoft.Extensions.Logging;
using Notifications.Application.Push;

namespace Notifications.Infrastructure.Push;

/// <summary>
/// No-op gateway for non-production environments. Logs the message instead of sending it.
/// <see cref="PushOptionsValidator"/> blocks <c>Provider = Dummy</c> in Production.
/// </summary>
internal sealed partial class DummyPushGateway(ILogger<DummyPushGateway> logger) : IPushGateway
{
    public Task<Result> SendAsync(PushMessage message, CancellationToken cancellationToken)
    {
        LogPushNotSent(logger, message.Tokens.Count, message.Title);
        return Task.FromResult(Result.Success);
    }

    [LoggerMessage(Level = LogLevel.Warning, Message = "Push not sent (dummy gateway) to {TokenCount} token(s): {Title}")]
    private static partial void LogPushNotSent(ILogger logger, int tokenCount, string title);
}
