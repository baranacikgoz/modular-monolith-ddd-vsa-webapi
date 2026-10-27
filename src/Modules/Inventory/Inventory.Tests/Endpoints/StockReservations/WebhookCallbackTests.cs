using System.Net;
using System.Net.Http.Json;
using System.Text;
using Common.Application.Options;
using Common.IntegrationEvents;
using Common.Tests;
using Inventory.Application.Persistence;
using Inventory.Domain.StockReservations;
using Inventory.Domain.WarehouseConnections;
using Inventory.Tests.Support;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Xunit;
using RateLimitingConstants = Inventory.Infrastructure.RateLimiting.Constants;
using ReservationConstants = Inventory.Domain.StockReservations.Constants;
using RotateResponse = Inventory.Endpoints.WarehouseConnections.v1.RotateWebhookSecret.Response;

namespace Inventory.Tests.Endpoints.StockReservations;

/// <summary>
///     The warehouse webhook of one connection: anonymous, authenticated by an HMAC made with the connection's own secret,
///     and applied once per delivery id however often the warehouse delivers it.
/// </summary>
[Collection("IntegrationTestCollection")]
public class WebhookCallbackTests(IntegrationTestWebAppFactory factory) : BaseIntegrationTest(factory)
{
    private sealed record Scenario(WarehouseConnection Connection, string Secret, StockReservation Reservation);

    private async Task<Scenario> SeedAsync(bool deactivated = false, bool committed = false)
    {
        var storeId = DefaultIdType.CreateVersion7();
        var (connection, secret) = await WarehouseConnectionTestData.SeedAsync(Factory.Services, storeId, deactivated: deactivated);
        var productId = await WarehouseConnectionTestData.SeedProductOfStoreAsync(Factory.Services, storeId);
        var reservation = await WarehouseConnectionTestData.SeedReservationAsync(Factory.Services, productId, committed);
        return new Scenario(connection, secret, reservation);
    }

    private static HttpClient Anonymous(IntegrationTestFactory factory) => factory.CreateClient();

    private async Task<int> CountDeliveriesAsync(WarehouseConnectionId connectionId)
    {
        await using var scope = Factory.Services.CreateAsyncScope();
        var db = scope.ServiceProvider.GetRequiredService<IInventoryDbContext>();
        return await db.WebhookDeliveries.AsNoTracking().CountAsync(d => d.ConnectionId == connectionId);
    }

    private async Task<int> CountCommittedEventsAsync(StockReservation reservation)
    {
        var queued = await StockLevelTestData.QueuedEventsAsync<StockReservationCommittedIntegrationEvent>(Factory.Services);
        return queued.Count(e => e.ReservationId == reservation.Id.Value);
    }

    [Fact]
    public async Task Webhook_ValidSignature_CommitsTheReservationAndRecordsTheDelivery()
    {
        var scenario = await SeedAsync();

        var response = await WarehouseWebhookSender.PostAsync(
            Anonymous(Factory), scenario.Connection.Id, scenario.Secret, scenario.Reservation.Id, "delivery-1", "provider-ref-1");

        Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
        var reloaded = await WarehouseConnectionTestData.ReloadReservationAsync(Factory.Services, scenario.Reservation.Id);
        Assert.Equal(ReservationStatus.Committed, reloaded.Status);
        Assert.Equal("provider-ref-1", reloaded.ProviderReference);
        Assert.Equal(1, await CountDeliveriesAsync(scenario.Connection.Id));
        Assert.Equal(1, await CountCommittedEventsAsync(scenario.Reservation));
    }

    [Fact]
    public async Task Webhook_InvalidSignature_ReturnsUnauthorizedAndChangesNothing()
    {
        var scenario = await SeedAsync();

        var response = await WarehouseWebhookSender.PostAsync(
            Anonymous(Factory), scenario.Connection.Id, scenario.Secret, scenario.Reservation.Id, "delivery-1", "provider-ref-1",
            signatureOverride: "00");

        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        var reloaded = await WarehouseConnectionTestData.ReloadReservationAsync(Factory.Services, scenario.Reservation.Id);
        Assert.Equal(ReservationStatus.Active, reloaded.Status);
        Assert.Equal(0, await CountDeliveriesAsync(scenario.Connection.Id));
    }

    [Fact]
    public async Task Webhook_MissingSignatureHeader_ReturnsUnauthorized()
    {
        var scenario = await SeedAsync();
        var rawBody = WarehouseWebhookSender.BodyOf(scenario.Reservation.Id, "delivery-1", "x");

        using var content = new StringContent(rawBody, Encoding.UTF8, "application/json");
        var response = await Anonymous(Factory).PostAsync(WarehouseWebhookSender.RouteOf(scenario.Connection.Id), content);

        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
    }

    [Fact]
    public async Task Webhook_SignedWithTheSecretOfAnotherConnection_ReturnsUnauthorized()
    {
        var scenario = await SeedAsync();
        var (_, otherSecret) = await WarehouseConnectionTestData.SeedAsync(Factory.Services);

        var response = await WarehouseWebhookSender.PostAsync(
            Anonymous(Factory), scenario.Connection.Id, otherSecret, scenario.Reservation.Id, "delivery-1", "provider-ref-1");

        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
    }

