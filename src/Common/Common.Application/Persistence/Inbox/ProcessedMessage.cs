using Common.Domain.Entities;
using EntityFramework.Exceptions.Common;

namespace Common.Application.Persistence.Inbox;

/// <summary>
///     Transactional inbox row: one per (consumer, message) pair, written by
///     <c>IntegrationEventHandlerBase</c> into the consuming module's own DbContext before
///     <c>ProcessAsync</c> runs, so the handler's <c>SaveChangesAsync</c> commits the "already processed"
///     mark and the side effects in one transaction. A redelivery after commit (or a second instance) then
///     fails on the composite primary key instead of running the handler twice. The FusionCache key is only a
///     fast pre-filter in front of this row.
/// </summary>
public sealed class ProcessedMessage : AuditableEntity
{
    public const string TableName = "ProcessedMessages";
    public const string PrimaryKeyName = "PK_" + TableName;
    public const int ConsumerNameMaxLength = 256;

    private ProcessedMessage(string consumerName, DefaultIdType messageId, DateTimeOffset processedOn)
    {
        ConsumerName = consumerName;
        MessageId = messageId;
        ProcessedOn = processedOn;
    }

#pragma warning disable CS8618 // EF materialization sets the properties via reflection.
    private ProcessedMessage()
    {
    }
#pragma warning restore CS8618

    public string ConsumerName { get; private set; }
    public DefaultIdType MessageId { get; private set; }
    public DateTimeOffset ProcessedOn { get; private set; }

    public static ProcessedMessage Create(string consumerName, DefaultIdType messageId, DateTimeOffset processedOn)
        => new(consumerName, messageId, processedOn);

    /// <summary>
    ///     True when the unique violation is this table's primary key, i.e. the message was already processed
    ///     by this consumer. Npgsql reports the constraint name; the table name is a fallback for a driver that
    ///     does not.
    /// </summary>
    public static bool IsDuplicateKey(UniqueConstraintException exception)
        => string.Equals(exception.ConstraintName, PrimaryKeyName, StringComparison.Ordinal)
           || (exception.ConstraintName is null
               && exception.SchemaQualifiedTableName is { } table
               && table.EndsWith(TableName, StringComparison.Ordinal));
}
