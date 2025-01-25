using System.Reflection;
using Ardalis.Specification;
using Common.Application.Persistence;
using Common.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Common.Infrastructure.Persistence.Repository;
public static class Setup
{
    public static IServiceCollection AddModuleRepositories<TModuleContext>(this IServiceCollection services, Assembly assemblyContainingEntities)
        where TModuleContext : DbContext
    {
        // Get all entity types that implements IAuditableEntity somehow.
        var entityTypes = assemblyContainingEntities.GetTypes().Where(t =>
            typeof(IAuditableEntity).IsAssignableFrom(t) &&
            !t.IsAbstract &&
            !t.IsInterface);

        foreach (var entityType in entityTypes)
        {
            // Register a generic repository for each entity type
            var repositoryType = typeof(IRepository<>).MakeGenericType(entityType);
            var repositoryImplementationType = typeof(BaseRepository<>).MakeGenericType(entityType);

            services.AddScoped(repositoryType, serviceProvider =>
            {
                var context = serviceProvider.GetRequiredService<TModuleContext>();
                return Activator.CreateInstance(repositoryImplementationType, context) ?? throw new InvalidOperationException("Could not create repository.");
            });
        }

        return services;
    }
}
