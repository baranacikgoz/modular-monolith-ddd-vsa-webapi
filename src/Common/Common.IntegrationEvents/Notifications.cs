namespace Common.IntegrationEvents;

/// <summary>
///     A condition that needs a human right now. Addressed by realm role name: Notifications pushes it to every
///     connected user holding <paramref name="RoleName" /> and emails the projected recipients of that role.
/// </summary>
public sealed record CriticalAlertRaisedIntegrationEvent(string RoleName, string Title, string Message) : IntegrationEvent;
