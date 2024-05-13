using IdentityAndAuth.Application.Tokens.Services;
using IdentityAndAuth.Infrastructure.Tokens.Services;
using Microsoft.Extensions.DependencyInjection;

namespace IdentityAndAuth.Infrastructure.Tokens;

internal static class Setup
{
    public static IServiceCollection AddTokensInfrastructure(this IServiceCollection services)
        => services
            .AddTransient<ITokenService, TokenService>();
}
