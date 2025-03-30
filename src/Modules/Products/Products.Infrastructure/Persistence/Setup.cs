using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Products.Infrastructure.Persistence.Seeding;
using Products.Application.Persistence;
using Common.Infrastructure.Persistence.DbContext;

namespace Products.Infrastructure.Persistence;

internal static class Setup
{
    public static IServiceCollection AddPersistence(this IServiceCollection services)
        => services
            .AddTransient<Seeder>()
            .AddModuleDbContext<ProductsDbContext>(moduleName: nameof(Products));

    public static WebApplication UsePersistence(this WebApplication app)
    {
        using (var scope = app.Services.CreateScope())
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
