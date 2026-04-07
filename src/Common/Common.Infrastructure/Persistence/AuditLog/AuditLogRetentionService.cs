using Common.Application.Options;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Npgsql;

namespace Common.Infrastructure.Persistence.AuditLog;

public sealed partial class AuditLogRetentionService(
    IOptions<DatabaseOptions> databaseOptions,
    IOptions<AuditLogOptions> auditLogOptions,
    ILogger<AuditLogRetentionService> logger)
{
    private readonly string _connectionString = databaseOptions.Value.ConnectionString;
    private readonly int _retentionDays = auditLogOptions.Value.RetentionDays;

    /// <summary>
    /// Purges audit log entries older than the configured retention period
    /// across all module schemas.
    /// </summary>
    public async Task PurgeExpiredEntriesAsync(CancellationToken cancellationToken = default)
    {
        var cutoffDate = DateTimeOffset.UtcNow.AddDays(-_retentionDays);

        LoggerMessages.LogRetentionStart(logger, _retentionDays, cutoffDate);

        await using var connection = new NpgsqlConnection(_connectionString);
        await connection.OpenAsync(cancellationToken);

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

        var totalDeleted = 0;
        foreach (var schema in schemas)
        {
            // Schema names are sourced from information_schema (trusted), not user input.
            // Using string interpolation for the schema identifier is safe here.
#pragma warning disable CA2100 // Schema name is from information_schema, not user input
            var deleteQuery = $"""
                DELETE FROM "{schema}"."AuditLog"
                WHERE "CreatedOn" < @cutoffDate;
                """;

            await using var deleteCmd = new NpgsqlCommand(deleteQuery, connection);
#pragma warning restore CA2100
            deleteCmd.Parameters.AddWithValue("@cutoffDate", cutoffDate);

            var deleted = await deleteCmd.ExecuteNonQueryAsync(cancellationToken);
            totalDeleted += deleted;

            if (deleted > 0)
            {
                LoggerMessages.LogSchemaRetention(logger, schema, deleted);
            }
        }

        LoggerMessages.LogRetentionComplete(logger, totalDeleted);
    }

    private static partial class LoggerMessages
    {
        [LoggerMessage(Level = LogLevel.Information,
            Message = "Starting audit log retention purge. RetentionDays={RetentionDays}, CutoffDate={CutoffDate}.")]
        public static partial void LogRetentionStart(ILogger logger, int retentionDays, DateTimeOffset cutoffDate);

        [LoggerMessage(Level = LogLevel.Information,
            Message = "Purged {Deleted} expired audit log entries from schema '{Schema}'.")]
        public static partial void LogSchemaRetention(ILogger logger, string schema, int deleted);

        [LoggerMessage(Level = LogLevel.Information,
            Message = "Audit log retention purge complete. TotalDeleted={TotalDeleted}.")]
        public static partial void LogRetentionComplete(ILogger logger, int totalDeleted);
    }
}
