using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Core.Contracts;

namespace Common.IntegrationEvents;

public sealed record UserRegisteredIntegrationEvent(
    Guid UserId,
    string Name,
    string PhoneNumber
    ) : DomainEvent;
