using Common.Application.Persistence;
using Common.Infrastructure.Persistence;
using Common.Infrastructure.Persistence.DbContext;
using IAM.Application.Persistence;
using IAM.Infrastructure.Persistence.Seeding;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace IAM.Infrastructure.Persistence;

public static class Setup
{
    public static IServiceCollection AddPersistence(this IServiceCollection services)
    {
        return services
            .AddTransient<Seeder>()
            .AddTransient<IDatabaseSeeder, IamDatabaseSeeder>()
            .AddModuleDbContext<IIAMDbContext, IAMDbContext>(nameof(IAM));
    }

    public static IApplicationBuilder UsePersistence(this IApplicationBuilder app)
    {
        var logger = app.ApplicationServices
            .GetRequiredService<ILoggerFactory>()
            .CreateLogger(typeof(Setup).FullName!);

        MigrationGuard.EnsureNoMigrationsPending<IAMDbContext>(
            app.ApplicationServices, logger, nameof(IAM));

        return app;
    }
}
