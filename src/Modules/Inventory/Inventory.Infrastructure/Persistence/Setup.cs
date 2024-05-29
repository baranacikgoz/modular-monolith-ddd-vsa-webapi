using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Inventory.Persistence.Seeding;
using MassTransit;
using Common.Infrastructure.Persistence.Context;
using Common.Infrastructure.Persistence.UoW;
using Microsoft.EntityFrameworkCore;
using Common.Infrastructure.Persistence.Repository;

namespace Inventory.Infrastructure.Persistence;

internal static class Setup
{
    public static IServiceCollection AddPersistence(this IServiceCollection services)
        => services
            .AddTransient<Seeder>()
            .AddModuleDbContext<InventoryDbContext>(moduleName: nameof(Inventory))
            .AddModuleUnitOfWork<InventoryDbContext>(moduleName: nameof(Inventory))
            .AddModuleRepositories<InventoryDbContext>(assemblyContainingEntities: typeof(Domain.IAssemblyReference).Assembly);

    public static WebApplication UsePersistence(this WebApplication app)
    {
        using (var scope = app.Services.CreateScope())
        {
            var busControl = scope.ServiceProvider.GetRequiredService<IBusControl>();
            busControl.Start();

            var context = scope.ServiceProvider.GetRequiredService<InventoryDbContext>();
            context.Database.Migrate();

            var seeder = scope.ServiceProvider.GetRequiredService<Seeder>();
            seeder.SeedDbAsync().GetAwaiter().GetResult();

            busControl.Stop();
        }

        return app;
    }
}
