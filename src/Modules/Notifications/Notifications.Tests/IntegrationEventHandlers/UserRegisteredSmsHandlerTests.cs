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

public sealed class UserRegisteredSmsHandlerTests : IDisposable
{
    private readonly ISmsService _smsService;
    private readonly FusionCache _cache;
    private readonly UserRegisteredSmsHandler _handler;

    public UserRegisteredSmsHandlerTests()
    {
        _smsService = Substitute.For<ISmsService>();
        var logger = Substitute.For<ILogger<UserRegisteredSmsHandler>>();
        _cache = new FusionCache(new FusionCacheOptions());
        var cachingOptions = Options.Create(new CachingOptions
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
            IdempotencyL1Duration = TimeSpan.FromHours(1),
        });
        _handler = new UserRegisteredSmsHandler(logger, _cache, cachingOptions, _smsService);
    }

    public void Dispose()
    {
        _cache.Dispose();
        GC.SuppressFinalize(this);
    }

    private static ConsumeContext<UserRegisteredIntegrationEvent> MakeContext(UserRegisteredIntegrationEvent @event)
    {
        var context = Substitute.For<ConsumeContext<UserRegisteredIntegrationEvent>>();
        context.Message.Returns(@event);
        context.CancellationToken.Returns(CancellationToken.None);
        return context;
    }

    [Fact]
    public async Task Consume_WhenUserRegistered_SendsWelcomeSms()
    {
        var userId = ApplicationUserId.New();
        var @event = new UserRegisteredIntegrationEvent(userId, "John Doe", "1234567890");

        await _handler.Consume(MakeContext(@event));

        await _smsService.Received(1).SendWelcomeAsync(@event.FullName, @event.PhoneNumber);
    }

    [Fact]
    public async Task Consume_SameEventIdTwice_ProcessesOnlyOnce()
    {
        var userId = ApplicationUserId.New();
        var @event = new UserRegisteredIntegrationEvent(userId, "Jane Doe", "0987654321");

        await _handler.Consume(MakeContext(@event));
        await _handler.Consume(MakeContext(@event));

        await _smsService.Received(1).SendWelcomeAsync(@event.FullName, @event.PhoneNumber);
    }
}
