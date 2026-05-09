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
            .AddScoped<IDomainEventDispatcher, DomainEventDispatcher>()
            .AddScoped<IIntegrationEventDispatcher, IntegrationEventDispatcher>()
            .AddScoped<IIntegrationEventOutbox, IntegrationEventOutbox>();

        foreach (var assembly in assemblies)
        {
            services.Scan(scan => scan
                .FromAssemblies(assembly)
                .AddClasses(classes => classes.AssignableTo(typeof(IDomainEventHandler<>)))
                .AsImplementedInterfaces()
                .WithScopedLifetime()
                .AddClasses(classes => classes.AssignableTo(typeof(IIntegrationEventHandler<>)))
                .AsImplementedInterfaces()
                .WithScopedLifetime());
        }

        return services;
    }
}
