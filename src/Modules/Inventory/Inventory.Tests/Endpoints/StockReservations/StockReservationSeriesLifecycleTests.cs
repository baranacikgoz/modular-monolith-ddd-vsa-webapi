using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using Bogus;
using Common.Application.Options;
using Common.Infrastructure.Persistence.Outbox;
using Common.IntegrationEvents;
using Common.Tests;
using Inventory.Application.Persistence;
using Inventory.Domain.StockReservations;
using Inventory.Infrastructure.StockReservations;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Xunit;
using ReserveSeriesRequest = Inventory.Endpoints.StockReservations.v1.ReserveSeries.Request;
using ReserveSeriesResponse = Inventory.Endpoints.StockReservations.v1.ReserveSeries.Response;

namespace Inventory.Tests.Endpoints.StockReservations;

/// <summary>
///     Scenario-builder integration suite for the full StockReservation saga (ReserveSeries → webhook
///     Commit → Release/Expire), mirroring Appointments' CreateManualBookingTests.cs shape: private seed
///     helpers building a real multi-aggregate scenario (via the real Products HTTP endpoints, since
///     Inventory.Tests deliberately does not reference Products.Domain - see module-boundary rule), then
///     exercising the endpoint under test, then asserting domain-invariant outcomes.
/// </summary>
[Collection("IntegrationTestCollection")]
public class StockReservationSeriesLifecycleTests : BaseIntegrationTest
{
    private readonly Faker _faker = new();

    public StockReservationSeriesLifecycleTests(IntegrationTestWebAppFactory factory) : base(factory)
    {
    }

    // ── Seed helpers (real Products HTTP endpoints - Inventory.Tests has no reference to Products.Domain) ──

    private async Task<DefaultIdType> SeedProductAsync(HttpClient client, int quantity = 100, decimal price = 9.99m)
    {
        var storeResponse = await client.PostAsJsonAsync(
            new Uri("/v1/stores/my", UriKind.Relative),
            new Products.Endpoints.Stores.v1.My.Create.Request(
                _faker.Company.CompanyName(), _faker.Lorem.Sentence(), _faker.Address.FullAddress()));
        Assert.True(storeResponse.IsSuccessStatusCode, await storeResponse.Content.ReadAsStringAsync());
        var store = await storeResponse.Content.ReadFromJsonAsync<Products.Endpoints.Stores.v1.My.Create.Response>(JsonSerializerOptions);

        var templateResponse = await client.PostAsJsonAsync(
            new Uri("/v1/product-templates", UriKind.Relative),
            new Products.Endpoints.ProductTemplates.v1.Create.Request
            {
                Brand = _faker.Company.CompanyName(), Model = _faker.Commerce.ProductName(), Color = _faker.Commerce.Color()
            });
        Assert.True(templateResponse.IsSuccessStatusCode, await templateResponse.Content.ReadAsStringAsync());
        var template = await templateResponse.Content.ReadFromJsonAsync<Products.Endpoints.ProductTemplates.v1.Create.Response>(JsonSerializerOptions);

        // Built as a plain anonymous object (raw Guid, not the typed ProductTemplateId) because
        // StronglyTypedIdReadOnlyJsonConverter.Write throws NotImplementedException - it only supports
        // reading a strongly-typed id back out of a response, never serializing one into a request. Same
        // workaround MyAddProductTests uses for this exact property.
        var addProductJson = JsonSerializer.Serialize(new
        {
            productTemplateId = template!.Id.Value,
            name = _faker.Commerce.ProductName(),
            description = _faker.Lorem.Sentence(),
            quantity,
            price
        });
        using var addProductContent = new StringContent(addProductJson, Encoding.UTF8, "application/json");
        var productResponse = await client.PostAsync(
            new Uri($"/v1/stores/{store!.Id}/products", UriKind.Relative), addProductContent);
        Assert.True(productResponse.IsSuccessStatusCode, await productResponse.Content.ReadAsStringAsync());
        var product = await productResponse.Content.ReadFromJsonAsync<Products.Endpoints.Stores.v1.AddProduct.Response>(JsonSerializerOptions);

        return product!.Id.Value;
    }

