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

    /// <summary>
    ///     Cron for the recurring sweep that republishes the push of every connection with a stock mark older than
    ///     <see cref="StockPushDirtyMinAgeSeconds" />. The sweep only publishes; a push runs in the queue consumer.
    /// </summary>
    public required string StockPushDirtySweepCron { get; set; }

    /// <summary>
    ///     A stock mark younger than this is left to the push its own change queued; an older one means that push was lost or
    ///     failed, and the sweep queues another.
    /// </summary>
    public required int StockPushDirtyMinAgeSeconds { get; set; }

    /// <summary>Stock levels sent to the warehouse in one gateway call.</summary>
    public required int StockPushBatchSize { get; set; }

    /// <summary>Gateway calls one push message makes before it queues a continuation for itself when marks remain.</summary>
    public required int StockPushBatchesPerMessage { get; set; }

    /// <summary>Push messages one pod works on at once. Also the consumer's prefetch count.</summary>
    public required int StockPushConcurrentMessageLimit { get; set; }

    /// <summary>Connections (or job rows) one page of a recurring sweep publishes before the next page is read.</summary>
    public required int SweepPageSize { get; set; }

    /// <summary>Bulk adjustment messages one pod works on at once. Also the consumer's prefetch count: a prefetched message
    /// waiting for a slot sits unacked and would trip the broker's consumer timeout.</summary>
    public required int BulkAdjustJobConcurrentMessageLimit { get; set; }

    /// <summary>Batches of <see cref="BulkImportOptions.SaveChunkSize" /> rows one bulk adjustment message saves before the job
    /// stores its cursor and queues a continuation message for itself.</summary>
    public required int BulkAdjustBatchesPerMessage { get; set; }

    /// <summary>Row errors kept on a bulk adjustment job; further rejected rows are only counted.</summary>
    public required int BulkAdjustMaxStoredRowErrors { get; set; }

    /// <summary>Cron of the recurring job that fails stale bulk adjustment jobs and deletes finished ones with their uploads.</summary>
    public required string StockJobHousekeepingCron { get; set; }
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

        RuleFor(x => x.StockPushDirtySweepCron)
            .NotEmpty()
            .WithMessage("StockPushDirtySweepCron must not be empty.");

        RuleFor(x => x.StockPushDirtyMinAgeSeconds)
            .GreaterThan(0)
            .WithMessage("StockPushDirtyMinAgeSeconds must be greater than 0.");

        RuleFor(x => x.StockPushBatchSize)
            .GreaterThan(0)
            .WithMessage("StockPushBatchSize must be greater than 0.");

        RuleFor(x => x.StockPushBatchesPerMessage)
            .GreaterThan(0)
            .WithMessage("StockPushBatchesPerMessage must be greater than 0.");

        RuleFor(x => x.StockPushConcurrentMessageLimit)
            .GreaterThan(0)
            .WithMessage("StockPushConcurrentMessageLimit must be greater than 0.");

        RuleFor(x => x.SweepPageSize)
            .GreaterThan(0)
            .WithMessage("SweepPageSize must be greater than 0.");

        RuleFor(x => x.BulkAdjustJobConcurrentMessageLimit)
            .GreaterThan(0)
            .WithMessage("BulkAdjustJobConcurrentMessageLimit must be greater than 0.");

        RuleFor(x => x.BulkAdjustBatchesPerMessage)
            .GreaterThan(0)
            .WithMessage("BulkAdjustBatchesPerMessage must be greater than 0.");

        RuleFor(x => x.BulkAdjustMaxStoredRowErrors)
            .GreaterThan(0)
            .WithMessage("BulkAdjustMaxStoredRowErrors must be greater than 0.");

        RuleFor(x => x.StockJobHousekeepingCron)
            .NotEmpty()
            .WithMessage("StockJobHousekeepingCron must not be empty.");
    }
}
