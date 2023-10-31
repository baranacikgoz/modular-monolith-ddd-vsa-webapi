using Microsoft.Extensions.DependencyInjection;

namespace IdentityAndAuth.Features.Tokens.Services;

public static class Setup
{
    public static IServiceCollection AddTokensServices(this IServiceCollection services)
        => services
            .AddTransient<ITokenService, TokenService>();
}
