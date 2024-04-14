using Common.Persistence.EventSourcing;
using Common.Persistence.TransactionalOutbox;
using MassTransit;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Common.Persistence;
public static class Setup
{
    public static IServiceCollection AddCommonPersistence(this IServiceCollection services)
        => services
            .AddOutboxDbContextAndInterceptor()
            .AddOutboxHostedService()
            .AddEventSourcingInterceptors()
            .AddScoped<ApplyAuditingInterceptor>()
            .AddSingleton<ClearAggregateEventsInterceptor>();

    public static WebApplication UseCommonPersistence(this WebApplication app)
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
}
