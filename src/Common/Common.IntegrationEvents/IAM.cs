using Common.Domain.StronglyTypedIds;

namespace Common.IntegrationEvents;

public sealed record UserRegisteredIntegrationEvent(
    ApplicationUserId UserId,
    string FullName,
    string PhoneNumber
) : IntegrationEvent;

public sealed record SessionTokenReuseDetectedIntegrationEvent(
    ApplicationUserId UserId,
    Guid SessionId,
    string? DeviceName
) : IntegrationEvent;
