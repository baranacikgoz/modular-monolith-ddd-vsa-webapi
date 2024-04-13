using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Core.Contracts;

namespace Common.IntegrationEvents;
public sealed record StoreCreatedIntegrationEvent(Guid StoreId, Guid OwnerId) : DomainEvent;

public sealed record ProductCreatedIntegrationEvent(Guid ProductId, Guid StoreId, string Name) : DomainEvent;
