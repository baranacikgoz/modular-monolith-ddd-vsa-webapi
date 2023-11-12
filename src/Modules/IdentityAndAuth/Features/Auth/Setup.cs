
using IdentityAndAuth.Features.Auth.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace IdentityAndAuth.Features.Auth;

internal static class Setup
{
    public static IServiceCollection AddAuthFeature(this IServiceCollection services, IConfiguration configuration)
        => services
            .AddAuthInfrastructure(configuration);
}
