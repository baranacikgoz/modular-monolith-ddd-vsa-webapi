
using Common.Application.BackgroundJobs;
using Common.IntegrationEvents;
using Common.Domain.StronglyTypedIds;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using NSubstitute;
using Notifications.Application;
using Notifications.Application.IntegrationEventHandlers;
using Xunit;
using System.Linq.Expressions;
using MassTransit;

namespace Notifications.Tests.IntegrationEventHandlers;

public class UserRegisteredIntegrationEventHandlerTests
{
    private readonly IBackgroundJobs _backgroundJobs;
    private readonly ILogger<UserRegisteredIntegrationEventHandler> _logger;
    private readonly UserRegisteredIntegrationEventHandler _handler;

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

        // Act
        await _handler.Consume(context);

        // Assert
        _backgroundJobs.Received(1).Enqueue(Arg.Any<Expression<Func<ISmsService, Task>>>());
    }
}
