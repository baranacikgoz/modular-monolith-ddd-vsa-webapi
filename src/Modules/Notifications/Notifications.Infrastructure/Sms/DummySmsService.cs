using Notifications.Application;

namespace Notifications.Infrastructure.Sms;

public class DummySmsService : ISmsService
{
    public Task SendWelcomeAsync(string name, string phoneNumber)
    {
        return Task.CompletedTask;
    }
}
