using Common.Domain.StronglyTypedIds;

namespace Common.IntegrationEvents;

public sealed record UserRegisteredIntegrationEvent(
    ApplicationUserId UserId,
    string FullName,
    string PhoneNumber
) : IntegrationEvent;
