using Common.Infrastructure.Persistence.Auditing;
using Common.Infrastructure.Persistence.EventSourcing;
using Microsoft.Extensions.DependencyInjection;

namespace Common.Infrastructure.Persistence;
public static class Setup
{
    public static IServiceCollection AddCommonPersistence(this IServiceCollection services)
        => services
            .AddEventSourcingInterceptors()
            .AddAuditingInterceptors();
}
