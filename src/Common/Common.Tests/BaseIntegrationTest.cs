using Microsoft.Extensions.DependencyInjection;
using Respawn;
using Respawn.Graph;
using Xunit;
using System.Data.Common;
using Npgsql;

namespace Common.Tests;

[Collection("IntegrationTestCollection")]
public abstract class BaseIntegrationTest : IAsyncLifetime
{
    protected IntegrationTestFactory Factory { get; }
    protected IServiceScope Scope { get; }

    private static Respawner? _respawner;

    protected BaseIntegrationTest(IntegrationTestFactory factory)
    {
        Factory = factory;
        Scope = factory.Services.CreateScope();
    }

    public virtual async Task InitializeAsync()
    {
        if (_respawner == null)
        {
            await using var conn = new NpgsqlConnection(Factory.ConnectionString);
            await conn.OpenAsync();

            _respawner = await Respawner.CreateAsync(conn, new RespawnerOptions
            {
                DbAdapter = DbAdapter.Postgres,
                SchemasToInclude = new[] { "public" },
                TablesToIgnore = new[] { new Table("__EFMigrationsHistory") }
            });
        }

        await using var connection = new NpgsqlConnection(Factory.ConnectionString);
        await connection.OpenAsync();
        
        await _respawner.ResetAsync(connection);
    }

    public virtual Task DisposeAsync()
    {
        Scope.Dispose();
        return Task.CompletedTask;
    }
}

[CollectionDefinition("IntegrationTestCollection")]
public class IntegrationTestCollection : ICollectionFixture<IntegrationTestFactory>
{
    // This class has no code, and is never created. Its purpose is simply
    // to be the place to apply [CollectionDefinition] and all the
    // ICollectionFixture<> interfaces.
}
