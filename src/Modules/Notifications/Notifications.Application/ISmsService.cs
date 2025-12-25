namespace Notifications.Application;

public interface ISmsService
{
    Task SendWelcomeAsync(string name, string phoneNumber);
}
