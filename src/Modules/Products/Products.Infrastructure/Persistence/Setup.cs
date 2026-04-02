using Common.Infrastructure.Persistence.DbContext;
using MassTransit;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Products.Application.Persistence;
using Products.Infrastructure.Persistence.Seeding;

namespace Products.Infrastructure.Persistence;

public static class Setup
{
    public static IServiceCollection AddPersistence(this IServiceCollection services)
    {
        return services
            .AddTransient<Seeder>()
            .AddModuleDbContext<IProductsDbContext, ProductsDbContext>(nameof(Products));
    }

    public static IApplicationBuilder UsePersistence(this IApplicationBuilder app)
    {
        using (var scope = app.ApplicationServices.CreateScope())
        {
            var busControl = scope.ServiceProvider.GetRequiredService<IBusControl>();
            busControl.Start();

            var context = scope.ServiceProvider.GetRequiredService<ProductsDbContext>();
            context.Database.Migrate();

            var seeder = scope.ServiceProvider.GetRequiredService<Seeder>();
            seeder.SeedDbAsync().GetAwaiter().GetResult();

            busControl.Stop();
        }

        return app;
    }
}
