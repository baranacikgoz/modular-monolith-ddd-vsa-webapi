namespace Notifications.Services;
public interface ISmsService
{
    Task SendWelcomeAsync(string name, string phoneNumber);
}
