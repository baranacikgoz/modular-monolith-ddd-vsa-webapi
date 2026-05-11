using Common.Domain.Events;
using Common.Domain.StronglyTypedIds;

namespace IAM.Domain.Identity.DomainEvents.v1;

public sealed record V1UserRegisteredDomainEvent(
    ApplicationUserId UserId,
    string FullName,
    string PhoneNumber,
    DateOnly BirthDate,
    Uri? ImageUrl = null
) : DomainEvent;
