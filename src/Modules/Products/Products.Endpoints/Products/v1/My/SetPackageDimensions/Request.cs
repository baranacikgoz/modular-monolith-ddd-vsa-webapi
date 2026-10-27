using System.Globalization;
using Common.Application.Localization.Resources;
using Common.Application.ModelBinders;
using Common.Application.Validation;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Products.Domain.Products;

namespace Products.Endpoints.Products.v1.My.SetPackageDimensions;

public sealed record Request
{
    [FromRoute]
    [ModelBinder<StronglyTypedIdBinder<ProductId>>]
    public required ProductId Id { get; init; }

    [FromBody] public required RequestBody Body { get; init; }

    public sealed record RequestBody
    {
        public required decimal LengthCm { get; init; }
        public required decimal WidthCm { get; init; }
        public required decimal HeightCm { get; init; }
        public required decimal WeightKg { get; init; }
    }
}

public sealed class RequestValidator : CustomValidator<Request>
{
    public RequestValidator(IResxLocalizer localizer)
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage(localizer.Products_SetPackageDimensions_Id_NotEmpty);

        RuleFor(x => x.Body)
            .NotEmpty()
            .WithMessage(localizer.Products_SetPackageDimensions_Body_NotEmpty)
            .SetValidator(new RequestBodyValidator(localizer));
    }
}

public sealed class RequestBodyValidator : CustomValidator<Request.RequestBody>
{
    public RequestBodyValidator(IResxLocalizer localizer)
    {
        RuleFor(x => x.LengthCm)
            .Must(value => value > 0 && value <= Constants.PackageSideMaxCm)
            .WithMessage(string.Format(CultureInfo.CurrentCulture, localizer.Products_SetPackageDimensions_LengthCm_Range,
                Constants.PackageSideMaxCm));

        RuleFor(x => x.WidthCm)
            .Must(value => value > 0 && value <= Constants.PackageSideMaxCm)
            .WithMessage(string.Format(CultureInfo.CurrentCulture, localizer.Products_SetPackageDimensions_WidthCm_Range,
                Constants.PackageSideMaxCm));

        RuleFor(x => x.HeightCm)
            .Must(value => value > 0 && value <= Constants.PackageSideMaxCm)
            .WithMessage(string.Format(CultureInfo.CurrentCulture, localizer.Products_SetPackageDimensions_HeightCm_Range,
                Constants.PackageSideMaxCm));

        RuleFor(x => x.WeightKg)
            .Must(value => value > 0 && value <= Constants.PackageWeightMaxKg)
            .WithMessage(string.Format(CultureInfo.CurrentCulture, localizer.Products_SetPackageDimensions_WeightKg_Range,
                Constants.PackageWeightMaxKg));
    }
}
