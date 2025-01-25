using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Products.Infrastructure.Persistence;

namespace Products.Infrastructure;

public static class ModuleInstaller
{
    public static IServiceCollection AddProductsModule(this IServiceCollection services)
        => services
            .AddPersistence();

    public static WebApplication UseProductsModule(this WebApplication app)
    {
        app.UsePersistence();

        return app;
    }
}
