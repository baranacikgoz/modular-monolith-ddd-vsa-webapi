using Microsoft.Extensions.DependencyInjection;

namespace IdentityAndAuth.Features.Tokens.Services;

internal static class Setup
{
    public static IServiceCollection AddTokensServices(this IServiceCollection services)
        => services
            .AddTransient<ITokenService, TokenService>();
}
