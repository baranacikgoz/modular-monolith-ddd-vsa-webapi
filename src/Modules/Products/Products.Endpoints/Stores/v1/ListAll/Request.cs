using Common.Application.Localization.Resources;
using Common.Application.Pagination;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Products.Domain.Stores;

namespace Products.Endpoints.Stores.v1.ListAll;

public sealed record Request : PaginationRequest
{
    [FromQuery] public StoreStatus? Status { get; init; }
}

public sealed class RequestValidator : PaginationRequestValidator<Request>
{
    public RequestValidator(IResxLocalizer localizer) : base(localizer)
    {
        RuleFor(x => x.Status)
            .IsInEnum()
            .WithMessage(localizer.Stores_ListAll_Status_Invalid)
            .When(x => x.Status is not null);
    }
}
