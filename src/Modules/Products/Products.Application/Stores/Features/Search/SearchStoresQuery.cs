using Common.Application.CQS;
using Common.Application.Localization;
using Common.Application.Queries.Pagination;
using FluentValidation;
using Microsoft.Extensions.Localization;
using Products.Application.Stores.DTOs;

namespace Products.Application.Stores.Features.Search;

public sealed record SearchStoresQuery : PaginationQuery, IQuery<PaginationResult<StoreDto>>
{
    public string? Name { get; init; }
    public string? Description { get; init; }
    public string? Address { get; init; }
}

public sealed class SearchStoresQueryValidator : PaginationQueryValidator<SearchStoresQuery>
{
    public SearchStoresQueryValidator(IStringLocalizer<ResxLocalizer> localizer) : base(localizer)
    {
        RuleFor(x => x.Name)
            .MaximumLength(Domain.Stores.Constants.NameMaxLength)
                .WithMessage(localizer["Stores.Search.Name.MaximumLength {0}", Domain.Stores.Constants.NameMaxLength])
            .When(x => x.Name is not null);

        RuleFor(x => x.Description)
            .MaximumLength(Domain.Stores.Constants.DescriptionMaxLength)
                .WithMessage(localizer["Stores.Search.Description.MaximumLength {0}", Domain.Stores.Constants.DescriptionMaxLength])
            .When(x => x.Description is not null);

        RuleFor(x => x.Address)
            .MaximumLength(Domain.Stores.Constants.AddressMaxLength)
                .WithMessage(localizer["Stores.Search.Address.MaximumLength {0}", Domain.Stores.Constants.AddressMaxLength])
            .When(x => x.Address is not null);
    }
}
