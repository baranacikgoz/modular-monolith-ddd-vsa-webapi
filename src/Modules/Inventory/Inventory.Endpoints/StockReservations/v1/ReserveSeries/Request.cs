using Common.Application.Localization.Resources;
using Common.Application.Validation;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace Inventory.Endpoints.StockReservations.v1.ReserveSeries;

public sealed record Request
{
    [FromBody] public required RequestBody Body { get; init; }

    public sealed class RequestBody
    {
        public required DefaultIdType ProductId { get; init; }
        public required int QuantityPerOccurrence { get; init; }
        public required DateTimeOffset FirstDeadline { get; init; }
        public required int IntervalDays { get; init; }
        public required int OccurrenceCount { get; init; }
        public required ICollection<WarehouseAllocation> Warehouses { get; init; }

        public sealed class WarehouseAllocation
        {
            public required DefaultIdType WarehouseId { get; init; }
            public required int Quantity { get; init; }
        }
    }
}

public sealed class RequestValidator : CustomValidator<Request>
{
    public RequestValidator(IResxLocalizer localizer)
    {
        RuleFor(x => x.Body)
            .NotEmpty()
            .SetValidator(new RequestBodyValidator(localizer));
    }
}

public sealed class RequestBodyValidator : CustomValidator<Request.RequestBody>
{
    public RequestBodyValidator(IResxLocalizer localizer)
    {
        RuleFor(x => x.ProductId)
            .NotEmpty()
            .WithMessage(localizer.StockReservations_ReserveSeries_ProductId_NotEmpty);

        RuleFor(x => x.QuantityPerOccurrence)
            .GreaterThan(0)
            .WithMessage(localizer.StockReservations_ReserveSeries_QuantityPerOccurrence_GreaterThanZero);

        RuleFor(x => x.OccurrenceCount)
            .GreaterThan(0)
            .WithMessage(localizer.StockReservations_ReserveSeries_OccurrenceCount_GreaterThanZero);

        RuleFor(x => x.IntervalDays)
            .GreaterThan(0)
            .WithMessage(localizer.StockReservations_ReserveSeries_IntervalDays_GreaterThanZero);

        RuleFor(x => x.Warehouses)
            .NotEmpty()
            .WithMessage(localizer.StockReservations_ReserveSeries_Warehouses_NotEmpty);

        // Nested per-item validation: each warehouse allocation checked independently.
        RuleForEach(x => x.Warehouses).ChildRules(warehouse =>
        {
            warehouse.RuleFor(w => w.WarehouseId)
                .NotEmpty()
                .WithMessage(localizer.StockReservations_ReserveSeries_Warehouses_WarehouseId_NotEmpty);

            warehouse.RuleFor(w => w.Quantity)
                .GreaterThan(0)
                .WithMessage(localizer.StockReservations_ReserveSeries_Warehouses_Quantity_GreaterThanZero);
        });

        // Whole-object cross-collection checks: uniqueness within the list, then referential consistency
        // against the header quantity - mirrors Appointments' SubmitOnboarding's TempId-distinct +
        // sibling-collection-referential-integrity rules.
        RuleFor(x => x)
            .Must(body => body.Warehouses.Select(w => w.WarehouseId).Distinct().Count() == body.Warehouses.Count)
            .WithMessage(localizer.StockReservations_ReserveSeries_Warehouses_DuplicateWarehouseId)
            .When(x => x.Warehouses.Count > 0);

        RuleFor(x => x)
            .Must(body => body.Warehouses.Sum(w => w.Quantity) == body.QuantityPerOccurrence)
            .WithMessage(localizer.StockReservations_ReserveSeries_Warehouses_QuantitySumMismatch)
            .When(x => x.Warehouses.Count > 0);
    }
}
