namespace Notifications.Services;
public class DummySmsService : ISmsService
{
    public Task SendWelcomeAsync(string name, string phoneNumber)
    {
        return Task.CompletedTask;
    }
}
