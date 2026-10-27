using System.Globalization;
using Common.Application.Extensions;
using Common.Application.Localization.Resources;
using Common.Application.Options;
using Common.Application.Validation;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Inventory.Endpoints.StockLevels.v1.DailyTotals;

public sealed record Request
{
    [FromQuery] public required DefaultIdType StoreId { get; init; }

    /// <summary>First day of the range, a calendar day in the business time zone.</summary>
    [FromQuery] public required DateOnly From { get; init; }

    /// <summary>Last day of the range, included. Today in the business time zone when omitted.</summary>
    [FromQuery] public DateOnly? To { get; init; }

    public DateOnly LastDay(TimeProvider timeProvider, TimeZoneInfo businessTimeZone) => To ?? timeProvider.GetToday(businessTimeZone);
}

public sealed class RequestValidator : CustomValidator<Request>
{
    public RequestValidator(IResxLocalizer localizer, IOptions<InventoryOptions> options, TimeProvider timeProvider)
    {
        var maxDays = options.Value.DailyTotalsMaxRangeDays;
        var businessTimeZone = TimeZoneInfo.FindSystemTimeZoneById(options.Value.BusinessTimeZoneId);

        RuleFor(x => x.StoreId)
            .NotEmpty()
            .WithMessage(localizer.StockLevels_DailyTotals_StoreId_NotEmpty);

        RuleFor(x => x)
            .Must(x => x.From <= x.LastDay(timeProvider, businessTimeZone))
            .WithMessage(localizer.StockLevels_DailyTotals_From_NotAfterTo);

        RuleFor(x => x)
            .Must(x => x.LastDay(timeProvider, businessTimeZone).DayNumber - x.From.DayNumber < maxDays)
            .WithMessage(string.Format(CultureInfo.CurrentCulture, localizer.StockLevels_DailyTotals_Range_MaxDays, maxDays));
    }
}
