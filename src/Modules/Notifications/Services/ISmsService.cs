using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notifications.Services;
public interface ISmsService
{
    Task SendWelcomeAsync(string name, string phoneNumber);
}
