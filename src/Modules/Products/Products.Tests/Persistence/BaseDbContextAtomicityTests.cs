using Bogus;
using Common.Domain.StronglyTypedIds;
using Common.Tests;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Outbox.Persistence;
using Products.Infrastructure.Persistence;
using Products.Domain.Stores;
using Xunit;

#pragma warning disable CA1707 // Remove the underscores from member name

namespace Products.Tests.Persistence;

[Collection("IntegrationTestCollection")]
public class BaseDbContextAtomicityTests : BaseIntegrationTest
{
    private readonly Faker _faker = new();

    public BaseDbContextAtomicityTests(IntegrationTestWebAppFactory factory) : base(factory)
    {
    }

    [Fact]
    public async Task SaveChangesAsync_WhenAggregateRaisesDomainEvent_CreatesOutboxMessage()
    {
        // Arrange
        using var scope = Factory.Services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<ProductsDbContext>();
        var outboxDb = scope.ServiceProvider.GetRequiredService<OutboxDbContext>();
        var ownerId = new ApplicationUserId(Guid.NewGuid());
        var store = Store.Create(ownerId, _faker.Company.CompanyName(), _faker.Lorem.Sentence(), _faker.Address.FullAddress());

        db.Stores.Add(store);

        // Act
        await db.SaveChangesAsync();

        // Assert - Outbox message should exist for the domain event
        var outboxMessages = await outboxDb.OutboxMessages
            .AsNoTracking()
            .Where(m => !m.IsProcessed)
            .OrderByDescending(m => m.CreatedOn)
            .ToListAsync();

        Assert.NotEmpty(outboxMessages);
        Assert.Contains(outboxMessages, m => m.Event != null);
    }

    [Fact]
    public async Task SaveChangesAsync_WhenAggregateRaisesDomainEvent_CreatesAuditLogEvent()
    {
        // Arrange
        using var scope = Factory.Services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<ProductsDbContext>();
        var ownerId = new ApplicationUserId(Guid.NewGuid());
        var store = Store.Create(ownerId, _faker.Company.CompanyName(), _faker.Lorem.Sentence(), _faker.Address.FullAddress());

        db.Stores.Add(store);

        // Act
        await db.SaveChangesAsync();

        // Assert - AuditLog event should exist for the domain event
        var auditLogEntries = await db.AuditLog
            .AsNoTracking()
            .Where(e => e.AggregateId == store.Id.Value)
            .ToListAsync();

        Assert.NotEmpty(auditLogEntries);
        Assert.All(auditLogEntries, e =>
        {
            Assert.Equal(nameof(Store), e.AggregateType);
            Assert.NotNull(e.Event);
            Assert.True(e.Version > 0);
        });
    }

    [Fact]
    public async Task SaveChangesAsync_WhenAggregateRaisesDomainEvent_CreatesOutboxAndAuditLogAtomically()
    {
        // Arrange
        using var scope = Factory.Services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<ProductsDbContext>();
        var outboxDb = scope.ServiceProvider.GetRequiredService<OutboxDbContext>();
        var ownerId = new ApplicationUserId(Guid.NewGuid());
        var store = Store.Create(ownerId, _faker.Company.CompanyName(), _faker.Lorem.Sentence(), _faker.Address.FullAddress());

        var outboxCountBefore = await outboxDb.OutboxMessages.AsNoTracking().CountAsync();
        var auditLogCountBefore = await db.AuditLog.AsNoTracking().CountAsync();

        db.Stores.Add(store);

        // Act
        await db.SaveChangesAsync();

        // Assert - Both outbox message AND event store event should have been created
        var outboxCountAfter = await outboxDb.OutboxMessages.AsNoTracking().CountAsync();
        var auditLogCountAfter = await db.AuditLog.AsNoTracking().CountAsync();

        // Store.Create raises exactly 1 domain event (V1StoreCreatedDomainEvent)
        Assert.Equal(outboxCountBefore + 1, outboxCountAfter);
        Assert.Equal(auditLogCountBefore + 1, auditLogCountAfter);
    }

    [Fact]
    public async Task SaveChangesAsync_WhenNoEventsRaised_DoesNotCreateOutboxOrAuditLogRecords()
    {
        // Arrange
        using var scope = Factory.Services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<ProductsDbContext>();
        var outboxDb = scope.ServiceProvider.GetRequiredService<OutboxDbContext>();

        // Count existing records before
        var outboxCountBefore = await outboxDb.OutboxMessages.AsNoTracking().CountAsync();
        var auditLogCountBefore = await db.AuditLog.AsNoTracking().CountAsync();

        // Save with no changes (no events raised)
        await db.SaveChangesAsync();

        // Assert - no new records created
        var outboxCountAfter = await outboxDb.OutboxMessages.AsNoTracking().CountAsync();
        var auditLogCountAfter = await db.AuditLog.AsNoTracking().CountAsync();

        Assert.Equal(outboxCountBefore, outboxCountAfter);
        Assert.Equal(auditLogCountBefore, auditLogCountAfter);
    }

    [Fact]
    public async Task SaveChangesAsync_WhenMultipleEventsRaised_CreatesMatchingOutboxAndAuditLogRecords()
    {
        // Arrange - Store.Create raises 1 event, then Update raises more
        using var scope = Factory.Services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<ProductsDbContext>();
        var ownerId = new ApplicationUserId(Guid.NewGuid());
        var store = Store.Create(ownerId, _faker.Company.CompanyName(), _faker.Lorem.Sentence(), _faker.Address.FullAddress());

        db.Stores.Add(store);
        await db.SaveChangesAsync();

        // Now update the store to raise more events
        var newName = _faker.Company.CompanyName();
        var newDescription = _faker.Lorem.Sentence();
        store.Update(newName, newDescription, null);

        // Act
        await db.SaveChangesAsync();

        // Assert - All events should be in event store
        var auditLogEntries = await db.AuditLog
            .AsNoTracking()
            .Where(e => e.AggregateId == store.Id.Value)
            .ToListAsync();

        // Store.Create = 1 event, Update with name + description = 2 events = total 3
        Assert.Equal(3, auditLogEntries.Count);
    }
}
