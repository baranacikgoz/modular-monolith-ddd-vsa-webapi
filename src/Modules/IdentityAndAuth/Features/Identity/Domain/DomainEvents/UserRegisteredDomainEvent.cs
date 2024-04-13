using Common.Core.Contracts;

namespace IdentityAndAuth.Features.Identity.Domain.DomainEvents;
public sealed record UserRegisteredDomainEvent(
    ApplicationUserId UserId,
    string Name,
    string LastName,
    string PhoneNumber,
    string NationalIdentityNumber,
    DateOnly BirthDate
    ) : DomainEvent;
