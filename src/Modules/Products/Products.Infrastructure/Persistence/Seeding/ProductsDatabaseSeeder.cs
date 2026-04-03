using Common.Application.Persistence;
using Microsoft.Extensions.DependencyInjection;

namespace Products.Infrastructure.Persistence.Seeding;

internal sealed class ProductsDatabaseSeeder(IServiceScopeFactory serviceScopeFactory) : IDatabaseSeeder
{
    // Matches ProductsModule.StartupPriority = 4, runs after IamDatabaseSeeder (priority 2)
    public int Priority => 4;

    public async Task SeedAsync(CancellationToken cancellationToken)
    {
        using var scope = serviceScopeFactory.CreateScope();
        var seeder = scope.ServiceProvider.GetRequiredService<Seeder>();
        await seeder.SeedDbAsync(cancellationToken);
    }
}
