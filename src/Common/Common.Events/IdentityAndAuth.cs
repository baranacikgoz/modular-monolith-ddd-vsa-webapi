using Common.Core.Contracts;

namespace Common.Events;

public sealed record UserCreatedDomainEvent(
    Guid Id,
    string Name,
    string LastName,
    string PhoneNumber,
    string NationalIdentityNumber,
    DateOnly BirthDate
    ) : DomainEvent;

public sealed record RefreshTokenUpdatedDomainEvent(Guid UserId) : DomainEvent;

public sealed record UserImageUrlUpdatedDomainEvent(Guid UserId, Uri ImageUrl) : DomainEvent;
