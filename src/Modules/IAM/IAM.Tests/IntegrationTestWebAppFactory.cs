using System.Data.Common;
using Common.Tests;
using IAM.Application.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace IAM.Tests;

public class IntegrationTestWebAppFactory : IntegrationTestFactory
{
    public override async Task InitializeAsync()
    {
        await base.InitializeAsync();

        using var scope = Services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<IIAMDbContext>() as DbContext;
        if (db != null)
        {
            await db.Database.MigrateAsync(); // Apply migrations so Respawner can see actual tables
        }
    }
}
