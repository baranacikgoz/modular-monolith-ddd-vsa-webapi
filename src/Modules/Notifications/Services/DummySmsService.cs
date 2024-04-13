using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notifications.Services;
public class DummySmsService : ISmsService
{
    public Task SendWelcomeAsync(string name, string phoneNumber)
    {
        return Task.CompletedTask;
    }
}
