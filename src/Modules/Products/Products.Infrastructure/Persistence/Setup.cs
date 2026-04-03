using Common.Application.Persistence;
using Common.Infrastructure.Persistence;
using Common.Infrastructure.Persistence.DbContext;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Products.Application.Persistence;
using Products.Infrastructure.Persistence.Seeding;

namespace Products.Infrastructure.Persistence;

public static class Setup
{
    public static IServiceCollection AddPersistence(this IServiceCollection services)
    {
        return services
            .AddTransient<Seeder>()
            .AddTransient<IDatabaseSeeder, ProductsDatabaseSeeder>()
            .AddModuleDbContext<IProductsDbContext, ProductsDbContext>(nameof(Products));
    }

    public static IApplicationBuilder UsePersistence(this IApplicationBuilder app)
    {
        var logger = app.ApplicationServices
            .GetRequiredService<ILoggerFactory>()
            .CreateLogger(typeof(Setup).FullName!);

        MigrationGuard.EnsureNoMigrationsPending<ProductsDbContext>(
            app.ApplicationServices, logger, nameof(Products));

        return app;
    }
}
