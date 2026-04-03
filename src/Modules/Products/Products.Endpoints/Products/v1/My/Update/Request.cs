using System.Globalization;
using Common.Application.Localization.Resources;
using Common.Application.ModelBinders;
using Common.Application.Validation;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Products.Domain.Products;

namespace Products.Endpoints.Products.v1.My.Update;

public sealed record Request
{
    [FromRoute]
    [ModelBinder<StronglyTypedIdBinder<ProductId>>]
    public required ProductId Id { get; init; }

    [FromBody] public required RequestBody Body { get; init; }

    public class RequestBody
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public decimal? Price { get; set; }
        public int? Quantity { get; set; }
    }
}

public class RequestValidator : CustomValidator<Request>
{
    public RequestValidator(IResxLocalizer localizer)
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage(localizer.Products_Update_ProductId_NotEmpty);

        RuleFor(x => x.Body)
            .NotEmpty()
            .WithMessage(localizer.Products_Update_ProductBody_NotEmpty)
            .SetValidator(new RequestBodyValidator(localizer));
    }
}

public class RequestBodyValidator : CustomValidator<Request.RequestBody>
{
    public RequestBodyValidator(IResxLocalizer localizer)
    {
        RuleFor(x => x)
            .Must(x => !string.IsNullOrEmpty(x.Name) || !string.IsNullOrEmpty(x.Description) || x.Quantity.HasValue ||
                       x.Price.HasValue)
            .WithMessage(localizer.Products_Update_AtLeastOnePropertyIsRequired);

        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage(localizer.Products_Update_Name_NotEmpty)
            .MaximumLength(Constants.NameMaxLength)
            .WithMessage(string.Format(CultureInfo.CurrentCulture, localizer.Products_Update_Name_MaxLength,
                Constants.NameMaxLength))
            .When(x => !string.IsNullOrEmpty(x.Name));

        RuleFor(x => x.Description)
            .NotEmpty()
            .WithMessage(localizer.Products_Update_Description_NotEmpty)
            .MaximumLength(Constants.DescriptionMaxLength)
            .WithMessage(string.Format(CultureInfo.CurrentCulture, localizer.Products_Update_Description_MaxLength,
                Constants.DescriptionMaxLength))
            .When(x => !string.IsNullOrEmpty(x.Description));

        RuleFor(x => x.Quantity)
            .GreaterThanOrEqualTo(Constants.QuantityGreaterThanOrEqualTo)
            .WithMessage(string.Format(CultureInfo.CurrentCulture,
                localizer.Products_Update_Quantity_GreaterThanOrEqualTo,
                Constants.QuantityGreaterThanOrEqualTo))
            .When(x => x.Quantity is not null);

        RuleFor(x => x.Price)
            .GreaterThan(Constants.PriceGreaterThanOrEqualTo)
            .WithMessage(string.Format(CultureInfo.CurrentCulture, localizer.Products_Update_Price_GreaterThanOrEqualTo,
                Constants.PriceGreaterThanOrEqualTo))
            .When(x => x.Price is not null);
    }
}
