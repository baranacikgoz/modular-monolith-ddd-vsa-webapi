using Microsoft.Extensions.DependencyInjection;
using Sales.Features.Products.UseCases.v1.Create;

namespace Sales.Features.Products.Infrastructure;

internal static class Setup
{
    public static IServiceCollection AddProductsInfrastructure(this IServiceCollection services)
        => services
            .AddScoped<ISomeDummyService, SomeDummyService>();
}
