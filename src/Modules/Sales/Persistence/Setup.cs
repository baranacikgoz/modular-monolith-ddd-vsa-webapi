using Common.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.AspNetCore.Builder;
using Sales.Persistence.Seeding;
using MassTransit;

namespace Sales.Persistence;

internal static class Setup
{
    public static IServiceCollection AddPersistence(this IServiceCollection services)
        => services
            .AddTransient<Seeder>()
            .AddDbContext<SalesDbContext>((sp, options) =>
            {
                var connectionString = sp.GetRequiredService<IOptions<DatabaseOptions>>().Value.ConnectionString;
                options.UseNpgsql(
                    connectionString,
                    o => o.MigrationsHistoryTable(HistoryRepository.DefaultTableName, nameof(Sales)));
            });

    public static WebApplication UsePersistence(this WebApplication app)
    {
        using (var scope = app.Services.CreateScope())
        {
            var busControl = scope.ServiceProvider.GetRequiredService<IBusControl>();
            busControl.Start();

            var context = scope.ServiceProvider.GetRequiredService<SalesDbContext>();
            context.Database.Migrate();

            var seeder = scope.ServiceProvider.GetRequiredService<Seeder>();
            seeder.SeedDbAsync().GetAwaiter().GetResult();

            busControl.Stop();
        }

        return app;
    }
}
