using System.Reflection;
using Common.Eventbus.Extensions;
using Common.Eventbus.Persistence;
using Common.Options;
using MassTransit;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Common.Eventbus;

public static class Setup
{
    public static IServiceCollection AddEventBus(
        this IServiceCollection services,
        IConfiguration configuration,
        params Assembly[] assembliesToRegisterConsumers)
    {
        services.AddSingleton<IEventBus, MassTransitEventBus>();

        services.AddDbContext<OutboxDbContext>((sp, o) =>
        {
            var connectionString = sp.GetRequiredService<IOptions<DatabaseOptions>>().Value.ConnectionString;

            o.UseNpgsql(connectionString,
                o =>
                {
                    o.MigrationsHistoryTable(HistoryRepository.DefaultTableName, ModuleConstants.OutboxSchemaName);
                });
        });

        services.AddMassTransit(conf =>
        {
            // If you are planning to scale out the application,
            // this DbContext should be configured to connect to the same database for all application instances.
            // Read the comments in "UsingInMemory" method below as well.
            conf.AddEntityFrameworkOutbox<OutboxDbContext>(o =>
            {
                o.UsePostgres();
                o.UseBusOutbox();
            });

            // Allow each module to configure their own consumers in a decoupled way.
            foreach (var assembly in assembliesToRegisterConsumers)
            {
                conf.AddConsumersFromAssembly(assembly);
            }

            // If you are planning to scale out the application,
            // you should use a distributed message broker like RabbitMQ.
            // Read the comments in "AddEntityFrameworkOutbox" method above as well.
            conf.UsingInMemory((context, cfg) =>
            {
                cfg.ConfigureEndpoints(context);
            });
        });

        return services;
    }

    public static WebApplication UseEventBus(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<OutboxDbContext>();
        dbContext.Database.Migrate();

        return app;
    }
}
