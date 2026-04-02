using Common.Application.Options;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Common.Infrastructure.Modules;

public interface IModule
{
    /// <summary>
    ///     Gets the name of the module (e.g., "Products").
    /// </summary>
    string Name { get; }

    /// <summary>
    ///     Gets the startup priority of the module. Lower numbers start earlier. Default is 100.
    /// </summary>
    int StartupPriority { get; }

    /// <summary>
    ///     Gets the custom rate limiting policies for this module, if any.
    /// </summary>
    IEnumerable<Action<RateLimiterOptions, CustomRateLimitingOptions>>? RateLimitingPolicies => null;

    /// <summary>
    ///     Registers the module's services into the dependency injection container.
    /// </summary>
    void AddServices(IServiceCollection services, IConfiguration configuration);

    /// <summary>
    ///     Configures the module's middleware pipeline.
    /// </summary>
    void UseModule(IApplicationBuilder app);

    /// <summary>
    ///     Maps the module's endpoints.
    /// </summary>
    void MapEndpoints(IEndpointRouteBuilder endpoints);
}
