using Common.Endpoints.Versioning;
using Common.Infrastructure.Modules;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Products.Endpoints.Products;
using Products.Endpoints.ProductTemplates;
using Products.Endpoints.Stores;
using Products.Infrastructure.Persistence;
using SharpGrip.FluentValidation.AutoValidation.Endpoints.Extensions;

namespace Products.Endpoints;

public sealed class ProductsModule : IModule
{
    public string Name => "Products";

    public void AddServices(IServiceCollection services, IConfiguration configuration)
    {
        services.AddPersistence();
    }

    public void UseModule(IApplicationBuilder app)
    {
        app.UsePersistence();
    }

    public void MapEndpoints(IEndpointRouteBuilder endpoints)
    {
        var apiVersionSet = endpoints.GetApiVersionSet();
        var versionedApiGroup = endpoints
            .MapGroup("/v{version:apiVersion}")
            .AddFluentValidationAutoValidation()
            .WithApiVersionSet(apiVersionSet)
            .RequireAuthorization();

        versionedApiGroup.MapStoresEndpoints();
        versionedApiGroup.MapProductsEndpoints();
        versionedApiGroup.MapProductTemplatesEndpoints();
    }

    public IEnumerable<Action<global::Microsoft.AspNetCore.RateLimiting.RateLimiterOptions, global::Common.Application.Options.CustomRateLimitingOptions>>? RateLimitingPolicies => global::Products.Infrastructure.RateLimiting.Policies.Get();
}
