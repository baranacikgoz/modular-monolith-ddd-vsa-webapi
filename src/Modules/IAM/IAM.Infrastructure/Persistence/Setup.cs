using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Builder;
using IAM.Persistence.Seeding;
using MassTransit;
using Common.Infrastructure.Persistence.Context;
using Common.Infrastructure.Persistence.UoW;
using Common.Infrastructure.Persistence.Repository;

namespace IAM.Infrastructure.Persistence;

internal static class Setup
{
    public static IServiceCollection AddPersistence(this IServiceCollection services)
        => services
            .AddTransient<Seeder>()
            .AddModuleDbContext<IAMDbContext>(moduleName: nameof(IAM))
            .AddModuleUnitOfWork<IAMDbContext>(moduleName: nameof(IAM))
            .AddModuleRepositories<IAMDbContext>(assemblyContainingEntities: typeof(Domain.IAssemblyReference).Assembly);

    public static WebApplication UsePersistence(this WebApplication app)
    {
        using (var scope = app.Services.CreateScope())
        {
            var busControl = scope.ServiceProvider.GetRequiredService<IBusControl>();
            busControl.Start();

            var context = scope.ServiceProvider.GetRequiredService<IAMDbContext>();
            context.Database.Migrate();

            var seeder = scope.ServiceProvider.GetRequiredService<Seeder>();
            seeder.SeedDbAsync().GetAwaiter().GetResult();

            busControl.Stop();
        }

        return app;
    }
}
