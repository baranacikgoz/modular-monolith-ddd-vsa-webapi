using System.Globalization;
using Common.Application.Localization.Resources;
using Common.Application.ModelBinders;
using Common.Application.Validation;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Products.Domain.Products;

namespace Products.Endpoints.Products.v1.My.SetAttributes;

public sealed record Request
{
    [FromRoute]
    [ModelBinder<StronglyTypedIdBinder<ProductId>>]
    public required ProductId Id { get; init; }

    [FromBody] public required RequestBody Body { get; init; }

    public sealed record RequestBody
    {
        public required IReadOnlyList<AttributeItem> Attributes { get; init; }
    }

    public sealed record AttributeItem
    {
        public required string Name { get; init; }
        public required string Value { get; init; }
    }
}

public sealed class RequestValidator : CustomValidator<Request>
{
    public RequestValidator(IResxLocalizer localizer)
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage(localizer.Products_SetAttributes_Id_NotEmpty);

        RuleFor(x => x.Body)
            .NotEmpty()
            .WithMessage(localizer.Products_SetAttributes_Body_NotEmpty)
            .SetValidator(new RequestBodyValidator(localizer));
    }
}

// The number of attributes and the duplicate names are rules of the product, checked by SetAttributes.
public sealed class RequestBodyValidator : CustomValidator<Request.RequestBody>
{
    public RequestBodyValidator(IResxLocalizer localizer)
    {
        RuleFor(x => x.Attributes)
            .NotNull()
            .WithMessage(localizer.Products_SetAttributes_Attributes_NotNull);

        RuleForEach(x => x.Attributes)
            .ChildRules(attribute =>
            {
                attribute.RuleFor(a => a.Name)
                    .NotEmpty()
                    .WithMessage(localizer.Products_SetAttributes_Name_NotEmpty)
                    .MaximumLength(Constants.AttributeNameMaxLength)
                    .WithMessage(string.Format(CultureInfo.CurrentCulture, localizer.Products_SetAttributes_Name_MaxLength,
                        Constants.AttributeNameMaxLength));

                attribute.RuleFor(a => a.Value)
                    .NotEmpty()
                    .WithMessage(localizer.Products_SetAttributes_Value_NotEmpty)
                    .MaximumLength(Constants.AttributeValueMaxLength)
                    .WithMessage(string.Format(CultureInfo.CurrentCulture, localizer.Products_SetAttributes_Value_MaxLength,
                        Constants.AttributeValueMaxLength));
            })
            .When(x => x.Attributes is not null);
    }
}
