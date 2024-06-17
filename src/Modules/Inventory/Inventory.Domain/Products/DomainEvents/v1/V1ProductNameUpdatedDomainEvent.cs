using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Domain.Events;

namespace Inventory.Domain.Products.DomainEvents.v1;
public sealed record V1ProductNameUpdatedDomainEvent(ProductId Id, string Name) : DomainEvent;
