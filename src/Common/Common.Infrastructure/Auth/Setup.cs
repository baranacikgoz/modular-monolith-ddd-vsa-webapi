using Common.Application.Auth;
using Common.Infrastructure.Auth.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace Common.Infrastructure.Auth;

public static class Setup
{
    public static IServiceCollection AddCommonAuth(this IServiceCollection services)
    {
        return services
            .AddScoped<ICurrentUser, CurrentUser>(sp =>
            {
                var httpContext = sp.GetRequiredService<IHttpContextAccessor>().HttpContext;
                var user = httpContext?.User;

                return new CurrentUser(user);
            });
    }
}
