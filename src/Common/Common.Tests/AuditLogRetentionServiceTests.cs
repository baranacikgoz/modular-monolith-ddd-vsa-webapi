using Common.Application.Options;
using Common.Infrastructure.Persistence.AuditLog;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Options;
using Npgsql;
using Xunit;

namespace Common.Tests;

#pragma warning disable CA1707 // Remove the underscores from member name
public class AuditLogRetentionServiceTests(IntegrationTestFactory factory) : BaseIntegrationTest(factory)
{
    private const string Schema = "IAM";

    [Fact]
    public async Task PurgeExpiredEntries_MoreRowsThanBatchSize_DeletesAllExpiredKeepsRecent()
    {
        var cutoff = DateTimeOffset.UtcNow.AddDays(-90);
        await SeedRowsAsync(count: 12, createdOn: cutoff.AddDays(-1));
        await SeedRowsAsync(count: 3, createdOn: cutoff.AddDays(1));

        // Batch size (5) smaller than the expired row count (12) forces the purge loop to iterate
        // more than once, proving it doesn't stop after a single DELETE.
        var service = CreateService(purgeBatchSize: 5);
        await service.PurgeExpiredEntriesAsync();

        Assert.Equal(3, await CountRowsAsync());
    }

    [Fact]
    public async Task PurgeExpiredEntries_NoExpiredRows_DeletesNothing()
    {
        var cutoff = DateTimeOffset.UtcNow.AddDays(-90);
        await SeedRowsAsync(count: 3, createdOn: cutoff.AddDays(1));

        var service = CreateService(purgeBatchSize: 5);
        await service.PurgeExpiredEntriesAsync();

        Assert.Equal(3, await CountRowsAsync());
    }

    private AuditLogRetentionService CreateService(int purgeBatchSize)
    {
        var dataSource = Scope.ServiceProvider.GetRequiredService<NpgsqlDataSource>();
        var options = Options.Create(new AuditLogOptions { RetentionDays = 90, PurgeBatchSize = purgeBatchSize });
        return new AuditLogRetentionService(dataSource, options, NullLogger<AuditLogRetentionService>.Instance);
    }

    private async Task SeedRowsAsync(int count, DateTimeOffset createdOn)
    {
        await using var connection = new NpgsqlConnection(Factory.ConnectionString);
        await connection.OpenAsync();

        const string sql = $$"""
            INSERT INTO "{{Schema}}"."AuditLog" ("AggregateId", "AggregateType", "Version", "EventType", "Event", "CreatedOn")
            VALUES (@aggregateId, 'TestAggregate', 1, 'TestEvent', '{}'::jsonb, @createdOn);
            """;

        for (var i = 0; i < count; i++)
        {
            await using var cmd = new NpgsqlCommand(sql, connection);
            cmd.Parameters.AddWithValue("@aggregateId", Guid.NewGuid());
            cmd.Parameters.AddWithValue("@createdOn", createdOn);
            await cmd.ExecuteNonQueryAsync();
        }
    }

    private async Task<long> CountRowsAsync()
    {
        await using var connection = new NpgsqlConnection(Factory.ConnectionString);
        await connection.OpenAsync();

        await using var cmd = new NpgsqlCommand($"""SELECT COUNT(*) FROM "{Schema}"."AuditLog";""", connection);
        return (long)(await cmd.ExecuteScalarAsync())!;
    }
}
#pragma warning restore CA1707
