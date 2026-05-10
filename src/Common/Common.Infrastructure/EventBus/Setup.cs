using System.Reflection;
using Common.Application.EventBus;
using Microsoft.Extensions.DependencyInjection;

namespace Common.Infrastructure.EventBus;

public static class Setup
{
    public static IServiceCollection AddCommonEventHandling(
        this IServiceCollection services,
        params Assembly[] assemblies)
    {
        services
            .AddScoped<IEventDispatcher, EventDispatcher>()
            .AddScoped<IIntegrationEventOutbox, IntegrationEventOutbox>();

        foreach (var assembly in assemblies)
        {
            services.Scan(scan => scan
                .FromAssemblies(assembly)
                .AddClasses(classes => classes.AssignableTo(typeof(IEventHandler<>)))
                .AsImplementedInterfaces()
                .WithScopedLifetime());
        }

        return services;
    }
}
