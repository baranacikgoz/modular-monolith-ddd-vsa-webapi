using System.Net;
using System.Text.Json;
using Common.Application.Extensions;
using Common.Application.Options;
using Common.Domain.ResultMonad;
using Common.Endpoints.Webhooks;
using Common.Infrastructure.Persistence.Extensions;
using Common.Infrastructure.Security;
using FluentValidation;
using Inventory.Application.Persistence;
using Inventory.Domain.WarehouseConnections;
using Inventory.Infrastructure.Telemetry;
using Inventory.Infrastructure.Webhooks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using RateLimitingConstants = Inventory.Infrastructure.RateLimiting.Constants;

namespace Inventory.Endpoints.StockReservations.v1.WebhookCallback;

internal static class Endpoint
{
    private const string SignatureHeaderName = "X-Warehouse-Signature";
    private static readonly JsonSerializerOptions JsonOptions = new(JsonSerializerDefaults.Web);

    internal static void MapEndpoint(RouteGroupBuilder stockReservationsApiGroup)
    {
        stockReservationsApiGroup
            .MapPost(WebhookRoute.Template, HandleWarehouseWebhookAsync)
            .WithDescription(
                "Anonymous webhook callback from a store's warehouse system. Authenticated by an HMAC-SHA256 signature header made " +
                "with the connection's own webhook secret instead of a bearer token. A delivery id seen before on the connection " +
                "is acknowledged and applies nothing. A status word this system has no mapping for is acknowledged and recorded, " +
                "never refused.")
            .AllowAnonymous()
            .RequireRateLimiting(RateLimitingConstants.WarehouseWebhook)
            .LimitRequestBody(context =>
                context.RequestServices.GetRequiredService<IOptions<InventoryOptions>>().Value.WebhookMaxBodyBytes)
            .Produces(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status401Unauthorized)
            .Produces(StatusCodes.Status404NotFound)
            .TransformResultToNoContentResponse();
    }

    private static async Task<Result> HandleWarehouseWebhookAsync(
        [AsParameters] RouteParameters route,
        HttpContext httpContext,
        IValidator<Request> validator,
        IInventoryDbContext dbContext,
        SecretProtector protector,
        WarehouseWebhookIntake intake,
        CancellationToken cancellationToken)
    {
        // The raw bytes, never a decoded string: the signature covers exactly what the partner sent.
        using var buffer = new MemoryStream();
        await httpContext.Request.Body.CopyToAsync(buffer, cancellationToken);
        var rawBody = buffer.ToArray();

        return await dbContext.WarehouseConnections
            .AsNoTracking()
            .TagWith(nameof(HandleWarehouseWebhookAsync), route.ConnectionId)
            .Where(c => c.Id == route.ConnectionId)
            .SingleAsResultAsync(nameof(WarehouseConnection), cancellationToken)
            .BindAsync(connection => Task.FromResult(
                VerifySignature(httpContext.Request.Headers, rawBody, protector.Decrypt(connection.EncryptedWebhookSecret))
                    .Map(() => connection)))
            .BindAsync(connection => connection.Status == WarehouseConnectionStatus.Active
                ? ParseAndValidateAsync(rawBody, validator, cancellationToken).BindAsync(body => intake.ReceiveAsync(
                    connection, body.DeliveryId, body.ReservationId, body.ProviderReference, body.StatusWord, cancellationToken))
                // A deactivated connection's authenticated delivery is acknowledged and ignored: the warehouse stops retrying and
                // nothing of its body is read or applied.
                : Task.FromResult(Result<WebhookDeliveryOutcome>.Success(WebhookDeliveryOutcome.Ignored)))
            .TapAsync(RecordOutcome);
    }

    private static void RecordOutcome(WebhookDeliveryOutcome outcome)
    {
        InventoryTelemetry.WebhookDeliveries.Add(1, new KeyValuePair<string, object?>("outcome", outcome.ToString()));
    }

    /// <summary>
    ///     Verifies the caller knows the connection's webhook secret: HMAC-SHA256 over the raw body bytes, compared in
    ///     constant time by <see cref="HmacSignatureVerifier"/>.
    /// </summary>
    private static Result VerifySignature(IHeaderDictionary headers, byte[] rawBody, string webhookSecret)
    {
        if (!headers.TryGetValue(SignatureHeaderName, out var signatureHeader) || string.IsNullOrEmpty(signatureHeader))
        {
            return new Error { Key = "MissingWebhookSignature", StatusCode = HttpStatusCode.Unauthorized };
        }

        return HmacSignatureVerifier.VerifySha256(webhookSecret, rawBody, signatureHeader.ToString())
            ? Result.Success
            : new Error { Key = "InvalidWebhookSignature", StatusCode = HttpStatusCode.Unauthorized };
    }

    private static async Task<Result<Request>> ParseAndValidateAsync(
        byte[] rawBody, IValidator<Request> validator, CancellationToken cancellationToken)
    {
        Request? body;
        try
        {
            body = JsonSerializer.Deserialize<Request>(rawBody, JsonOptions);
        }
        catch (JsonException)
        {
            return new Error { Key = "MalformedWebhookBody", StatusCode = HttpStatusCode.BadRequest };
        }

        if (body is null)
        {
            return new Error { Key = "MalformedWebhookBody", StatusCode = HttpStatusCode.BadRequest };
        }

        var validationResult = await validator.ValidateAsync(body, cancellationToken);
        if (!validationResult.IsValid)
        {
            return Error.Validation(validationResult.Errors.Select(e => e.ErrorMessage).ToList());
        }

        return body;
    }
}
