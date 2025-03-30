using Microsoft.Extensions.DependencyInjection;

namespace Common.Infrastructure.Persistence.Auditing;
public static class Setup
{
    public static IServiceCollection AddAuditingInterceptors(this IServiceCollection services)
        => services
            .AddScoped<ApplyAuditingInterceptor>();
}
