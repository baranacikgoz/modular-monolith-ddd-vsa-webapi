using System.Net.Http.Headers;
using System.Text.Json;
using Common.Domain.Entities;
using Common.Domain.StronglyTypedIds;
using Common.Infrastructure.Persistence.ValueConverters;
using Common.Tests;
using Microsoft.Extensions.DependencyInjection;
using Products.Application.Persistence;
using Products.Domain.Stores;
using Products.Domain.Stores.DomainEvents.v1;
using Products.Domain.Stores.DomainEvents.v2;
using Xunit;

namespace Products.Tests.Stores;

/// <summary>
///     A shipped event is a frozen contract: the AuditLog keeps rows written by every version forever, so the
///     version a command raises today and the versions still readable are separate facts.
/// </summary>
[Collection("IntegrationTestCollection")]
public class StoreAddressEventVersioningTests : BaseIntegrationTest
{
    private const string StoreIdText = "0198d3a4-5b7e-7c3a-9d41-6f2e8b1a4c05";

    // A V1 row exactly as the AuditLog stores it for an address change made before V2 existed.
    private const string ShippedV1Row =
        """{"eventTypeFullName":"Products.Domain.Stores.DomainEvents.v1.V1StoreAddressUpdatedDomainEvent","eventData":{"storeId":"0198d3a4-5b7e-7c3a-9d41-6f2e8b1a4c05","address":"Old Road 1","id":"0198d3a4-5b7e-7c3a-9d41-6f2e8b1a4c06","version":3,"createdOn":"2026-09-01T10:00:00+00:00"}}""";

    public StoreAddressEventVersioningTests(IntegrationTestWebAppFactory factory) : base(factory)
    {
    }

    [Fact]
    public void AuditLogConverter_ShippedV1Row_StillDeserializesToTheV1Event()
    {
        var converter = new DomainEventConverter();

        var restored = converter.ConvertFromProvider(ShippedV1Row);

        var @event = Assert.IsType<V1StoreAddressUpdatedDomainEvent>(restored);
        Assert.Equal(new StoreId(Guid.Parse(StoreIdText)), @event.StoreId);
        Assert.Equal("Old Road 1", @event.Address);
        Assert.Equal(3, @event.Version);
    }

    [Fact]
    public void AuditLogConverter_V2Event_RoundTripsWithThePreviousAddress()
    {
        var converter = new DomainEventConverter();
        var original = new V2StoreAddressUpdatedDomainEvent(new StoreId(Guid.Parse(StoreIdText)), "New Road 2", "Old Road 1");

        var restored = converter.ConvertFromProvider(converter.ConvertToProvider(original));

        var @event = Assert.IsType<V2StoreAddressUpdatedDomainEvent>(restored);
        Assert.Equal("New Road 2", @event.Address);
        Assert.Equal("Old Road 1", @event.PreviousAddress);
    }

    [Fact]
    public async Task AuditLog_HistoryWrittenByBothVersions_ReadsBackEachInItsOwnType()
    {
        // Arrange: a store whose history holds a V1 row written long ago and a V2 row written by today's command.
        var store = Store.Create(new ApplicationUserId(Guid.NewGuid()), "Store", "Description", "Old Road 1");
        store.ClearEvents();
        store.Update(null, null, "New Road 2");

        using var scope = Factory.Services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<IProductsDbContext>();
        db.Stores.Add(store);
        db.AuditLog.Add(AuditLogEntry.Create(nameof(Store), store.Id.Value, 1, new V1StoreAddressUpdatedDomainEvent(store.Id, "Older Road 0")));
        await db.SaveChangesAsync();

        var client = Factory.CreateClient();
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("TestScheme");

        // Act
        var response = await client.GetAsync(new Uri($"/v1/stores/{store.Id}/audit-log?PageNumber=1&PageSize=10", UriKind.Relative));

        // Assert: newest first, the V2 row carries the previous address and the V1 row is still served as the V1 type.
        response.EnsureSuccessStatusCode();
        using var json = JsonDocument.Parse(await response.Content.ReadAsStringAsync());
        var rows = json.RootElement.GetProperty("data").EnumerateArray().ToList();
        Assert.Equal(2, rows.Count);
        Assert.Equal(nameof(V2StoreAddressUpdatedDomainEvent), rows[0].GetProperty("eventType").GetString());
        Assert.Equal("Old Road 1", rows[0].GetProperty("payload").GetProperty("previousAddress").GetString());
        Assert.Equal(nameof(V1StoreAddressUpdatedDomainEvent), rows[1].GetProperty("eventType").GetString());
        Assert.Equal("Older Road 0", rows[1].GetProperty("payload").GetProperty("address").GetString());
    }
}
