using Microsoft.Extensions.DependencyInjection;

namespace Common.Infrastructure.Persistence.EventSourcing;
public static class Setup
{
    public static IServiceCollection AddEventSourcingInterceptors(this IServiceCollection services)
        => services
            .AddSingleton<InsertEventStoreEventsInterceptor>();
}
