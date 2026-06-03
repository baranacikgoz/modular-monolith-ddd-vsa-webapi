using System.Text.Json;
using Common.Infrastructure.Persistence;
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
        var tracker = Scope.ServiceProvider.GetRequiredService<SeedingCompletionTracker>();
        await tracker.WaitForSeedingAsync();

        if (_respawner == null)
        {
            await using var conn = new NpgsqlConnection(Factory.ConnectionString);
            await conn.OpenAsync();

            _respawner = await Respawner.CreateAsync(conn,
                new RespawnerOptions
                {
                    DbAdapter = DbAdapter.Postgres,
                    SchemasToInclude =
                        new[] { "public", "IAM", "Outbox", "Products", "BackgroundJobs", "Notifications" },
                    TablesToIgnore = new[]
                    {
                        new Table("__EFMigrationsHistory"), new Table("AspNetRoles", "IAM"),
                        new Table("AspNetRoleClaims", "IAM")
                    }
                });
        }

        await using var connection = new NpgsqlConnection(Factory.ConnectionString);
        await connection.OpenAsync();

        try
        {
            await _respawner.ResetAsync(connection);
        }
        catch (Exception ex)
        {
            // TEMP CI DIAGNOSTIC: Respawn's DELETE times out only on CI. xUnit swallows Console output,
            // so embed the pg activity / blocking-lock dump in the thrown exception message instead.
            var dump = await DumpDbActivityAsync();
            throw new InvalidOperationException(
                $"[RESPAWN-DIAG] ResetAsync failed: {ex.GetType().Name}: {ex.Message}\n{dump}", ex);
        }
    }

#pragma warning disable CA1305 // Invariant formatting irrelevant for TEMP CI diagnostic output.
    private async Task<string> DumpDbActivityAsync()
    {
        var sb = new System.Text.StringBuilder();
        try
        {
            await using var diag = new NpgsqlConnection(Factory.ConnectionString);
            await diag.OpenAsync();

            await using (var cmd = new NpgsqlCommand(
                """
                SELECT pid, state, wait_event_type, wait_event,
                       EXTRACT(EPOCH FROM (now() - query_start))::int AS dur_s,
                       left(regexp_replace(query, '\s+', ' ', 'g'), 200) AS q
                FROM pg_stat_activity
                WHERE datname = current_database() AND pid <> pg_backend_pid()
                ORDER BY query_start
                """, diag))
            {
                cmd.CommandTimeout = 10;
                await using var r = await cmd.ExecuteReaderAsync();
                sb.AppendLine("[RESPAWN-DIAG] pg_stat_activity:");
                while (await r.ReadAsync())
                {
                    sb.AppendLine(
                        $"  pid={r["pid"]} state={r["state"]} wait={r["wait_event_type"]}/{r["wait_event"]} dur_s={r["dur_s"]} q={r["q"]}");
                }
            }

            await using (var cmd = new NpgsqlCommand(
                """
                SELECT blocked.pid AS blocked_pid,
                       left(regexp_replace(blocked.query, '\s+', ' ', 'g'), 120) AS blocked_q,
                       blocking.pid AS blocking_pid,
                       blocking.state AS blocking_state,
                       left(regexp_replace(blocking.query, '\s+', ' ', 'g'), 120) AS blocking_q
                FROM pg_stat_activity blocked
                JOIN LATERAL unnest(pg_blocking_pids(blocked.pid)) AS bp(pid) ON true
                JOIN pg_stat_activity blocking ON blocking.pid = bp.pid
                """, diag))
            {
                cmd.CommandTimeout = 10;
                await using var r = await cmd.ExecuteReaderAsync();
                sb.AppendLine("[RESPAWN-DIAG] blocking pairs:");
                while (await r.ReadAsync())
                {
                    sb.AppendLine(
                        $"  blocked_pid={r["blocked_pid"]} q={r["blocked_q"]} <-- blocking_pid={r["blocking_pid"]} state={r["blocking_state"]} q={r["blocking_q"]}");
                }
            }
        }
#pragma warning disable CA1031
        catch (Exception diagEx)
#pragma warning restore CA1031
        {
            sb.AppendLine($"[RESPAWN-DIAG] diagnostic dump failed: {diagEx.Message}");
        }

        return sb.ToString();
    }
#pragma warning restore CA1305

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
