using Common.Core.Contracts;
using Common.Core.Contracts.Identity;

namespace IdentityAndAuth.Features.Identity.Domain.DomainEvents;
public sealed record UserRegisteredDomainEvent(
    ApplicationUserId UserId,
    string Name,
    string LastName,
    string PhoneNumber,
    string NationalIdentityNumber,
    DateOnly BirthDate
    ) : DomainEvent;
