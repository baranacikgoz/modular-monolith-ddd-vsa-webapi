using Common.Application.Persistence.Outbox;
using Common.Domain.StronglyTypedIds;
using Common.IntegrationEvents;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Outbox.Persistence;
using Xunit;

#pragma warning disable CA1707

namespace Outbox.Tests;

public sealed class OutboxBackoffTests : IClassFixture<OutboxTestWebAppFactory>
{
    private readonly OutboxTestWebAppFactory _factory;

    public OutboxBackoffTests(OutboxTestWebAppFactory factory)
    {
        _factory = factory;
        _ = factory.CreateClient(); // eager — IClassFixture rule
    }

    [Fact]
    public async Task Message_WithFutureNextRetryAt_IsSkippedByProcessor()
    {
        int messageId;
        var now = DateTimeOffset.UtcNow;

        await using (var scope = _factory.Services.CreateAsyncScope())
        {
            var db = scope.ServiceProvider.GetRequiredService<OutboxDbContext>();
            var message = OutboxMessage.Create(now, new UserRegisteredIntegrationEvent(
                ApplicationUserId.New(), "Test User", "+905550000001"));
            message.IncrementRetryCount(now, TimeSpan.FromMinutes(10)); // NextRetryAt far in future
            db.OutboxMessages.Add(message);
            await db.SaveChangesAsync();
            messageId = message.Id;
        }

        // 7 poll cycles at 100ms — processor must not touch message in backoff window
        await Task.Delay(700);

        await using (var scope = _factory.Services.CreateAsyncScope())
        {
            var db = scope.ServiceProvider.GetRequiredService<OutboxDbContext>();
            var message = await db.OutboxMessages.AsNoTracking().SingleAsync(m => m.Id == messageId);

            Assert.False(message.IsProcessed);
            Assert.Equal(1, message.RetryCount); // processor did not increment further
            Assert.True(message.NextRetryAt > DateTimeOffset.UtcNow);
        }
    }

    [Fact]
    public async Task Message_WithNullNextRetryAt_IsPickedUpByProcessor()
    {
        int messageId;
        var now = DateTimeOffset.UtcNow;

        await using (var scope = _factory.Services.CreateAsyncScope())
        {
            var db = scope.ServiceProvider.GetRequiredService<OutboxDbContext>();
            var message = OutboxMessage.Create(now, new UserRegisteredIntegrationEvent(
                ApplicationUserId.New(), "Test User", "+905550000002"));
            // NextRetryAt is null — processor must pick it up on the next poll
            db.OutboxMessages.Add(message);
            await db.SaveChangesAsync();
            messageId = message.Id;
        }

        // Give processor 2s — at 100ms poll, it will attempt the message many times
        await Task.Delay(2000);

        await using (var scope = _factory.Services.CreateAsyncScope())
        {
            var db = scope.ServiceProvider.GetRequiredService<OutboxDbContext>();
            var message = await db.OutboxMessages.AsNoTracking().SingleAsync(m => m.Id == messageId);

            // Either published successfully or retried (both prove the processor picked it up)
            Assert.True(message.IsProcessed || message.RetryCount > 0);
        }
    }
}
