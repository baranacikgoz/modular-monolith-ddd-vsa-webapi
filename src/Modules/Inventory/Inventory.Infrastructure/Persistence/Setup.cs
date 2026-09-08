using Common.Infrastructure.Persistence;
using Common.Infrastructure.Persistence.DbContext;
using Inventory.Application.Persistence;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Inventory.Infrastructure.Persistence;

public static class Setup
{
    public static IServiceCollection AddPersistence(this IServiceCollection services)
    {
        return services
            .AddModuleDbContext<IInventoryDbContext, InventoryDbContext>(nameof(Inventory));
    }

    public static IApplicationBuilder UsePersistence(this IApplicationBuilder app)
    {
        var logger = app.ApplicationServices
            .GetRequiredService<ILoggerFactory>()
            .CreateLogger(typeof(Setup).FullName!);

        MigrationGuard.EnsureNoMigrationsPending<InventoryDbContext>(
            app.ApplicationServices, logger, nameof(Inventory));

        return app;
    }
}
