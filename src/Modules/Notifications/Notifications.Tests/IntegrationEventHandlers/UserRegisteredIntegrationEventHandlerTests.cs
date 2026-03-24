using System.Linq.Expressions;
using Common.Application.BackgroundJobs;
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
    private readonly UserRegisteredIntegrationEventHandler _handler;
    private readonly ILogger<UserRegisteredIntegrationEventHandler> _logger;

    public UserRegisteredIntegrationEventHandlerTests()
    {
        _backgroundJobs = Substitute.For<IBackgroundJobs>();
        _logger = Substitute.For<ILogger<UserRegisteredIntegrationEventHandler>>();
        _handler = new UserRegisteredIntegrationEventHandler(_backgroundJobs, _logger);
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
}
