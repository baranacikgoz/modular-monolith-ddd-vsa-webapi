using Common.Domain.Aggregates;
using Common.Domain.Events;
using Common.Domain.ResultMonad;
using Common.Domain.StronglyTypedIds;
using FluentAssertions;
using Xunit;

namespace UnitTests.Common;

public abstract class AggregateTests<TAggregate, TId>
    where TAggregate : AggregateRoot<TId>, new()
    where TId : IStronglyTypedId
{
#pragma warning disable CA1051 // Do not declare visible instance fields
    protected TAggregate Aggregate = new();
#pragma warning restore CA1051 // Do not declare visible instance fields
    private object? _objectUnderTheTestAlongWithAggregate;
    private Result<TAggregate>? _aggregateResult;
    private Result? _plainResult;
    private Error? _error;

    public AggregateTests<TAggregate, TId> Given(Func<TAggregate> func)
    {
        Aggregate = func();
        return this;
    }

    public AggregateTests<TAggregate, TId> Given(params DomainEvent[] events)
    {
        Aggregate.LoadFromHistory(events);
        return this;
    }

    public AggregateTests<TAggregate, TId> When(Action<TAggregate> action)
    {
        action(Aggregate);
        return this;
    }

    public AggregateTests<TAggregate, TId> When(Func<TAggregate, Result<TAggregate>> func)
    {
        var result = func(Aggregate);
        _aggregateResult = result;
        _error = result.Error;
        return this;
    }

    public AggregateTests<TAggregate, TId> When(Func<TAggregate, Result> func)
    {
        var result = func(Aggregate);
        _plainResult = result;
        _error = result.Error;
        return this;
    }

    public AggregateTests<TAggregate, TId> When(Func<TAggregate, object, Result> func)
    {
        _objectUnderTheTestAlongWithAggregate.Should().NotBeNull();
        var result = func(Aggregate, _objectUnderTheTestAlongWithAggregate!);
        _plainResult = result;
        _error = result.Error;
        return this;
    }

    public AggregateTests<TAggregate, TId> When(Func<TAggregate, object> func)
    {
        var result = func(Aggregate);
        _objectUnderTheTestAlongWithAggregate = result;
        return this;
    }

    public AggregateTests<TAggregate, TId> Then(params Action<TAggregate>[] assertions)
    {
        if (assertions.Length > 0)
        {
            assertions
                .Should()
                .AllSatisfy(assert => assert(Aggregate));
        }

        return this;
    }

    public AggregateTests<TAggregate, TId> Then<TDomainEvent>(params Action<TDomainEvent>[] assertions)
        where TDomainEvent : DomainEvent
    {
        var events = Aggregate
                    .Events
                    .OfType<TDomainEvent>()
                    .ToList();

        events.Should().NotBeNull();
        events.Should().NotBeEmpty();
        events.Should().ContainSingle();

        var @event = events[0];

        if (assertions.Length > 0)
        {
            assertions
                .Should()
                .AllSatisfy(assert => assert(@event));
        }

        return this;
    }

    public AggregateTests<TAggregate, TId> Then<TDomainEvent>(params Action<TAggregate, TDomainEvent>[] assertions)
        where TDomainEvent : DomainEvent
    {
        var events = Aggregate
                    .Events
                    .OfType<TDomainEvent>()
                    .ToList();

        events.Should().NotBeNull();
        events.Should().NotBeEmpty();
        events.Should().ContainSingle();

        var @event = events[0];

        if (assertions.Length > 0)
        {
            assertions
                .Should()
                .AllSatisfy(assert => assert(Aggregate, @event));
        }

        return this;
    }

    public AggregateTests<TAggregate, TId> Then<TDomainEvent>(params Action<TAggregate, object, TDomainEvent>[] assertions)
        where TDomainEvent : DomainEvent
    {
        var events = Aggregate
                    .Events
                    .OfType<TDomainEvent>()
                    .ToList();

        events.Should().NotBeNull();
        events.Should().NotBeEmpty();
        events.Should().ContainSingle();

        var @event = events[0];

        _objectUnderTheTestAlongWithAggregate.Should().NotBeNull();

        if (assertions.Length > 0)
        {
            assertions
                .Should()
                .AllSatisfy(assert => assert(Aggregate, _objectUnderTheTestAlongWithAggregate!, @event));
        }

        return this;
    }

    public AggregateTests<TAggregate, TId> ThenNoEventsOfType<TDomainEvent>()
        where TDomainEvent : DomainEvent
    {
        var events = Aggregate
                    .Events
                    .OfType<TDomainEvent>()
                    .ToList();

        events.Should().NotBeNull();
        events.Should().BeEmpty();

        return this;
    }

    public AggregateTests<TAggregate, TId> ThenError(params Action<Error>[] assertions)
    {
        // either _aggregateResult or _plainResult should be set
        if (_aggregateResult is null && _plainResult is null)
        {
            Assert.Fail("Either _aggregateResult or _plainResult should have been set");
        }

        if (_aggregateResult is not null && _plainResult is not null)
        {
            Assert.Fail("Both _aggregateResult and _plainResult should not have been set at the same time");
        }

        if (_aggregateResult is not null)
        {
            _error = _aggregateResult.Error;
        }
        else
        {
            _error = _plainResult!.Error;
        }

        _error.Should().NotBeNull();

        if (assertions.Length > 0)
        {
            assertions
                .Should()
                .AllSatisfy(assert => assert(_error!));
        }

        return this;
    }
}
