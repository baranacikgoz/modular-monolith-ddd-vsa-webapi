using System.Globalization;
using System.Text.Json.Serialization;
using Common.Application.JsonConverters;
using Common.Application.Localization.Resources;
using Common.Application.ModelBinders;
using Common.Application.Validation;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
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
    public RequestValidator(IResxLocalizer localizer)
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage(localizer.Stores_v1_AddProduct_StoreId_NotEmpty);

        RuleFor(x => x.Body)
            .NotEmpty()
            .WithMessage(localizer.Stores_v1_AddProduct_ProductBody_NotEmpty)
            .SetValidator(new RequestBodyValidator(localizer));
    }
}

public sealed class RequestBodyValidator : CustomValidator<Request.RequestBody>
{
    public RequestBodyValidator(IResxLocalizer localizer)
    {
        RuleFor(x => x.ProductTemplateId)
            .NotEmpty()
            .WithMessage(localizer.Stores_AddProduct_ProductTemplateId_NotEmpty);

        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage(localizer.Stores_AddProduct_Name_NotEmpty)
            .MaximumLength(Constants.NameMaxLength)
            .WithMessage(string.Format(CultureInfo.CurrentCulture, localizer.Stores_AddProduct_Name_MaxLength,
                Constants.NameMaxLength));

        RuleFor(x => x.Description)
            .NotEmpty()
            .WithMessage(localizer.Stores_AddProduct_Description_NotEmpty)
            .MaximumLength(Constants.DescriptionMaxLength)
            .WithMessage(string.Format(CultureInfo.CurrentCulture, localizer.Stores_AddProduct_Description_MaxLength,
                Constants.DescriptionMaxLength));

        RuleFor(x => x.Quantity)
            .GreaterThanOrEqualTo(Constants.QuantityGreaterThanOrEqualTo)
            .WithMessage(string.Format(CultureInfo.CurrentCulture,
                localizer.Stores_AddProduct_Quantity_GreaterThanOrEqualTo,
                Constants.QuantityGreaterThanOrEqualTo));

        RuleFor(x => x.Price)
            .GreaterThan(Constants.PriceGreaterThanOrEqualTo)
            .WithMessage(string.Format(CultureInfo.CurrentCulture, localizer.Stores_AddProduct_Price_GreaterThanOrEqualTo,
                Constants.PriceGreaterThanOrEqualTo));
    }
}
