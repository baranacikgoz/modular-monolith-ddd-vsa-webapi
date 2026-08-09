using Common.Application.Auth;
using Common.Infrastructure.Auth.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Common.Infrastructure.Auth;

public static class Setup
{
    public static IServiceCollection AddCommonAuth(this IServiceCollection services)
    {
        return services
            .AddScoped<ICurrentUser, CurrentUser>();
    }
}
