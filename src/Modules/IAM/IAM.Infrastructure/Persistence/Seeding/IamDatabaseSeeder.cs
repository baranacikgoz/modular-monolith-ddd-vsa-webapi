using Common.Application.Persistence;
using Microsoft.Extensions.DependencyInjection;

namespace IAM.Infrastructure.Persistence.Seeding;

internal sealed class IamDatabaseSeeder(IServiceScopeFactory serviceScopeFactory) : IDatabaseSeeder
{
    // Matches IamModule.StartupPriority = 2
    public int Priority => 2;

    public async Task SeedAsync(CancellationToken cancellationToken)
    {
        using var scope = serviceScopeFactory.CreateScope();
        var seeder = scope.ServiceProvider.GetRequiredService<Seeder>();
        await seeder.SeedDbAsync();
    }
}
