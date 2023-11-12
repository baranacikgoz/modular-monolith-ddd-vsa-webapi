using IdentityAndAuth.Features.Tokens.Domain.Services;
using Microsoft.Extensions.DependencyInjection;

namespace IdentityAndAuth.Features.Tokens.Infrastructure;

internal static class Setup
{
    public static IServiceCollection AddTokensInfrastructure(this IServiceCollection services)
        => services
            .AddTransient<ITokenService, TokenService>();
}
