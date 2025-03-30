using Products.Domain.Stores;
using Products.Application.Stores.DTOs;
using Common.Application.Validation;
using Microsoft.Extensions.Localization;
using Common.Application.Localization;
using FluentValidation;
using Common.Application.CQS;
using Products.Domain.Products;
using System.Linq.Expressions;

namespace Products.Application.Stores.Features.GetById;

public sealed record GetStoreByIdQuery(StoreId Id) : IQuery<StoreDto>
{
    public Expression<Func<Store, bool>>? EnsureOwnership { get; init; }
}

public sealed class GetStoreByIdQueryValidator : CustomValidator<GetStoreByIdQuery>
{
    public GetStoreByIdQueryValidator(IStringLocalizer<ResxLocalizer> localizer)
    {
        RuleFor(x => x.Id)
            .NotEmpty()
                .WithMessage(localizer["Stores.GetById.Id.NotEmpty"]);
    }
}
