using IAM.Application.Tokens.Services;
using IAM.Infrastructure.Tokens.Services;
using Microsoft.Extensions.DependencyInjection;

namespace IAM.Infrastructure.Tokens;

internal static class Setup
{
    public static IServiceCollection AddTokensInfrastructure(this IServiceCollection services)
    {
        return services
            .AddSingleton<ITokenService, TokenService>();
    }
}
