using Common.Application.CQS;
using Common.Application.Localization;
using Common.Application.Validation;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Localization;
using Products.Application.Products.DTOs;
using Products.Domain.Products;

namespace Products.Application.Products.Features.GetById;

public sealed record GetProductByIdQuery(ProductId Id) : IQuery<ProductDto>;

public sealed class GetProductByIdQueryValidator : CustomValidator<GetProductByIdQuery>
{
    public GetProductByIdQueryValidator(IStringLocalizer<ResxLocalizer> localizer)
    {
        RuleFor(x => x.Id)
            .NotEmpty()
                .WithMessage(localizer["Products.GetById.Id.NotEmpty"]);
    }
}
