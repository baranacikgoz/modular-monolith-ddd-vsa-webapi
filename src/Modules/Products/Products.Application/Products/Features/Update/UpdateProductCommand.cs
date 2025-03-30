using System.Linq.Expressions;
using Common.Application.CQS;
using Common.Application.Validation;
using FluentValidation;
using Microsoft.Extensions.Localization;
using Products.Domain.Products;

namespace Products.Application.Products.Features.Update;

public sealed record UpdateProductCommand(ProductId Id, string? Name, string? Description, int? Quantity, decimal? Price) : ICommand
{
    /// <summary>
    /// To prevent somebody from updating a product that does not belong to them.
    /// </summary>
    public Expression<Func<Product, bool>>? EnsureOwnership { get; init; }
}

public class UpdateProductCommandValidator : CustomValidator<UpdateProductCommand>
{
    public UpdateProductCommandValidator(IStringLocalizer<UpdateProductCommandValidator> localizer)
    {
        RuleFor(x => x.Id)
            .NotEmpty()
                .WithMessage(localizer["Products.Update.ProductId.NotEmpty"]);

        RuleFor(x => x)
           .Must(x => (!string.IsNullOrEmpty(x.Name)) || (!string.IsNullOrEmpty(x.Description)) || x.Quantity.HasValue || x.Price.HasValue)
               .WithMessage(localizer["Products.Update.AtLeastOnePropertyIsRequired"]);

        RuleFor(x => x.Name)
            .NotEmpty()
                .WithMessage(localizer["Products.Update.Name.NotEmpty"])
            .MaximumLength(Domain.Products.Constants.NameMaxLength)
                .WithMessage(localizer["Products.Update.Name.MaxLength {0}", Domain.Products.Constants.NameMaxLength])
            .When(x => !string.IsNullOrEmpty(x.Name));

        RuleFor(x => x.Description)
            .NotEmpty()
                .WithMessage(localizer["Products.Update.Description.NotEmpty"])
            .MaximumLength(Domain.Products.Constants.DescriptionMaxLength)
                .WithMessage(localizer["Products.Update.Description.MaxLength {0}", Domain.Products.Constants.DescriptionMaxLength])
            .When(x => !string.IsNullOrEmpty(x.Description));

        RuleFor(x => x.Quantity)
            .GreaterThanOrEqualTo(Domain.Products.Constants.QuantityGreaterThanOrEqualTo)
                .WithMessage(localizer["Products.Update.Quantity.GreaterThanOrEqualTo {0}", Domain.Products.Constants.QuantityGreaterThanOrEqualTo])
            .When(x => x.Quantity is not null);

        RuleFor(x => x.Price)
            .GreaterThan(Domain.Products.Constants.PriceGreaterThanOrEqualTo)
                .WithMessage(localizer["Products.Update.Price.GreaterThanOrEqualTo {0}", Domain.Products.Constants.PriceGreaterThanOrEqualTo])
            .When(x => x.Price is not null);
    }
}
