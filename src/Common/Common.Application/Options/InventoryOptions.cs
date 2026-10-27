using Common.Application.Validation;
using FluentValidation;

namespace Common.Application.Options;

public class InventoryOptions
{
    public required int MaxOccurrencesPerSeries { get; set; }
    public required int SafetyBufferQuantity { get; set; }

    /// <summary>Cron for the recurring job that expires stock reservations past their deadline.</summary>
    public required string StockReservationExpirySweepCron { get; set; }

    /// <summary>Active reservations examined per recurring job run.</summary>
    public required int StockReservationExpirySweepBatchSize { get; set; }

    /// <summary>Cron for the recurring job that heals the local product copy from Products.</summary>
    public required string ProductSummaryReconciliationCron { get; set; }

    /// <summary>
    ///     Time zone id (IANA, for example <c>Europe/Berlin</c>) whose calendar days the per-store daily stock totals are
    ///     counted in. A business day is not the UTC day.
    /// </summary>
    public required string BusinessTimeZoneId { get; set; }

    /// <summary>Largest inbound warehouse webhook body accepted, in bytes; a larger one is refused with 413 before it is read.</summary>
    public required int WebhookMaxBodyBytes { get; set; }

    /// <summary>
    ///     Days a recorded webhook delivery id is kept. It has to outlive the longest span in which the warehouse system may deliver
    ///     the same delivery again; after that the record only takes space.
    /// </summary>
    public required int WebhookDeliveryRetentionDays { get; set; }

    /// <summary>Cron for the recurring job that deletes recorded webhook deliveries older than <see cref="WebhookDeliveryRetentionDays" />.</summary>
    public required string WebhookDeliveryRetentionCron { get; set; }
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

        RuleFor(x => x.ProductSummaryReconciliationCron)
            .NotEmpty()
            .WithMessage("ProductSummaryReconciliationCron must not be empty.");

        RuleFor(x => x.BusinessTimeZoneId)
            .NotEmpty()
            .Must(id => !string.IsNullOrEmpty(id) && TimeZoneInfo.TryFindSystemTimeZoneById(id, out _))
            .WithMessage("BusinessTimeZoneId must be the id of a time zone known to the host.");

        RuleFor(x => x.WebhookMaxBodyBytes)
            .GreaterThan(0)
            .WithMessage("WebhookMaxBodyBytes must be greater than 0.");

        RuleFor(x => x.WebhookDeliveryRetentionDays)
            .GreaterThan(0)
            .WithMessage("WebhookDeliveryRetentionDays must be greater than 0.");

        RuleFor(x => x.WebhookDeliveryRetentionCron)
            .NotEmpty()
            .WithMessage("WebhookDeliveryRetentionCron must not be empty.");
    }
}
