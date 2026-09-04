namespace Common.Domain.Events;

/// <summary>
/// Returned by <c>PolymorphicEventConverter</c> in place of a hard failure when a stored
/// DomainEvent's CLR type cannot be resolved (e.g. its defining module is disabled via
/// modules.json, so the assembly is never loaded). Keeps an AuditLog read path from throwing
/// a NullReferenceException on <c>DomainEvent.GetType()</c> for an unresolvable row.
/// </summary>
public sealed record UnknownDomainEvent(string EventTypeFullName, string RawEventData) : DomainEvent;
