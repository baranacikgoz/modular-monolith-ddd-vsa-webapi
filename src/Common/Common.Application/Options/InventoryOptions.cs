using Common.Application.Validation;
using FluentValidation;

namespace Common.Application.Options;

public enum WarehouseGatewayProvider
{
    /// <summary>In-memory fake, always succeeds. Non-production only.</summary>
    Dummy,
    Http
}

public class InventoryOptions
{
    public required int MaxOccurrencesPerSeries { get; set; }
    public required int SafetyBufferQuantity { get; set; }

    /// <summary>Cron for the recurring job that expires stock reservations past their deadline.</summary>
    public required string StockReservationExpirySweepCron { get; set; }

    /// <summary>Active reservations examined per recurring job run.</summary>
    public required int StockReservationExpirySweepBatchSize { get; set; }

    public required WarehouseGatewayProvider WarehouseGatewayProvider { get; set; }

    /// <summary>Base URL for the warehouse system's HTTP API. Only read when Provider is Http.</summary>
    public string? WarehouseGatewayBaseUrl { get; set; }

    /// <summary>Shared secret used to verify the HMAC-SHA256 signature on inbound warehouse webhook callbacks.</summary>
    public required string WebhookSharedSecret { get; set; }
}

public class InventoryOptionsValidator : CustomValidator<InventoryOptions>
{
    public InventoryOptionsValidator()
    {
        RuleFor(x => x.MaxOccurrencesPerSeries)
            .GreaterThan(0)
            .WithMessage("Max occurrences per series must be greater than 0.");

        RuleFor(x => x.SafetyBufferQuantity)
            .GreaterThanOrEqualTo(0)
            .WithMessage("Safety buffer quantity must be at least 0.");

        RuleFor(x => x.StockReservationExpirySweepCron)
            .NotEmpty()
            .WithMessage("StockReservationExpirySweepCron must not be empty.");

        RuleFor(x => x.StockReservationExpirySweepBatchSize)
            .GreaterThan(0)
            .WithMessage("StockReservationExpirySweepBatchSize must be greater than 0.");

        // Dummy gateway always succeeds; set Provider to 'Http' (with a real BaseUrl) before deploying.
        RuleFor(x => x.WarehouseGatewayProvider)
            .Must((_, provider, context) => !context.IsProduction() || provider != WarehouseGatewayProvider.Dummy)
            .WithMessage("InventoryOptions.WarehouseGatewayProvider is 'Dummy' in Production.");

        RuleFor(x => x.WarehouseGatewayBaseUrl)
            .NotEmpty()
            .Must(x => Uri.TryCreate(x, UriKind.Absolute, out _))
            .WithMessage("WarehouseGatewayBaseUrl must be a valid URL.")
            .When(x => x.WarehouseGatewayProvider == WarehouseGatewayProvider.Http);

        RuleFor(x => x.WebhookSharedSecret)
            .NotEmpty()
            .WithMessage("WebhookSharedSecret must not be empty.");
    }
}
