using Common.Tests;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Products.Application.Persistence;

namespace Products.Tests;

public class IntegrationTestWebAppFactory : IntegrationTestFactory
{
    public override async Task InitializeAsync()
    {
        await base.InitializeAsync();

        using var scope = Services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<IProductsDbContext>() as DbContext;
        if (db != null)
        {
            await db.Database.MigrateAsync(); // Apply migrations so Respawner can see actual tables
        }
    }
}
