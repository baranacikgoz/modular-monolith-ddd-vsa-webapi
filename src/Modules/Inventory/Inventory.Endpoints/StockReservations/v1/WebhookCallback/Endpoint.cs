using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using Common.Application.Extensions;
using Common.Domain.ResultMonad;
using Common.Infrastructure.Persistence.Extensions;
using FluentValidation;
using Inventory.Application.Persistence;
using Inventory.Domain.StockReservations;
using Inventory.Infrastructure.Telemetry;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Options;
using Common.Application.Options;

namespace Inventory.Endpoints.StockReservations.v1.WebhookCallback;

internal static class Endpoint
{
    private const string SignatureHeaderName = "X-Warehouse-Signature";
    private static readonly JsonSerializerOptions JsonOptions = new(JsonSerializerDefaults.Web);

    internal static void MapEndpoint(RouteGroupBuilder stockReservationsApiGroup)
    {
        stockReservationsApiGroup
            .MapPost("webhooks/warehouse", HandleWarehouseWebhookAsync)
            .WithDescription(
                "Anonymous webhook callback from the warehouse system - authenticated via an HMAC-SHA256 " +
                "signature header instead of a bearer token, mirroring Payments' Iyzico webhook pattern.")
            .AllowAnonymous()
            .Produces(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status401Unauthorized)
            .TransformResultToNoContentResponse();
    }

    private static async Task<Result> HandleWarehouseWebhookAsync(
        HttpContext httpContext,
        IOptions<InventoryOptions> inventoryOptions,
        IValidator<Request> validator,
        IInventoryDbContext dbContext,
        TimeProvider timeProvider,
        CancellationToken cancellationToken)
    {
        httpContext.Request.EnableBuffering();
        using var reader = new StreamReader(httpContext.Request.Body, leaveOpen: true);
        var rawBody = await reader.ReadToEndAsync(cancellationToken);
        httpContext.Request.Body.Position = 0;

        return await VerifySignature(httpContext.Request.Headers, rawBody, inventoryOptions.Value.WebhookSharedSecret)
            .BindAsync(() => ParseAndValidateAsync(rawBody, validator, cancellationToken))
            .BindAsync(body => dbContext
                .StockReservations
                .TagWith(nameof(HandleWarehouseWebhookAsync), body.ReservationId)
                .Where(r => r.Id == body.ReservationId)
                .SingleAsResultAsync(nameof(StockReservation), cancellationToken)
                .TapAsync(reservation => reservation.Commit(body.ProviderReference, timeProvider.GetUtcNow())))
            .TapAsync(async _ => await dbContext.SaveChangesAsync(cancellationToken))
            .TapAsync(_ => InventoryTelemetry.StockReservationsCommitted.Add(1));
    }

    /// <summary>
    ///     Verifies the caller knows <see cref="InventoryOptions.WebhookSharedSecret"/> by recomputing the
    ///     HMAC-SHA256 over the EXACT raw body bytes and comparing in constant time - never a plain
    ///     equality/SequenceEqual on secret-derived material, which leaks timing information an attacker
    ///     can use to forge a valid signature byte-by-byte.
    /// </summary>
    private static Result VerifySignature(IHeaderDictionary headers, string rawBody, string sharedSecret)
    {
        if (!headers.TryGetValue(SignatureHeaderName, out var signatureHeader) || string.IsNullOrEmpty(signatureHeader))
        {
            return new Error { Key = "MissingWebhookSignature", StatusCode = HttpStatusCode.Unauthorized };
        }

        using var hmac = new HMACSHA256(Encoding.UTF8.GetBytes(sharedSecret));
        var expectedSignature = hmac.ComputeHash(Encoding.UTF8.GetBytes(rawBody));

        if (!TryDecodeHex(signatureHeader.ToString(), out var providedSignature) ||
            !CryptographicOperations.FixedTimeEquals(expectedSignature, providedSignature))
        {
            return new Error { Key = "InvalidWebhookSignature", StatusCode = HttpStatusCode.Unauthorized };
        }

        return Result.Success;
    }

    private static bool TryDecodeHex(string hex, out byte[] bytes)
    {
        try
        {
            bytes = Convert.FromHexString(hex);
            return true;
        }
        catch (FormatException)
        {
            bytes = [];
            return false;
        }
    }

    private static async Task<Result<Request>> ParseAndValidateAsync(
        string rawBody, IValidator<Request> validator, CancellationToken cancellationToken)
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