    [Fact]
    public async Task Webhook_AfterTheSecretWasRotated_OnlyTheNewSecretVerifies()
    {
        var scenario = await SeedAsync();
        var rotated = await StockLevelTestData.AuthedClient(Factory)
            .PostAsync(new Uri($"/v1/warehouse-connections/{scenario.Connection.Id}/rotate-webhook-secret", UriKind.Relative), null);
        var newSecret = (await rotated.Content.ReadFromJsonAsync<RotateResponse>(JsonSerializerOptions))!.WebhookSecret;

        var withOld = await WarehouseWebhookSender.PostAsync(
            Anonymous(Factory), scenario.Connection.Id, scenario.Secret, scenario.Reservation.Id, "delivery-1", "provider-ref-1");
        var withNew = await WarehouseWebhookSender.PostAsync(
            Anonymous(Factory), scenario.Connection.Id, newSecret, scenario.Reservation.Id, "delivery-1", "provider-ref-1");

        Assert.Equal(HttpStatusCode.Unauthorized, withOld.StatusCode);
        Assert.Equal(HttpStatusCode.NoContent, withNew.StatusCode);
    }

    [Fact]
    public async Task Webhook_UnknownConnection_ReturnsNotFound()
    {
        var scenario = await SeedAsync();

        var response = await WarehouseWebhookSender.PostAsync(
            Anonymous(Factory), WarehouseConnectionId.New(), scenario.Secret, scenario.Reservation.Id, "delivery-1", "provider-ref-1");

        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]
    public async Task Webhook_DeactivatedConnection_IsAcknowledgedAndNothingIsApplied()
    {
        var scenario = await SeedAsync(deactivated: true);

        var response = await WarehouseWebhookSender.PostAsync(
            Anonymous(Factory), scenario.Connection.Id, scenario.Secret, scenario.Reservation.Id, "delivery-1", "provider-ref-1");

        Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
        var reloaded = await WarehouseConnectionTestData.ReloadReservationAsync(Factory.Services, scenario.Reservation.Id);
        Assert.Equal(ReservationStatus.Active, reloaded.Status);
        Assert.Equal(0, await CountDeliveriesAsync(scenario.Connection.Id));
    }

    [Fact]
    public async Task Webhook_ReservationOfAnotherStore_ReturnsNotFoundAndChangesNothing()
    {
        var scenario = await SeedAsync();
        var otherStoreProduct = await WarehouseConnectionTestData.SeedProductOfStoreAsync(Factory.Services, DefaultIdType.CreateVersion7());
        var foreign = await WarehouseConnectionTestData.SeedReservationAsync(Factory.Services, otherStoreProduct);

        var response = await WarehouseWebhookSender.PostAsync(
            Anonymous(Factory), scenario.Connection.Id, scenario.Secret, foreign.Id, "delivery-1", "provider-ref-1");

        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        var reloaded = await WarehouseConnectionTestData.ReloadReservationAsync(Factory.Services, foreign.Id);
        Assert.Equal(ReservationStatus.Active, reloaded.Status);
        Assert.Equal(0, await CountDeliveriesAsync(scenario.Connection.Id));
    }

    [Fact]
    public async Task Webhook_ProviderReferenceOverMaxLength_ReturnsBadRequest()
    {
        var scenario = await SeedAsync();

        var response = await WarehouseWebhookSender.PostAsync(
            Anonymous(Factory), scenario.Connection.Id, scenario.Secret, scenario.Reservation.Id, "delivery-1",
            new string('r', ReservationConstants.ProviderReferenceMaxLength + 1));

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        var reloaded = await WarehouseConnectionTestData.ReloadReservationAsync(Factory.Services, scenario.Reservation.Id);
        Assert.Equal(ReservationStatus.Active, reloaded.Status);
    }

    [Fact]
    public async Task Webhook_BodyOverTheLimit_Returns413BeforeTheEndpointReadsIt()
    {
        var scenario = await SeedAsync();
        var maxBodyBytes = Factory.Services.GetRequiredService<IOptions<InventoryOptions>>().Value.WebhookMaxBodyBytes;

        // Correctly signed, so only the size can reject it.
        var response = await WarehouseWebhookSender.PostAsync(
            Anonymous(Factory), scenario.Connection.Id, scenario.Secret, scenario.Reservation.Id, "delivery-1", new string('x', maxBodyBytes));

        Assert.Equal(HttpStatusCode.RequestEntityTooLarge, response.StatusCode);
        var reloaded = await WarehouseConnectionTestData.ReloadReservationAsync(Factory.Services, scenario.Reservation.Id);
        Assert.Equal(ReservationStatus.Active, reloaded.Status);
    }

