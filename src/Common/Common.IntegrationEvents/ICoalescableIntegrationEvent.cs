namespace Common.IntegrationEvents;

/// <summary>
///     An integration event that the domain events of one save may each collect for the same target, such as a bulk
///     operation touching many rows of one parent. Events of one CLR type with an equal non-null
///     <see cref="CoalesceKey" /> collected in one save are written to the outbox once: the first is kept, so the kept
///     event must stand for all of them. A later save publishes again.
/// </summary>
public interface ICoalescableIntegrationEvent
{
    /// <summary>Must carry everything that makes two payloads differ (usually the target id). Null never coalesces.</summary>
    string? CoalesceKey { get; }
}
