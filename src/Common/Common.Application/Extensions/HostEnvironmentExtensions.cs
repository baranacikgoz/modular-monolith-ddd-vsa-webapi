using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Common.Application.Extensions;

public static class HostEnvironmentExtensions
{
    /// <summary>
    ///     Registration-time environment check for module <c>AddServices</c> methods, which only receive
    ///     <see cref="IServiceCollection"/> + <c>IConfiguration</c>. Reads the <see cref="IHostEnvironment"/>
    ///     instance the host builder registered before modules run — honoring <c>DOTNET_ENVIRONMENT</c>,
    ///     <c>ASPNETCORE_ENVIRONMENT</c>, and <c>UseEnvironment(...)</c> overrides alike — without building
    ///     a service provider (forbidden in registration code, see CLAUDE.md §7).
    /// </summary>
    public static bool IsProductionEnvironment(this IServiceCollection services)
        => services.LastOrDefault(descriptor => descriptor.ServiceType == typeof(IHostEnvironment))
            ?.ImplementationInstance is IHostEnvironment environment
           && environment.IsEnvironment(Environments.Production);
}
