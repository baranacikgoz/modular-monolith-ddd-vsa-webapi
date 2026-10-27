using Microsoft.Extensions.DependencyInjection;

namespace Common.Infrastructure.Security;

public static class Setup
{
    /// <summary>
    ///     Registers <see cref="SecretProtector" /> as a singleton. It reads and checks its key when first resolved, so
    ///     registering it is safe even before any module consumes it.
    /// </summary>
    public static IServiceCollection AddCommonSecretProtection(this IServiceCollection services)
    {
        return services.AddSingleton<SecretProtector>();
    }
}
