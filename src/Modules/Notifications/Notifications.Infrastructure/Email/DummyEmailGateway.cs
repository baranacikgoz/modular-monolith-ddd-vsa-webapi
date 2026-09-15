using Common.Application.Options;
using Common.Domain.ResultMonad;
using Microsoft.Extensions.Logging;
using Notifications.Application.Email;

namespace Notifications.Infrastructure.Email;

/// <summary>
/// No-op gateway for non-production environments. Logs the message instead of sending it, so a
/// developer can read an OTP straight from the console. <see cref="EmailOptionsValidator"/> blocks
/// <c>Provider = Dummy</c> in Production.
/// </summary>
internal sealed partial class DummyEmailGateway(ILogger<DummyEmailGateway> logger) : IEmailGateway
{
    public Task<Result> SendAsync(EmailMessage message, CancellationToken cancellationToken)
    {
        LogEmailNotSent(logger, message.To, message.Subject);
        return Task.FromResult(Result.Success);
    }

    [LoggerMessage(Level = LogLevel.Warning, Message = "Email not sent (dummy gateway) to {To}: {Subject}")]
    private static partial void LogEmailNotSent(ILogger logger, string to, string subject);
}
