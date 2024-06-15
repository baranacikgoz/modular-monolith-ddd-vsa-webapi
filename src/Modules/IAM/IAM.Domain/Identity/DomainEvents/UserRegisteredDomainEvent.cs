using Common.Domain.Events;
using Common.Domain.StronglyTypedIds;

namespace IAM.Domain.Identity.DomainEvents;
public sealed record UserRegisteredDomainEvent(
    ApplicationUserId UserId,
    string Name,
    string LastName,
    string PhoneNumber,
    string NationalIdentityNumber,
    DateOnly BirthDate
    ) : DomainEvent;
