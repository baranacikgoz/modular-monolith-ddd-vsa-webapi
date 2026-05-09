using System.Reflection;
using Common.InterModuleRequests.Contracts;
using Microsoft.Extensions.DependencyInjection;

namespace Common.InterModuleRequests;

public static class Setup
{
    public static IServiceCollection AddCommonInterModuleRequests(
        this IServiceCollection services,
        params Assembly[] assemblies)
    {
        services.AddTransient(typeof(IInterModuleRequestClient<,>), typeof(DirectInterModuleRequestClient<,>));

        foreach (var assembly in assemblies)
        {
            services.Scan(scan => scan
                .FromAssemblies(assembly)
                .AddClasses(classes => classes.AssignableTo(typeof(IInterModuleRequestHandler<,>)))
                .AsImplementedInterfaces()
                .WithScopedLifetime());
        }

        return services;
    }
}
