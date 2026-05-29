using Notifications.Application.Sms;

namespace Notifications.Infrastructure.Sms;

internal sealed class DummySmsGateway : ISmsGateway
{
    public Task SendAsync(string phoneNumber, string message, CancellationToken cancellationToken)
        => Task.CompletedTask;
}
