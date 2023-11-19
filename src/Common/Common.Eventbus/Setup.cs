using System.Reflection;
using Common.Options;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using NimbleMediator.NotificationPublishers;
using NimbleMediator.ServiceExtensions;

namespace Common.Eventbus;

public static class Setup
{
    public static IServiceCollection AddEventBus(
        this IServiceCollection services,
        params Assembly[] assembliesToRegisterConsumersFrom)
    {
        services.AddNimbleMediator(cfg =>
        {
            cfg.RegisterServicesFromAssemblies(assembliesToRegisterConsumersFrom);
            cfg.SetDefaultNotificationPublisher<TaskWhenAllPublisher>();
        });

        services.AddScoped<IEventBus, NimbleMediatorEventBus>();

        return services;
    }

}
