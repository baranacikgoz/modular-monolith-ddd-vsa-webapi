using Common.Persistence.TransactionalOutbox;
using Microsoft.Extensions.DependencyInjection;

namespace Common.Persistence.EventSourcing;
public static class Setup
{
    public static IServiceCollection AddEventSourcingInterceptors(this IServiceCollection services)
        => services
            .AddSingleton<InsertEventStoreEventsInterceptor>()
            .AddSingleton<RemoveStreamIfAggregateIsRemovedInterceptor>();
}
