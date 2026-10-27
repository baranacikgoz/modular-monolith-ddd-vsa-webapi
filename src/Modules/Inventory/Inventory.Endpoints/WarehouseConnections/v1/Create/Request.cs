using System.Globalization;
using Common.Application.Localization.Resources;
using Common.Application.Validation;
using FluentValidation;
using Inventory.Domain.WarehouseConnections;

namespace Inventory.Endpoints.WarehouseConnections.v1.Create;

public sealed record Request
{
    public required DefaultIdType StoreId { get; init; }
    public required WarehouseProvider Provider { get; init; }

    /// <summary>Absolute https address of the warehouse system's API.</summary>
    public required Uri BaseUrl { get; init; }

    /// <summary>The credential this system presents to the warehouse. Stored encrypted, never returned.</summary>
    public required string ApiCredential { get; init; }
}

public sealed class RequestValidator : CustomValidator<Request>
{
    public RequestValidator(IResxLocalizer localizer)
    {
        RuleFor(x => x.StoreId)
            .NotEmpty()
            .WithMessage(localizer.WarehouseConnections_Create_StoreId_NotEmpty);

        RuleFor(x => x.Provider)
            .IsInEnum()
            .WithMessage(localizer.WarehouseConnections_Create_Provider_Invalid);

        RuleFor(x => x.BaseUrl)
            .NotNull()
            .WithMessage(localizer.WarehouseConnections_Create_BaseUrl_NotEmpty)
            .Must(url => url.OriginalString.Length <= Constants.BaseUrlMaxLength)
            .WithMessage(string.Format(CultureInfo.CurrentCulture,
                localizer.WarehouseConnections_Create_BaseUrl_MaximumLength, Constants.BaseUrlMaxLength))
            .Must(BeAnAbsoluteHttpsUrl)
            .WithMessage(localizer.WarehouseConnections_Create_BaseUrl_MustBeHttps);

        RuleFor(x => x.ApiCredential)
            .NotEmpty()
            .WithMessage(localizer.WarehouseConnections_Create_ApiCredential_NotEmpty)
            .MaximumLength(Constants.ApiCredentialMaxLength)
            .WithMessage(string.Format(CultureInfo.CurrentCulture,
                localizer.WarehouseConnections_Create_ApiCredential_MaximumLength, Constants.ApiCredentialMaxLength));
    }

    private static bool BeAnAbsoluteHttpsUrl(Uri baseUrl)
    {
        return baseUrl.IsAbsoluteUri && baseUrl.Scheme == Uri.UriSchemeHttps;
    }
}
