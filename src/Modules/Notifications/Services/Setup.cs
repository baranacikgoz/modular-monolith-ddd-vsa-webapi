using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace Notifications.Services;
internal static class Setup
{
    public static IServiceCollection AddNotificationServices(this IServiceCollection services)
        => services.AddSingleton<ISmsService, DummySmsService>();
}
