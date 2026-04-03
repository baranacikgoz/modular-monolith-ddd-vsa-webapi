using System.Text.Json;
using Common.Tests.SystemTextJson;
using Microsoft.AspNetCore.Http.Json;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Npgsql;
using Respawn;
using Respawn.Graph;
using Xunit;

namespace Common.Tests;

[Collection("IntegrationTestCollection")]
public abstract class BaseIntegrationTest : IAsyncLifetime
{
    private static Respawner? _respawner;

    private JsonSerializerOptions? _jsonSerializerOptions;

    protected BaseIntegrationTest(IntegrationTestFactory factory)
    {
        Factory = factory;
        Scope = factory.Services.CreateScope();
    }

    protected IntegrationTestFactory Factory { get; }
    protected IServiceScope Scope { get; }

    protected JsonSerializerOptions JsonSerializerOptions
    {
        get
        {
            if (_jsonSerializerOptions == null)
            {
                var options = Scope.ServiceProvider.GetRequiredService<IOptions<JsonOptions>>().Value.SerializerOptions;
                _jsonSerializerOptions = new JsonSerializerOptions(options);
                _jsonSerializerOptions.Converters.Insert(0, new NullableStronglyTypedIdReadOnlyJsonConverter());
            }

            return _jsonSerializerOptions;
        }
    }

    public virtual async Task InitializeAsync()
    {
        var tracker = Scope.ServiceProvider.GetRequiredService<Common.Infrastructure.Persistence.SeedingCompletionTracker>();
        await tracker.WaitForSeedingAsync();

        if (_respawner == null)
        {
            await using var conn = new NpgsqlConnection(Factory.ConnectionString);
            await conn.OpenAsync();

            _respawner = await Respawner.CreateAsync(conn,
                new RespawnerOptions
                {
                    DbAdapter = DbAdapter.Postgres,
                    SchemasToInclude = new[] { "public", "IAM", "Products", "BackgroundJobs", "Notifications" },
                    TablesToIgnore = new[]
                    {
                        new Table("__EFMigrationsHistory"), new Table("AspNetRoles", "IAM"),
                        new Table("AspNetRoleClaims", "IAM")
                    }
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
