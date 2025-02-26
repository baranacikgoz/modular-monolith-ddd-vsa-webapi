using System.Linq.Expressions;
using Common.Application.CQS;
using Common.Application.Localization;
using Common.Application.Validation;
using FluentValidation;
using Microsoft.Extensions.Localization;
using Products.Domain.Products;

namespace Products.Application.Products.Features.GetById;

public sealed record GetProductByIdQuery<TDto>(ProductId Id) : IQuery<TDto>
{
    public required Expression<Func<Product, TDto>> Selector { get; init; }
    public Expression<Func<Product, bool>>? EnsureOwnership { get; init; }
}

public sealed class GetProductByIdQueryValidator<TDto> : CustomValidator<GetProductByIdQuery<TDto>>
{
    public GetProductByIdQueryValidator(IStringLocalizer<ResxLocalizer> localizer)
    {
        RuleFor(x => x.Id)
            .NotEmpty()
                .WithMessage(localizer["Products.GetById.Id.NotEmpty"]);
    }
}
