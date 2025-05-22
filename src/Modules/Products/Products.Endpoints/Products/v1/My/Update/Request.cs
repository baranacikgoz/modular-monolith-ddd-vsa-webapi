using Common.Application.Localization;
using Common.Application.ModelBinders;
using Common.Application.Validation;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Products.Domain.Products;

namespace Products.Endpoints.Products.v1.My.Update;

public sealed record Request
{
    [FromRoute, ModelBinder<StronglyTypedIdBinder<ProductId>>]
    public required ProductId Id { get; init; }

    [FromBody]
    public required RequestBody Body { get; init; }

    public class RequestBody
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public decimal? Price { get; set; }
        public int? Quantity { get; set; }
    }

    public Request()
    {
    }
}

public class RequestValidator : CustomValidator<Request>
{
    public RequestValidator(IStringLocalizer<ResxLocalizer> localizer)
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage(localizer["Products.Update.ProductId.NotEmpty"]);

        RuleFor(x => x.Body)
            .NotEmpty()
            .WithMessage(localizer["Products.Update.ProductBody.NotEmpty"])
            .SetValidator(new RequestBodyValidator(localizer));
    }
}

public class RequestBodyValidator : CustomValidator<Request.RequestBody>
{
    public RequestBodyValidator(IStringLocalizer<ResxLocalizer> localizer)
    {
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
