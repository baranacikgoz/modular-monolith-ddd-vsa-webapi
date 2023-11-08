using System.Reflection;
using Common.Options;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Common.Eventbus;

public static class Setup
{
    public static IServiceCollection AddEventBus(
        this IServiceCollection services,
        params Assembly[] assembliesToRegisterConsumersFrom)
    {
        // IEventConsumers(EventHandlers in this case) is already registered with AddNimbleMediator() call.
        // I've just put the ``assembliesToRegisterConsumersFrom`` as param because if you'd like to use
        // something different like MassTransit, you would need to register consumers here.

        services.AddScoped<IEventBus, NimbleMediatorEventBus>();

        return services;
    }

}
