using Microsoft.Extensions.DependencyInjection;

namespace Common.Infrastructure.Persistence.EventSourcing;

public static class Setup
{
    public static IServiceCollection AddEventSourcingInterceptors(this IServiceCollection services)
    {
        // EventStore event insertion is now handled directly by BaseDbContext.SaveChangesAsync()
        // in a unified event collection pass. No interceptor registration needed.
        return services;
    }
}
