using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Core.Interfaces;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace Common.EventBus.Contracts;

public abstract partial class EventHandlerBase<TEvent> : IEventHandler<TEvent> where TEvent : class, IEvent
{
    public Task Consume(ConsumeContext<TEvent> context) => HandleAsync(context.Message, context.CancellationToken);

    protected abstract Task HandleAsync(TEvent @event, CancellationToken cancellationToken);
}
