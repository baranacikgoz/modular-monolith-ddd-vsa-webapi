using Common.Application.Options;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Common.Infrastructure.Modules;

/// <summary>
///     Marker interface for modules that are always loaded regardless of configuration or test overrides.
///     Implement this instead of <see cref="IModule"/> for infrastructure modules (e.g. Outbox, BackgroundJobs)
///     that other modules depend on.
/// </summary>
public interface ICoreModule : IModule;

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
    ///     Gets the custom ActivitySource names this module registers for distributed tracing.
    ///     The Host will call .AddSource() for each name.
    /// </summary>
    IEnumerable<string> ActivitySourceNames { get; }

    /// <summary>
    ///     Gets the custom Meter names this module registers for metrics.
    ///     The Host will call .AddMeter() for each name.
    /// </summary>
    IEnumerable<string> MeterNames { get; }

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
