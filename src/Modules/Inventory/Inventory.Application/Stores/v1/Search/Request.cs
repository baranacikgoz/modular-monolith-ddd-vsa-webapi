using System.Text.Json.Serialization;
using Common.Application.JsonConverters;
using Common.Application.Localization;
using Common.Application.Pagination;
using Common.Application.Validation;
using Common.Domain.StronglyTypedIds;
using FluentValidation;
using Inventory.Domain.Stores;
using Microsoft.Extensions.Localization;

namespace Inventory.Application.Stores.v1.Search;

public sealed record Request(string? Name, string? Description, int PageNumber, int PageSize)
    : PaginationRequest(PageNumber, PageSize);

public sealed class RequestValidator : PaginationRequestValidator<Request>
{
    public RequestValidator(IStringLocalizer<ResxLocalizer> localizer) : base(localizer)
    {
        RuleFor(x => x.Name)
            .MaximumLength(Domain.Stores.Constants.NameMaxLength)
                .WithMessage(localizer["Stores.v1.Search.Name.MaximumLength", Domain.Stores.Constants.NameMaxLength])
            .When(x => x.Name is not null);

        RuleFor(x => x.Description)
            .MaximumLength(Domain.Stores.Constants.DescriptionMaxLength)
                .WithMessage(localizer["Stores.v1.Search.Description.MaximumLength", Domain.Stores.Constants.DescriptionMaxLength])
            .When(x => x.Description is not null);
    }
}
