using Inventory.Infrastructure.Persistence;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Inventory.Application.Stores;
using Inventory.Application.Products;
using Inventory.Application.StoreProducts;
using Common.Infrastructure;
using System.Data;
using System.Reflection;
using Microsoft.AspNetCore.RateLimiting;
using Common.Infrastructure.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Hosting;
using Inventory.Infrastructure.RateLimiting;
using System.Composition;

namespace Inventory.Infrastructure;

public class InventoryModule : IModule
{
    public int RegistrationPriority => 1;

    public IEnumerable<Func<string?, IDbCommand, bool>> EfCoreInstrumentationFilters() => [];

    public IEnumerable<Assembly> GetAssemblies()
    {
        yield return typeof(Domain.IAssemblyReference).Assembly;
        yield return typeof(Application.IAssemblyReference).Assembly;
        yield return typeof(Infrastructure.IAssemblyReference).Assembly;
    }

    public IEnumerable<Action<RateLimiterOptions, CustomRateLimitingOptions>> RateLimitingPolicies()
    {
        yield return CreateStore.Policy;
    }

    public void Register(IServiceCollection services, IConfiguration configuration, IWebHostEnvironment env)
        => services
            .AddPersistence();

    public void Use(WebApplication app, RouteGroupBuilder routeGroupBuilder)
    {
        app.UsePersistence();

        routeGroupBuilder.MapStoresEndpoints();
        routeGroupBuilder.MapProductsEndpoints();
        routeGroupBuilder.MapStoreProductsEndpoints();
    }
}
