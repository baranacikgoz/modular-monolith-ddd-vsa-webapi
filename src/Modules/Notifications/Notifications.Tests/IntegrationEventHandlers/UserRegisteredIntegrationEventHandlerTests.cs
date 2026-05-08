using System.Linq.Expressions;
using Common.Application.BackgroundJobs;
using Common.Application.Options;
using Common.Domain.StronglyTypedIds;
using Common.IntegrationEvents;
using MassTransit;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Notifications.Application;
using Notifications.Application.IntegrationEventHandlers;
using NSubstitute;
using Xunit;
using ZiggyCreatures.Caching.Fusion;

namespace Notifications.Tests.IntegrationEventHandlers;

public sealed class UserRegisteredIntegrationEventHandlerTests : IDisposable
{
    private readonly IBackgroundJobs _backgroundJobs;
    private readonly FusionCache _cache;
    private readonly UserRegisteredIntegrationEventHandler _handler;
    private readonly ILogger<UserRegisteredIntegrationEventHandler> _logger;

    public UserRegisteredIntegrationEventHandlerTests()
    {
        _backgroundJobs = Substitute.For<IBackgroundJobs>();
        _logger = Substitute.For<ILogger<UserRegisteredIntegrationEventHandler>>();
        _cache = new FusionCache(new FusionCacheOptions());
        _handler = new UserRegisteredIntegrationEventHandler(_backgroundJobs, _logger, _cache, Options.Create(new CachingOptions
        {
            EntryDefaults = new CachingEntryDefaults
            {
                Duration = TimeSpan.FromMinutes(5),
                FailSafeMaxDuration = TimeSpan.FromHours(2),
                FailSafeThrottleDuration = TimeSpan.FromSeconds(30),
                FactorySoftTimeout = TimeSpan.FromMilliseconds(100),
                FactoryHardTimeout = TimeSpan.FromMilliseconds(1500),
            },
            IdempotencyKeyDuration = TimeSpan.FromDays(1),
        }));
    }

    public void Dispose()
    {
        _cache.Dispose();
        GC.SuppressFinalize(this);
    }

    [Fact]
    public async Task HandleAsyncShouldEnqueueWelcomeSmsWhenUserRegistered()
    {
        // Arrange
        var userId = ApplicationUserId.New();
        var @event = new UserRegisteredIntegrationEvent(
            userId,
            "John Doe",
            "1234567890");

        var context = Substitute.For<ConsumeContext<UserRegisteredIntegrationEvent>>();
        context.Message.Returns(@event);
        context.MessageId.Returns((Guid?)null);

        Expression<Func<ISmsService, Task>>? capturedExpression = null;
        _backgroundJobs.When(x => x.Enqueue(Arg.Any<Expression<Func<ISmsService, Task>>>()))
            .Do(ci => capturedExpression = ci.Arg<Expression<Func<ISmsService, Task>>>());

        // Act
        await _handler.Consume(context);

        // Assert
        _backgroundJobs.Received(1).Enqueue(Arg.Any<Expression<Func<ISmsService, Task>>>());

        Assert.NotNull(capturedExpression);

        var smsServiceMock = Substitute.For<ISmsService>();
        var func = capturedExpression.Compile();
        await func(smsServiceMock);

        await smsServiceMock.Received(1).SendWelcomeAsync(@event.Name, @event.PhoneNumber);
    }

    [Fact]
    public async Task Handle_SameMessageIdTwice_ProcessesOnlyOnce()
    {
        // Arrange
        var messageId = Guid.NewGuid();
        var userId = ApplicationUserId.New();
        var @event = new UserRegisteredIntegrationEvent(userId, "Jane Doe", "0987654321");

        var firstContext = Substitute.For<ConsumeContext<UserRegisteredIntegrationEvent>>();
        firstContext.Message.Returns(@event);
        firstContext.MessageId.Returns(messageId);
        firstContext.CancellationToken.Returns(CancellationToken.None);

        var secondContext = Substitute.For<ConsumeContext<UserRegisteredIntegrationEvent>>();
        secondContext.Message.Returns(@event);
        secondContext.MessageId.Returns(messageId);
        secondContext.CancellationToken.Returns(CancellationToken.None);

        // Act — first delivery: real in-memory FusionCache has no entry, factory runs
        await _handler.Consume(firstContext);

        // Act — second delivery: same messageId, cache hit, factory is skipped
        await _handler.Consume(secondContext);

        // Assert — background job enqueued exactly once despite two Consume calls
        _backgroundJobs.Received(1).Enqueue(Arg.Any<Expression<Func<ISmsService, Task>>>());
    }
}
