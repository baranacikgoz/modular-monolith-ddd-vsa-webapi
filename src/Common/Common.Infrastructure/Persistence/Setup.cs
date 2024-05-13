using Common.Infrastructure.Persistence.Context.Interceptors;
using Common.Infrastructure.Persistence.EventSourcing;
using Common.Infrastructure.Persistence.Outbox;
using MassTransit;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Common.Infrastructure.Persistence;
public static class Setup
{
    public static IServiceCollection AddCommonPersistence(this IServiceCollection services)
        => services
            .AddOutboxDbContextAndInterceptor()
            .AddOutboxHostedService()
            .AddEventSourcingInterceptors()
            .AddScoped<ApplyAuditingInterceptor>();

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
