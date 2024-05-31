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
    private TAggregate _aggregate = new();
    private Result<TAggregate>? _aggregateResult;
    private Result? _plainResult;
    private Error? _error;

    public AggregateTests<TAggregate, TId> Given(Func<TAggregate> func)
    {
        _aggregate = func();
        return this;
    }

    public AggregateTests<TAggregate, TId> Given(params DomainEvent[] events)
    {
        _aggregate.LoadFromHistory(events);
        return this;
    }

    public AggregateTests<TAggregate, TId> When(Action<TAggregate> action)
    {
        action(_aggregate);
        return this;
    }

    public AggregateTests<TAggregate, TId> When(Func<TAggregate, Result<TAggregate>> func)
    {
        var result = func(_aggregate);
        _aggregateResult = result;
        _error = result.Error;
        return this;
    }

    public AggregateTests<TAggregate, TId> When(Func<TAggregate, Result> func)
    {
        var result = func(_aggregate);
        _plainResult = result;
        _error = result.Error;
        return this;
    }

    public AggregateTests<TAggregate, TId> Then<TDomainEvent>(params Action<TDomainEvent>[] assertions)
        where TDomainEvent : DomainEvent
    {
        var events = _aggregate
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

    public AggregateTests<TAggregate, TId> ThenError<TError>(params Action<TError>[] assertions)
        where TError : Error
    {
        // either _aggregateResult or _plainResult should be set
        if(_aggregateResult is null && _plainResult is null)
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
        _error.Should().BeOfType<TError>();

        if (assertions.Length > 0)
        {
            assertions
                .Should()
                .AllSatisfy(assert => assert((TError)_error!));
        }

        return this;
    }
}