    private async Task<StockReservation> SeedReservationAsync(DefaultIdType productId, int quantity, DateTimeOffset deadline)
    {
        using var scope = Factory.Services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<IInventoryDbContext>();
        var reservation = StockReservation.Reserve(productId, quantity, deadline);
        db.StockReservations.Add(reservation);
        await db.SaveChangesAsync();
        return reservation;
    }

    private async Task<StockReservation> SeedCommittedReservationAsync(
        DefaultIdType productId, int quantity, DateTimeOffset deadline, string providerReference)
    {
        using var scope = Factory.Services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<IInventoryDbContext>();
        var reservation = StockReservation.Reserve(productId, quantity, deadline);
        reservation.Commit(providerReference, deadline.AddMinutes(-1));
        db.StockReservations.Add(reservation);
        await db.SaveChangesAsync();
        return reservation;
    }

    private static HttpClient AuthedClient(IntegrationTestFactory factory)
    {
        var client = factory.CreateClient();
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("TestScheme");
        return client;
    }

    private async Task<string> WebhookSecretAsync()
    {
        using var scope = Factory.Services.CreateScope();
        return scope.ServiceProvider.GetRequiredService<IOptions<InventoryOptions>>().Value.WebhookSharedSecret;
    }

    private static string ComputeSignature(string rawBody, string secret)
    {
        using var hmac = new HMACSHA256(Encoding.UTF8.GetBytes(secret));
        return Convert.ToHexString(hmac.ComputeHash(Encoding.UTF8.GetBytes(rawBody)));
    }

    private async Task<HttpResponseMessage> SendWebhookAsync(
        HttpClient client, StockReservationId reservationId, string providerReference, string? signatureOverride = null)
    {
        var rawBody = $$"""{"ReservationId":"{{reservationId.Value}}","ProviderReference":"{{providerReference}}"}""";
        var secret = await WebhookSecretAsync();
        var signature = signatureOverride ?? ComputeSignature(rawBody, secret);

        using var request = new HttpRequestMessage(HttpMethod.Post, new Uri("/v1/stock-reservations/webhooks/warehouse", UriKind.Relative))
        {
            Content = new StringContent(rawBody, Encoding.UTF8, "application/json")
        };
        request.Headers.Add("X-Warehouse-Signature", signature);

        return await client.SendAsync(request);
    }

    private async Task<StockReservation?> FindReservationAsync(StockReservationId id)
    {
        using var scope = Factory.Services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<IInventoryDbContext>();
        return await db.StockReservations.AsNoTracking().SingleOrDefaultAsync(r => r.Id == id);
    }

    // ── Happy path / recurrence generation (technique 8) + sequence-dependent field (technique 9) ──

    [Fact]
    public async Task ReserveSeries_HappyPath_FirstOccurrenceGetsSafetyBufferOthersDoNot()
    {
        var client = AuthedClient(Factory);
        var productId = await SeedProductAsync(client);

        using (var scope = Factory.Services.CreateScope())
        {
            var options = scope.ServiceProvider.GetRequiredService<IOptions<InventoryOptions>>().Value;
            var body = BuildBody(productId, quantityPerOccurrence: 10, occurrenceCount: 4);

            var response = await client.PostAsJsonAsync(new Uri("/v1/stock-reservations/series", UriKind.Relative), body);
            Assert.True(response.IsSuccessStatusCode, await response.Content.ReadAsStringAsync());
            var result = await response.Content.ReadFromJsonAsync<ReserveSeriesResponse>(JsonSerializerOptions);

            Assert.Equal(4, result!.Ids.Count);

            var db = scope.ServiceProvider.GetRequiredService<IInventoryDbContext>();
            var reservations = await db.StockReservations
                .Where(r => result.Ids.Contains(r.Id))
                .OrderBy(r => r.ReservationDeadline)
                .ToListAsync();

            Assert.Equal(4, reservations.Count);
            Assert.Equal(10 + options.SafetyBufferQuantity, reservations[0].Quantity);
            Assert.All(reservations.Skip(1), r => Assert.Equal(10, r.Quantity));
        }
    }

