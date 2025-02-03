using Common.Infrastructure.Options;
using Common.Infrastructure.Persistence.Outbox;
using MassTransit;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Outbox.Persistence;

namespace Outbox;
public static class ModuleInstaller
{
    public static IServiceCollection AddOutboxModule(this IServiceCollection services)
        => services
            .AddScoped<IOutboxDbContext, OutboxDbContext>()
            .AddOutboxDbContextAndInterceptor()
            .AddHostedService<OutboxBackgroundProcessor>();

    public static WebApplication UseOutboxModule(this WebApplication app)
    {
        using (var scope = app.Services.CreateScope())
        {
            var busControl = scope.ServiceProvider.GetRequiredService<IBusControl>();
            busControl.Start();

            var context = scope.ServiceProvider.GetRequiredService<OutboxDbContext>();
            context.Database.Migrate();

            busControl.Stop();
        }

        return app;
    }

    private static IServiceCollection AddOutboxDbContextAndInterceptor(this IServiceCollection services)
        => services
            .AddScoped<InsertOutboxMessagesInterceptor>()
            .AddDbContext<OutboxDbContext>((sp, options) =>
            {
                var connectionString = sp.GetRequiredService<IOptions<DatabaseOptions>>().Value.ConnectionString;
                options
                .UseNpgsql(
                    connectionString,
                    o => o.MigrationsHistoryTable(HistoryRepository.DefaultTableName, nameof(Outbox)));
            });
}
