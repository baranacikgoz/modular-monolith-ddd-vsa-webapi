using Common.Application.Options;
using Common.Domain.StronglyTypedIds;
using Common.IntegrationEvents;
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
    private readonly ISmsService _smsService;
    private readonly FusionCache _cache;
    private readonly UserRegisteredIntegrationEventHandler _handler;

    public UserRegisteredIntegrationEventHandlerTests()
    {
        _smsService = Substitute.For<ISmsService>();
        var logger = Substitute.For<ILogger<UserRegisteredIntegrationEventHandler>>();
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
        });
        _handler = new UserRegisteredIntegrationEventHandler(logger, _cache, cachingOptions, _smsService);
    }

    public void Dispose()
    {
        _cache.Dispose();
        GC.SuppressFinalize(this);
    }

    [Fact]
    public async Task HandleAsync_WhenUserRegistered_SendsWelcomeSms()
    {
        var userId = ApplicationUserId.New();
        var @event = new UserRegisteredIntegrationEvent(userId, "John Doe", "1234567890");

        await _handler.HandleAsync(@event, CancellationToken.None);

        await _smsService.Received(1).SendWelcomeAsync(@event.Name, @event.PhoneNumber);
    }

    [Fact]
    public async Task HandleAsync_SameEventIdTwice_ProcessesOnlyOnce()
    {
        var userId = ApplicationUserId.New();
        var @event = new UserRegisteredIntegrationEvent(userId, "Jane Doe", "0987654321");

        await _handler.HandleAsync(@event, CancellationToken.None);
        await _handler.HandleAsync(@event, CancellationToken.None);

        await _smsService.Received(1).SendWelcomeAsync(@event.Name, @event.PhoneNumber);
    }
}