    [Fact]
    public async Task ReserveSeries_HappyPath_DeadlinesSpacedByIntervalDays()
    {
        var client = AuthedClient(Factory);
        var productId = await SeedProductAsync(client);
        var first = DateTimeOffset.UtcNow.AddDays(10);
        var body = BuildBody(productId, occurrenceCount: 3, intervalDays: 7, firstDeadline: first);

        var response = await client.PostAsJsonAsync(new Uri("/v1/stock-reservations/series", UriKind.Relative), body);
        Assert.True(response.IsSuccessStatusCode, await response.Content.ReadAsStringAsync());
        var result = await response.Content.ReadFromJsonAsync<ReserveSeriesResponse>(JsonSerializerOptions);

        using var scope = Factory.Services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<IInventoryDbContext>();
        var deadlines = await db.StockReservations
            .Where(r => result!.Ids.Contains(r.Id))
            .Select(r => r.ReservationDeadline)
            .OrderBy(d => d)
            .ToListAsync();

        Assert.Equal(first, deadlines[0], TimeSpan.FromSeconds(1));
        Assert.Equal(first.AddDays(7), deadlines[1], TimeSpan.FromSeconds(1));
        Assert.Equal(first.AddDays(14), deadlines[2], TimeSpan.FromSeconds(1));
    }

    // ── Policy guards ──

