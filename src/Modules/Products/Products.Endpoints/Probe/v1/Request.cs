using Common.Application.Localization.Resources;
using Common.Application.Validation;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace Products.Endpoints.Probe.v1;

public sealed record Request
{
    [FromQuery]
    public int Count { get; init; }
}

public sealed class RequestValidator : CustomValidator<Request>
{
    public RequestValidator(IResxLocalizer localizer)
    {
        RuleFor(x => x.Count)
            .GreaterThan(0)
            .WithMessage(localizer.Products_v1_Probe_Count_GreaterThan);
    }
}
