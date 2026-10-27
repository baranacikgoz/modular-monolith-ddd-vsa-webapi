using Common.Domain.Entities;
using Inventory.Domain.StockLevels;

namespace Inventory.Domain.StockPushes;

/// <summary>
///     "This stock level changed and its store's warehouse has not been sent the new quantity yet". One row per stock
///     level, however many times it changed: a mark upserts the row and takes a new, ever larger
///     <see cref="MarkVersion" />, so a push clears only the version it read and a change made while it was pushing
///     survives. Not an aggregate: no invariant, no domain event. Written only by <c>StockPushMarks</c>' SQL, in the
///     transaction of the change that raised the mark, and materialized by EF via reflection.
/// </summary>
#pragma warning disable S3453, S1144
public sealed class StockPushDirtyItem : AuditableEntity
{
    public const string MarkVersionSequence = "StockPushMarkVersions";
    public const int LastErrorKeyMaxLength = 128;

    private StockPushDirtyItem()
    {
    } // EF Core needs a parameterless ctor

    public StockLevelId StockLevelId { get; private init; }

    /// <summary>The store whose warehouse is sent the quantity; the store's active connection is resolved when the push runs.</summary>
    public DefaultIdType StoreId { get; private init; }

    /// <summary>What the warehouse calls the product: the Products product id.</summary>
    public DefaultIdType ProductId { get; private init; }

    /// <summary>Drawn from a database sequence on every mark, so a version is never reused, not even after the row was cleared.</summary>
    public long MarkVersion { get; private set; }

    /// <summary>When the row was first marked since it was last cleared; the sweep republishes rows older than a configured age.</summary>
    public DateTimeOffset MarkedOn { get; private init; }

    /// <summary>
    ///     How many pushes of this stock level the warehouse refused alone, since the row was last marked. A row at the
    ///     configured maximum is parked: pushes skip it and the sweep ignores it, so one stock level the warehouse cannot
    ///     take never holds back the ones behind it. The row stays in the table for a person to see; a new mark (the
    ///     quantity changed again) starts the count over.
    /// </summary>
    public int PushAttempts { get; private set; }

    /// <summary>The error key of the latest refusal; null while the row has none.</summary>
    public string? LastErrorKey { get; private set; }
}
#pragma warning restore S3453, S1144
