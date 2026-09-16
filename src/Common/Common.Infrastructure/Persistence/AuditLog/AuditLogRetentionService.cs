using Common.Application.Options;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Npgsql;

namespace Common.Infrastructure.Persistence.AuditLog;

public sealed partial class AuditLogRetentionService(
    NpgsqlDataSource dataSource,
    IOptions<AuditLogOptions> auditLogOptions,
    ILogger<AuditLogRetentionService> logger)
{
    public async Task PurgeExpiredEntriesAsync(CancellationToken cancellationToken = default)
    {
        var defaultRetentionDays = auditLogOptions.Value.RetentionDays;

        LogRetentionStart(logger, defaultRetentionDays, auditLogOptions.Value.PurgeBatchSize);

        // Borrow a connection from the shared NpgsqlDataSource pool instead of opening a brand-new
        // unpooled connection per purge run.
        await using var connection = await dataSource.OpenConnectionAsync(cancellationToken);

        // Discover all AuditLog tables across schemas
        const string discoverSchemasQuery = """
            SELECT table_schema
            FROM information_schema.tables
            WHERE table_name = 'AuditLog'
              AND table_type = 'BASE TABLE'
            ORDER BY table_schema;
            """;

        var schemas = new List<string>();
        await using (var cmd = new NpgsqlCommand(discoverSchemasQuery, connection))
        {
            await using var reader = await cmd.ExecuteReaderAsync(cancellationToken);
            while (await reader.ReadAsync(cancellationToken))
            {
                schemas.Add(reader.GetString(0));
            }
        }

        var unmatchedOverrides = auditLogOptions.Value.PerSchemaRetentionDays.Keys
            .Where(schema => !schemas.Contains(schema))
            .ToList();
        if (unmatchedOverrides.Count > 0)
        {
            LogUnmatchedSchemaOverrides(logger, string.Join(", ", unmatchedOverrides));
        }

        var batchSize = auditLogOptions.Value.PurgeBatchSize;
        var totalDeleted = 0;
        foreach (var schema in schemas)
        {
            var retentionDays = auditLogOptions.Value.PerSchemaRetentionDays.GetValueOrDefault(schema, defaultRetentionDays);
            var cutoffDate = DateTimeOffset.UtcNow.AddDays(-retentionDays);

            // Schema names are sourced from information_schema (trusted), not user input.
            // Using string interpolation for the schema identifier is safe here.
#pragma warning disable CA2100 // Schema name is from information_schema, not user input
            var deleteQuery = $"""
                DELETE FROM "{schema}"."AuditLog"
                WHERE ctid IN (
                    SELECT ctid FROM "{schema}"."AuditLog"
                    WHERE "CreatedOn" < @cutoffDate
                    LIMIT @batchSize
                );
                """;

            while (!cancellationToken.IsCancellationRequested)
            {
                await using var deleteCmd = new NpgsqlCommand(deleteQuery, connection);
#pragma warning restore CA2100
                deleteCmd.Parameters.AddWithValue("@cutoffDate", cutoffDate);
                deleteCmd.Parameters.AddWithValue("@batchSize", batchSize);

                var deleted = await deleteCmd.ExecuteNonQueryAsync(cancellationToken);
                if (deleted == 0)
                {
                    break;
                }

                totalDeleted += deleted;
                LogSchemaRetention(logger, schema, deleted, retentionDays);
            }
        }

        LogRetentionComplete(logger, totalDeleted);
    }

    [LoggerMessage(Level = LogLevel.Information,
        Message = "Starting audit log retention purge. DefaultRetentionDays={DefaultRetentionDays}, PurgeBatchSize={PurgeBatchSize}.")]
    private static partial void LogRetentionStart(ILogger logger, int defaultRetentionDays, int purgeBatchSize);

    [LoggerMessage(Level = LogLevel.Information,
        Message = "Purged {Deleted} expired audit log entries from schema '{Schema}' (RetentionDays={RetentionDays}).")]
    private static partial void LogSchemaRetention(ILogger logger, string schema, int deleted, int retentionDays);

    [LoggerMessage(Level = LogLevel.Information,
        Message = "Audit log retention purge complete. TotalDeleted={TotalDeleted}.")]
    private static partial void LogRetentionComplete(ILogger logger, int totalDeleted);

    [LoggerMessage(Level = LogLevel.Warning,
        Message = "AuditLogOptions.PerSchemaRetentionDays references schema(s) with no AuditLog table, so the override has no effect: [{UnmatchedSchemas}].")]
    private static partial void LogUnmatchedSchemaOverrides(ILogger logger, string unmatchedSchemas);
}
