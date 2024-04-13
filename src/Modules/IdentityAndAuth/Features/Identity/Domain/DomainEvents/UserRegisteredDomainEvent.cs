using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Core.Contracts;

namespace IdentityAndAuth.Features.Identity.Domain.DomainEvents;
public sealed record UserRegisteredDomainEvent(
    Guid UserId,
    string Name,
    string LastName,
    string PhoneNumber,
    string NationalIdentityNumber,
    DateOnly BirthDate
    ) : DomainEvent;
