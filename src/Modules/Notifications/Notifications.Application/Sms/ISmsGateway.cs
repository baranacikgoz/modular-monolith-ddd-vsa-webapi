namespace Notifications.Application.Sms;

public interface ISmsGateway
{
    Task SendAsync(string phoneNumber, string message, CancellationToken cancellationToken);
}
