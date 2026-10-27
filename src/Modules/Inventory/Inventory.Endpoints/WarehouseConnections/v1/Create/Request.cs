using System.Globalization;
using Common.Application.Localization.Resources;
using Common.Application.Options;
using Common.Application.Validation;
using FluentValidation;
using Inventory.Domain.WarehouseConnections;
using Microsoft.Extensions.Options;

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
    public RequestValidator(IResxLocalizer localizer, IOptions<InventoryOptions> options)
    {
        var allowPrivateAddresses = options.Value.AllowPrivateWarehouseAddresses;

        RuleFor(x => x.StoreId)
            .NotEmpty()
            .WithMessage(localizer.WarehouseConnections_Create_StoreId_NotEmpty);

        RuleFor(x => x.Provider)
            .IsInEnum()
            .WithMessage(localizer.WarehouseConnections_Create_Provider_Invalid);

        // The address is typed in by the caller and called by this server. The outbound connection checks the addresses the
        // host resolves to again (WarehouseConnectGuard), so this is the early, readable half of the same policy.
        RuleFor(x => x.BaseUrl)
            .Cascade(CascadeMode.Stop)
            .NotNull()
            .WithMessage(localizer.WarehouseConnections_Create_BaseUrl_NotEmpty)
            .Must(url => url.OriginalString.Length <= Constants.BaseUrlMaxLength)
            .WithMessage(string.Format(CultureInfo.CurrentCulture,
                localizer.WarehouseConnections_Create_BaseUrl_MaximumLength, Constants.BaseUrlMaxLength))
            .Must(url => url.IsAbsoluteUri && url.Scheme == Uri.UriSchemeHttps)
            .WithMessage(localizer.WarehouseConnections_Create_BaseUrl_MustBeHttps)
            .Must(url => url.UserInfo.Length == 0 && url.Fragment.Length == 0)
            .WithMessage(localizer.WarehouseConnections_Create_BaseUrl_NoUserInfoOrFragment)
            .Must(url => allowPrivateAddresses || WarehouseAddressPolicy.IsPublicHost(url))
            .WithMessage(localizer.WarehouseConnections_Create_BaseUrl_AddressNotAllowed);

        RuleFor(x => x.ApiCredential)
            .NotEmpty()
            .WithMessage(localizer.WarehouseConnections_Create_ApiCredential_NotEmpty)
            .MaximumLength(Constants.ApiCredentialMaxLength)
            .WithMessage(string.Format(CultureInfo.CurrentCulture,
                localizer.WarehouseConnections_Create_ApiCredential_MaximumLength, Constants.ApiCredentialMaxLength));
    }
}
