using System.Linq.Expressions;
using Common.Application.BackgroundJobs;
using Common.Application.Caching;
using Common.Domain.StronglyTypedIds;
using Common.IntegrationEvents;
using MassTransit;
using Microsoft.Extensions.Logging;
using Notifications.Application;
using Notifications.Application.IntegrationEventHandlers;
using NSubstitute;
using Xunit;

namespace Notifications.Tests.IntegrationEventHandlers;

public class UserRegisteredIntegrationEventHandlerTests
{
    private readonly IBackgroundJobs _backgroundJobs;
    private readonly ICacheService _cache;
    private readonly UserRegisteredIntegrationEventHandler _handler;
    private readonly ILogger<UserRegisteredIntegrationEventHandler> _logger;

    public UserRegisteredIntegrationEventHandlerTests()
    {
        _backgroundJobs = Substitute.For<IBackgroundJobs>();
        _logger = Substitute.For<ILogger<UserRegisteredIntegrationEventHandler>>();
        _cache = Substitute.For<ICacheService>();
        _handler = new UserRegisteredIntegrationEventHandler(_backgroundJobs, _logger, _cache);
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

        // Compile and invoke the captured expression against a mock ISmsService
        // to verify that SendWelcomeSmsAsync (or SendWelcomeAsync) is called with correct parameters.
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

        var cacheKey = $"processed_msg:{messageId}";

        // First call: cache returns null (not yet processed)
        _cache.GetAsync<bool?>(cacheKey, CancellationToken.None).Returns((bool?)null);

        // Act — first delivery
        await _handler.Consume(firstContext);

        // Simulate cache now holding true after SetAsync was called on the first delivery
        _cache.GetAsync<bool?>(cacheKey, CancellationToken.None).Returns((bool?)true);

        // Act — second delivery (re-delivery of same message)
        await _handler.Consume(secondContext);

        // Assert — background job enqueued exactly once despite two Consume calls
        _backgroundJobs.Received(1).Enqueue(Arg.Any<Expression<Func<ISmsService, Task>>>());

        // SetAsync was called once (on first delivery only)
        await _cache.Received(1).SetAsync(
            cacheKey,
            true,
            absoluteExpirationRelativeToNow: TimeSpan.FromDays(1),
            cancellationToken: CancellationToken.None);
    }
}
