using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Notifications.Services;

namespace Notifications.ModuleSetup;
public static class ModuleInstaller
{
    public static IServiceCollection AddNotificationsModule(this IServiceCollection services)
        => services.AddNotificationServices();
}
