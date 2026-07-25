using Common.Application.Options;
using Common.Domain.ResultMonad;
using Microsoft.Extensions.Logging;
using Notifications.Application.Sms;

namespace Notifications.Infrastructure.Sms;

/// <summary>
/// No-op gateway for non-production environments. Logs the message instead of sending it, so a
/// developer can read an OTP straight from the console. <see cref="SmsOptionsValidator"/> blocks
/// <c>Provider = Dummy</c> in Production.
/// </summary>
internal sealed partial class DummySmsGateway(ILogger<DummySmsGateway> logger) : ISmsGateway
{
    public Task<Result> SendAsync(SmsMessage message, CancellationToken cancellationToken)
    {
        LogSmsNotSent(logger, message.PhoneNumber, message.Text);
        return Task.FromResult(Result.Success);
    }

    [LoggerMessage(Level = LogLevel.Warning, Message = "SMS not sent (dummy gateway) to {PhoneNumber}: {Text}")]
    private static partial void LogSmsNotSent(ILogger logger, string phoneNumber, string text);
}
