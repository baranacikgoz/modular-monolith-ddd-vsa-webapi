using System.Text.Json.Serialization;
using Common.Application.JsonConverters;
using Common.Application.Localization;
using Common.Application.ModelBinders;
using Common.Application.Validation;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Products.Domain.ProductTemplates;
using Products.Domain.Stores;
using Constants = Products.Domain.Products.Constants;

namespace Products.Endpoints.Stores.v1.AddProduct;

public sealed record Request
{
    [FromRoute]
    [ModelBinder<StronglyTypedIdBinder<StoreId>>]
    public required StoreId Id { get; init; }

    [FromBody] public required RequestBody Body { get; init; }

    public class RequestBody
    {
        [JsonConverter(typeof(StronglyTypedIdReadOnlyJsonConverter<ProductTemplateId>))]
        public required ProductTemplateId ProductTemplateId { get; init; }

        public required string Name { get; init; }
        public required string Description { get; init; }
        public required int Quantity { get; init; }
        public required decimal Price { get; init; }
    }
}

public sealed class RequestValidator : CustomValidator<Request>
{
    public RequestValidator(IStringLocalizer<ResxLocalizer> localizer)
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage(localizer["Stores.v1.AddProduct.StoreId.NotEmpty"]);

        RuleFor(x => x.Body)
            .NotEmpty()
            .WithMessage(localizer["Stores.v1.AddProduct.ProductBody.NotEmpty"])
            .SetValidator(new RequestBodyValidator(localizer));
    }
}

public sealed class RequestBodyValidator : CustomValidator<Request.RequestBody>
{
    public RequestBodyValidator(IStringLocalizer<ResxLocalizer> localizer)
    {
        RuleFor(x => x.ProductTemplateId)
            .NotEmpty()
            .WithMessage(localizer["Stores.AddProduct.ProductTemplateId.NotEmpty"]);

        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage(localizer["Stores.AddProduct.Name.NotEmpty"])
            .MaximumLength(Constants.NameMaxLength)
            .WithMessage(localizer["Stores.AddProduct.Name.MaxLength {0}", Constants.NameMaxLength]);

        RuleFor(x => x.Description)
            .NotEmpty()
            .WithMessage(localizer["Stores.AddProduct.Description.NotEmpty"])
            .MaximumLength(Constants.DescriptionMaxLength)
            .WithMessage(localizer["Stores.AddProduct.Description.MaxLength {0}", Constants.DescriptionMaxLength]);

        RuleFor(x => x.Quantity)
            .GreaterThanOrEqualTo(Constants.QuantityGreaterThanOrEqualTo)
            .WithMessage(localizer["Stores.AddProduct.Quantity.GreaterThanOrEqualTo {0}",
                Constants.QuantityGreaterThanOrEqualTo]);

        RuleFor(x => x.Price)
            .GreaterThan(Constants.PriceGreaterThanOrEqualTo)
            .WithMessage(localizer["Stores.AddProduct.Price.GreaterThanOrEqualTo {0}",
                Constants.PriceGreaterThanOrEqualTo]);
    }
}
