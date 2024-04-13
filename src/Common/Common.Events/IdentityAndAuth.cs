using Common.Core.Contracts;

namespace Common.IntegrationEvents;

public sealed record UserRegisteredIntegrationEvent(
    Guid UserId,
    string Name,
    string PhoneNumber
    ) : DomainEvent;