    [Fact]
    public async Task Webhook_MalformedBody_ReturnsBadRequest()
    {
        var scenario = await SeedAsync();

        var response = await WarehouseWebhookSender.PostRawAsync(
            Anonymous(Factory), scenario.Connection.Id, scenario.Secret, "{not valid json");

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task Webhook_DuplicateDelivery_IsAcknowledgedAgainAndAppliedOnce()
    {
        var scenario = await SeedAsync();
        var client = Anonymous(Factory);

        var first = await WarehouseWebhookSender.PostAsync(
            client, scenario.Connection.Id, scenario.Secret, scenario.Reservation.Id, "delivery-dup", "provider-ref-1");
        var second = await WarehouseWebhookSender.PostAsync(
            client, scenario.Connection.Id, scenario.Secret, scenario.Reservation.Id, "delivery-dup", "provider-ref-1");

        Assert.Equal(HttpStatusCode.NoContent, first.StatusCode);
        Assert.Equal(HttpStatusCode.NoContent, second.StatusCode);
        Assert.Equal(1, await CountDeliveriesAsync(scenario.Connection.Id));
        Assert.Equal(1, await CountCommittedEventsAsync(scenario.Reservation));
    }

    [Fact]
    public async Task Webhook_RedeliveryAfterTheReservationMovedOn_IsStillAcknowledged()
    {
        var scenario = await SeedAsync();
        var client = Anonymous(Factory);
        await WarehouseWebhookSender.PostAsync(
            client, scenario.Connection.Id, scenario.Secret, scenario.Reservation.Id, "delivery-A", "provider-ref-A");
        // A conflicting reference under another delivery id sends the reservation to manual reconciliation.
        await WarehouseWebhookSender.PostAsync(
            client, scenario.Connection.Id, scenario.Secret, scenario.Reservation.Id, "delivery-B", "provider-ref-B");

        var redelivered = await WarehouseWebhookSender.PostAsync(
            client, scenario.Connection.Id, scenario.Secret, scenario.Reservation.Id, "delivery-A", "provider-ref-A");

        // Applying it again would be refused (the reservation is no longer active); a delivery seen before is acknowledged instead.
        Assert.Equal(HttpStatusCode.NoContent, redelivered.StatusCode);
        var reloaded = await WarehouseConnectionTestData.ReloadReservationAsync(Factory.Services, scenario.Reservation.Id);
        Assert.Equal(ReservationStatus.RequiresReconciliation, reloaded.Status);
    }

    [Fact]
    public async Task Webhook_SameProviderReferenceUnderANewDeliveryId_IsABenignDuplicateWithoutASecondEvent()
    {
        var scenario = await SeedAsync();
        var client = Anonymous(Factory);

        var first = await WarehouseWebhookSender.PostAsync(
            client, scenario.Connection.Id, scenario.Secret, scenario.Reservation.Id, "delivery-1", "provider-ref-dup");
        var second = await WarehouseWebhookSender.PostAsync(
            client, scenario.Connection.Id, scenario.Secret, scenario.Reservation.Id, "delivery-2", "provider-ref-dup");

        Assert.Equal(HttpStatusCode.NoContent, first.StatusCode);
        Assert.Equal(HttpStatusCode.NoContent, second.StatusCode);
        var reloaded = await WarehouseConnectionTestData.ReloadReservationAsync(Factory.Services, scenario.Reservation.Id);
        Assert.Equal(ReservationStatus.Committed, reloaded.Status);
        Assert.Equal(2, await CountDeliveriesAsync(scenario.Connection.Id));
        Assert.Equal(1, await CountCommittedEventsAsync(scenario.Reservation));
    }

    [Fact]
    public async Task Webhook_ConflictingProviderReference_TransitionsToRequiresReconciliation()
    {
        var scenario = await SeedAsync();
        var client = Anonymous(Factory);

        var first = await WarehouseWebhookSender.PostAsync(
            client, scenario.Connection.Id, scenario.Secret, scenario.Reservation.Id, "delivery-1", "provider-ref-A");
        var second = await WarehouseWebhookSender.PostAsync(
            client, scenario.Connection.Id, scenario.Secret, scenario.Reservation.Id, "delivery-2", "provider-ref-B");

        // The aggregate never surfaces a conflicting webhook as a client-facing error: it is flagged for manual
        // reconciliation instead, so the webhook always 204s and the conflict is only visible in the persisted status.
        Assert.Equal(HttpStatusCode.NoContent, first.StatusCode);
        Assert.Equal(HttpStatusCode.NoContent, second.StatusCode);
        var reloaded = await WarehouseConnectionTestData.ReloadReservationAsync(Factory.Services, scenario.Reservation.Id);
        Assert.Equal(ReservationStatus.RequiresReconciliation, reloaded.Status);
    }

    // A named policy only limits the routes that opt in: the webhook is anonymous and internet facing, so it must keep its own.
    [Fact]
    public void Webhook_Endpoint_IsLimitedByItsOwnPolicy()
    {
        var endpoint = Factory.Services.GetRequiredService<EndpointDataSource>().Endpoints
            .OfType<RouteEndpoint>()
            .Single(e => e.RoutePattern.RawText is { } text
                         && text.Contains("/stock-reservations/webhooks/warehouse/", StringComparison.Ordinal));

        var policy = endpoint.Metadata.GetMetadata<EnableRateLimitingAttribute>();

        Assert.NotNull(policy);
        Assert.Equal(RateLimitingConstants.WarehouseWebhook, policy.PolicyName);
    }
}
