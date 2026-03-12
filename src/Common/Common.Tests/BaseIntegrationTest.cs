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

    private System.Text.Json.JsonSerializerOptions? _jsonSerializerOptions;
    protected System.Text.Json.JsonSerializerOptions JsonSerializerOptions
    {
        get
        {
            if (_jsonSerializerOptions == null)
            {
                var options = Scope.ServiceProvider.GetRequiredService<Microsoft.Extensions.Options.IOptions<Microsoft.AspNetCore.Http.Json.JsonOptions>>().Value.SerializerOptions;
                _jsonSerializerOptions = new System.Text.Json.JsonSerializerOptions(options);
                _jsonSerializerOptions.Converters.Insert(0, new Common.Tests.SystemTextJson.NullableStronglyTypedIdReadOnlyJsonConverter());
            }
            return _jsonSerializerOptions;
        }
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
                SchemasToInclude = new[] { "public", "IAM", "Products", "BackgroundJobs", "Notifications" },
                TablesToIgnore = new[] { new Table("__EFMigrationsHistory"), new Table("AspNetRoles", "IAM"), new Table("AspNetRoleClaims", "IAM") }
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
