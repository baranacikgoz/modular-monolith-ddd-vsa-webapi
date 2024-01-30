using Microsoft.Extensions.DependencyInjection;
using Sales.Features.Stores.UseCases.v1.Create;

namespace Sales.Features.Stores.Infrastructure;

internal static class Setup
{
    public static IServiceCollection AddStoresInfrastructure(this IServiceCollection services)
        => services
            .AddScoped<ISomeOtherDummyService, SomeOtherDummyService>();
}