    [Fact]
    public async Task ReserveSeries_OccurrenceCountExceedsMax_ReturnsBadRequest()
    {
        using var scope = Factory.Services.CreateScope();
        var maxOccurrences = scope.ServiceProvider.GetRequiredService<IOptions<InventoryOptions>>().Value.MaxOccurrencesPerSeries;

        var client = AuthedClient(Factory);
        var productId = await SeedProductAsync(client);
        var body = BuildBody(productId, occurrenceCount: maxOccurrences + 1);

        var response = await client.PostAsJsonAsync(new Uri("/v1/stock-reservations/series", UriKind.Relative), body);

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task ReserveSeries_FirstDeadlineInPast_ReturnsBadRequest()
    {
        var client = AuthedClient(Factory);
        var productId = await SeedProductAsync(client);
        var body = BuildBody(productId, firstDeadline: DateTimeOffset.UtcNow.AddDays(-1));

        var response = await client.PostAsJsonAsync(new Uri("/v1/stock-reservations/series", UriKind.Relative), body);

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    // ── Deep nested validator (technique 20) ──

    [Fact]
    public async Task ReserveSeries_DuplicateWarehouseIds_ReturnsBadRequest()
    {
        var client = AuthedClient(Factory);
        var productId = await SeedProductAsync(client);
        var duplicateId = Guid.NewGuid();
        var body = BuildBody(productId, warehouses:
        [
            new ReserveSeriesRequest.RequestBody.WarehouseAllocation { WarehouseId = duplicateId, Quantity = 5 },
            new ReserveSeriesRequest.RequestBody.WarehouseAllocation { WarehouseId = duplicateId, Quantity = 5 }
        ]);

        var response = await client.PostAsJsonAsync(new Uri("/v1/stock-reservations/series", UriKind.Relative), body);

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task ReserveSeries_WarehouseQuantitySumMismatch_ReturnsBadRequest()
    {
        var client = AuthedClient(Factory);
        var productId = await SeedProductAsync(client);
        // QuantityPerOccurrence defaults to 10 in BuildBody; warehouse sum here is 3, not 10.
        var body = BuildBody(productId, warehouses:
        [
            new ReserveSeriesRequest.RequestBody.WarehouseAllocation { WarehouseId = Guid.NewGuid(), Quantity = 3 }
        ]);

        var response = await client.PostAsJsonAsync(new Uri("/v1/stock-reservations/series", UriKind.Relative), body);

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task ReserveSeries_EmptyWarehouses_ReturnsBadRequest()
    {
        var client = AuthedClient(Factory);
        var productId = await SeedProductAsync(client);
        var body = BuildBody(productId, warehouses: []);

        var response = await client.PostAsJsonAsync(new Uri("/v1/stock-reservations/series", UriKind.Relative), body);

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task ReserveSeries_NonPositiveWarehouseQuantity_ReturnsBadRequest()
    {
        var client = AuthedClient(Factory);
        var productId = await SeedProductAsync(client);
        var body = BuildBody(productId, warehouses:
        [
            new ReserveSeriesRequest.RequestBody.WarehouseAllocation { WarehouseId = Guid.NewGuid(), Quantity = 0 },
            new ReserveSeriesRequest.RequestBody.WarehouseAllocation { WarehouseId = Guid.NewGuid(), Quantity = 10 }
        ]);

        var response = await client.PostAsJsonAsync(new Uri("/v1/stock-reservations/series", UriKind.Relative), body);

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    // ── Cross-module guard: Inventory → Products (the direction ReserveSeries added) ──

    [Fact]
    public async Task ReserveSeries_ProductDoesNotExist_ReturnsBadRequest()
    {
        var client = AuthedClient(Factory);
        var body = BuildBody(Guid.NewGuid());

        var response = await client.PostAsJsonAsync(new Uri("/v1/stock-reservations/series", UriKind.Relative), body);

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    // ── Commit via HMAC webhook ──

    [Fact]
    public async Task Webhook_ValidSignature_CommitsReservation()
    {
        var client = AuthedClient(Factory);
        var productId = await SeedProductAsync(client);
        var reservation = await SeedReservationAsync(productId, 10, DateTimeOffset.UtcNow.AddDays(5));

        var response = await SendWebhookAsync(client, reservation.Id, "provider-ref-1");

        Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
        var reloaded = await FindReservationAsync(reservation.Id);
        Assert.Equal(ReservationStatus.Committed, reloaded!.Status);
        Assert.Equal("provider-ref-1", reloaded.ProviderReference);
    }

    [Fact]
    public async Task Webhook_InvalidSignature_ReturnsUnauthorized()
    {
        var client = AuthedClient(Factory);
        var productId = await SeedProductAsync(client);
        var reservation = await SeedReservationAsync(productId, 10, DateTimeOffset.UtcNow.AddDays(5));

        var response = await SendWebhookAsync(client, reservation.Id, "provider-ref-1", signatureOverride: "00");

        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        var reloaded = await FindReservationAsync(reservation.Id);
        Assert.Equal(ReservationStatus.Active, reloaded!.Status);
    }

    [Fact]
    public async Task Webhook_MissingSignatureHeader_ReturnsUnauthorized()
    {
        var client = AuthedClient(Factory);
        var productId = await SeedProductAsync(client);
        var reservation = await SeedReservationAsync(productId, 10, DateTimeOffset.UtcNow.AddDays(5));
        var rawBody = $$"""{"ReservationId":"{{reservation.Id.Value}}","ProviderReference":"x"}""";

        using var content = new StringContent(rawBody, Encoding.UTF8, "application/json");
        var response = await client.PostAsync(new Uri("/v1/stock-reservations/webhooks/warehouse", UriKind.Relative), content);

        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
    }

    [Fact]
    public async Task Webhook_MalformedBody_ReturnsBadRequest()
    {
        var client = AuthedClient(Factory);
        const string rawBody = "{not valid json";
        var secret = await WebhookSecretAsync();
        var signature = ComputeSignature(rawBody, secret);

        using var request = new HttpRequestMessage(HttpMethod.Post, new Uri("/v1/stock-reservations/webhooks/warehouse", UriKind.Relative))
        {
            Content = new StringContent(rawBody, Encoding.UTF8, "application/json")
        };
        request.Headers.Add("X-Warehouse-Signature", signature);

        var response = await client.SendAsync(request);

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task Webhook_DuplicateProviderReference_IsIdempotentNoOp()
    {
        var client = AuthedClient(Factory);
        var productId = await SeedProductAsync(client);
        var reservation = await SeedReservationAsync(productId, 10, DateTimeOffset.UtcNow.AddDays(5));

        var first = await SendWebhookAsync(client, reservation.Id, "provider-ref-dup");
        var second = await SendWebhookAsync(client, reservation.Id, "provider-ref-dup");

        Assert.Equal(HttpStatusCode.NoContent, first.StatusCode);
        Assert.Equal(HttpStatusCode.NoContent, second.StatusCode);
        var reloaded = await FindReservationAsync(reservation.Id);
        Assert.Equal(ReservationStatus.Committed, reloaded!.Status);
        Assert.Equal("provider-ref-dup", reloaded.ProviderReference);
    }

    [Fact]
    public async Task Webhook_ConflictingProviderReference_TransitionsToRequiresReconciliation()
    {
        var client = AuthedClient(Factory);
        var productId = await SeedProductAsync(client);
        var reservation = await SeedReservationAsync(productId, 10, DateTimeOffset.UtcNow.AddDays(5));

        var first = await SendWebhookAsync(client, reservation.Id, "provider-ref-A");
        var second = await SendWebhookAsync(client, reservation.Id, "provider-ref-B");

        // The aggregate never surfaces a conflicting webhook as a client-facing error - it's flagged for
        // manual reconciliation instead (mirrors DepositPayment's double-charge-detection contract), so the
        // webhook always 204s; the conflict is only visible in the persisted Status.
        Assert.Equal(HttpStatusCode.NoContent, first.StatusCode);
        Assert.Equal(HttpStatusCode.NoContent, second.StatusCode);
        var reloaded = await FindReservationAsync(reservation.Id);
        Assert.Equal(ReservationStatus.RequiresReconciliation, reloaded!.Status);
    }

    // ── Release ──

    [Fact]
    public async Task Release_ActiveReservation_Succeeds()
    {
        var client = AuthedClient(Factory);
        var productId = await SeedProductAsync(client);
        var reservation = await SeedReservationAsync(productId, 10, DateTimeOffset.UtcNow.AddDays(5));

        var response = await client.PostAsync(new Uri($"/v1/stock-reservations/{reservation.Id}/release", UriKind.Relative), null);

        Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
        var reloaded = await FindReservationAsync(reservation.Id);
        Assert.Equal(ReservationStatus.Released, reloaded!.Status);
    }

    [Fact]
    public async Task Release_AlreadyReleased_IsIdempotentNoOp()
    {
        var client = AuthedClient(Factory);
        var productId = await SeedProductAsync(client);
        var reservation = await SeedReservationAsync(productId, 10, DateTimeOffset.UtcNow.AddDays(5));

        var first = await client.PostAsync(new Uri($"/v1/stock-reservations/{reservation.Id}/release", UriKind.Relative), null);
        var second = await client.PostAsync(new Uri($"/v1/stock-reservations/{reservation.Id}/release", UriKind.Relative), null);

        Assert.Equal(HttpStatusCode.NoContent, first.StatusCode);
        Assert.Equal(HttpStatusCode.NoContent, second.StatusCode);
        var reloaded = await FindReservationAsync(reservation.Id);
        Assert.Equal(ReservationStatus.Released, reloaded!.Status);
    }

    [Fact]
    public async Task Release_CommittedReservation_ReturnsBadRequest()
    {
        var client = AuthedClient(Factory);
        var productId = await SeedProductAsync(client);
        var reservation = await SeedCommittedReservationAsync(
            productId, 10, DateTimeOffset.UtcNow.AddDays(5), "already-committed-ref");

        var response = await client.PostAsync(new Uri($"/v1/stock-reservations/{reservation.Id}/release", UriKind.Relative), null);

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    // ── Sweep expiry (in-process call, not waiting for the real cron) ──

    [Fact]
    public async Task Sweep_PastDeadlineActiveReservation_TransitionsToExpired()
    {
        var client = AuthedClient(Factory);
        var productId = await SeedProductAsync(client);
        var reservation = await SeedReservationAsync(productId, 10, DateTimeOffset.UtcNow.AddDays(-1));

        using var scope = Factory.Services.CreateScope();
        var sweep = scope.ServiceProvider.GetRequiredService<StockReservationExpirySweepService>();
        await sweep.SweepAsync();

        var reloaded = await FindReservationAsync(reservation.Id);
        Assert.Equal(ReservationStatus.Expired, reloaded!.Status);
    }

    [Fact]
    public async Task Sweep_NotPastDeadline_LeavesUntouched()
    {
        var client = AuthedClient(Factory);
        var productId = await SeedProductAsync(client);
        var reservation = await SeedReservationAsync(productId, 10, DateTimeOffset.UtcNow.AddDays(5));

        using var scope = Factory.Services.CreateScope();
        var sweep = scope.ServiceProvider.GetRequiredService<StockReservationExpirySweepService>();
        await sweep.SweepAsync();

        var reloaded = await FindReservationAsync(reservation.Id);
        Assert.Equal(ReservationStatus.Active, reloaded!.Status);
    }

    [Fact]
    public async Task Sweep_AlreadyCommittedPastDeadline_IsNoOp()
    {
        var client = AuthedClient(Factory);
        var productId = await SeedProductAsync(client);
        var reservation = await SeedCommittedReservationAsync(
            productId, 10, DateTimeOffset.UtcNow.AddDays(-1), "committed-before-deadline");

        using var scope = Factory.Services.CreateScope();
        var sweep = scope.ServiceProvider.GetRequiredService<StockReservationExpirySweepService>();
        await sweep.SweepAsync();

        var reloaded = await FindReservationAsync(reservation.Id);
        Assert.Equal(ReservationStatus.Committed, reloaded!.Status);
    }

    // ── Cross-module availability: Products → Inventory (GetStockLevelRequest) ──

    [Fact]
    public async Task GetProduct_AvailableQuantity_SubtractsActiveReservations()
    {
        var client = AuthedClient(Factory);
        var productId = await SeedProductAsync(client, quantity: 100);

        using (var scope = Factory.Services.CreateScope())
        {
            var db = scope.ServiceProvider.GetRequiredService<IInventoryDbContext>();
            db.StockLevels.Add(Inventory.Domain.StockLevels.StockLevel.Create(productId, 100));
            await SeedReservationAsync(productId, 20, DateTimeOffset.UtcNow.AddDays(5));
            await SeedReservationAsync(productId, 15, DateTimeOffset.UtcNow.AddDays(5));
            await db.SaveChangesAsync();
        }

        var response = await client.GetAsync(new Uri($"/v1/products/{productId}", UriKind.Relative));
        Assert.True(response.IsSuccessStatusCode, await response.Content.ReadAsStringAsync());
        var body = await response.Content.ReadFromJsonAsync<Products.Endpoints.Products.v1.Get.Response>(JsonSerializerOptions);

        Assert.Equal(100, body!.Quantity);
        Assert.Equal(65, body.AvailableQuantity);
    }

    // ── Outbox publish (write-side assertion, matching this repo's own slice-test convention) ──

    [Fact]
    public async Task CreateProduct_PublishesProductCreatedIntegrationEvent()
    {
        var client = AuthedClient(Factory);
        await SeedProductAsync(client, quantity: 42);

        using var scope = Factory.Services.CreateScope();
        var outboxDb = scope.ServiceProvider.GetRequiredService<IOutboxDbContext>();
        var messages = await outboxDb.OutboxMessages
            .AsNoTracking()
            .Where(m => !m.IsProcessed)
            .ToListAsync();

        Assert.Contains(messages, m => m.Event is ProductCreatedIntegrationEvent e && e.Quantity == 42);
    }

    // Request wraps the actual payload in a "Body" property (no [AsParameters] on this endpoint, so the
    // whole Request is bound from JSON as-is - the wire shape needs the "body" wrapper key, not the flat
    // fields directly).
    private static ReserveSeriesRequest BuildBody(
        DefaultIdType productId,
        int quantityPerOccurrence = 10,
        DateTimeOffset? firstDeadline = null,
        int intervalDays = 7,
        int occurrenceCount = 2,
        ICollection<ReserveSeriesRequest.RequestBody.WarehouseAllocation>? warehouses = null)
    {
        var deadline = firstDeadline ?? DateTimeOffset.UtcNow.AddDays(10);

        return new ReserveSeriesRequest
        {
            Body = new ReserveSeriesRequest.RequestBody
            {
                ProductId = productId,
                QuantityPerOccurrence = quantityPerOccurrence,
                FirstDeadline = deadline,
                IntervalDays = intervalDays,
                OccurrenceCount = occurrenceCount,
                Warehouses = warehouses ??
                [
                    new ReserveSeriesRequest.RequestBody.WarehouseAllocation { WarehouseId = Guid.NewGuid(), Quantity = quantityPerOccurrence }
                ]
            }
        };
    }
}
