using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Common.Infrastructure.Modules;

public interface IModule
{
    /// <summary>
    /// Gets the name of the module (e.g., "Products").
    /// </summary>
    string Name { get; }

    /// <summary>
    /// Registers the module's services into the dependency injection container.
    /// </summary>
    void AddServices(IServiceCollection services, IConfiguration configuration);

    /// <summary>
    /// Configures the module's middleware pipeline.
    /// </summary>
    void UseModule(IApplicationBuilder app);

    /// <summary>
    /// Maps the module's endpoints.
    /// </summary>
    void MapEndpoints(IEndpointRouteBuilder endpoints);

    /// <summary>
    /// Gets the custom rate limiting policies for this module, if any.
    /// </summary>
    IEnumerable<Action<Microsoft.AspNetCore.RateLimiting.RateLimiterOptions, Common.Application.Options.CustomRateLimitingOptions>>? RateLimitingPolicies => null;
}
