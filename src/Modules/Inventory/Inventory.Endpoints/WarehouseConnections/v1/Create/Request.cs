using System.Globalization;
using Common.Application.Localization.Resources;
using Common.Application.Options;
using Common.Application.Validation;
using FluentValidation;
using Inventory.Domain.WarehouseConnections;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;

namespace Inventory.Endpoints.WarehouseConnections.v1.Create;

public sealed record Request
{
    public required DefaultIdType StoreId { get; init; }
    public required WarehouseProvider Provider { get; init; }

    /// <summary>Absolute https address of the warehouse system's API. Required for every provider except <c>Dummy</c>, which ignores it.</summary>
    public Uri? BaseUrl { get; init; }

    /// <summary>The credential this system presents to the warehouse. Stored encrypted, never returned. Required for every provider except <c>Dummy</c>, which ignores it.</summary>
    public string? ApiCredential { get; init; }
}

public sealed class RequestValidator : CustomValidator<Request>
{
    public RequestValidator(IResxLocalizer localizer, IOptions<InventoryOptions> options, IHostEnvironment environment)
    {
        var allowPrivateAddresses = options.Value.AllowPrivateWarehouseAddresses;
        var isProduction = environment.IsProduction();

        RuleFor(x => x.StoreId)
            .NotEmpty()
            .WithMessage(localizer.WarehouseConnections_Create_StoreId_NotEmpty);

        RuleFor(x => x.Provider)
            .IsInEnum()
            .WithMessage(localizer.WarehouseConnections_Create_Provider_Invalid);

        // The dummy provider answers from memory and never reaches the warehouse it pretends to talk to, so a production
        // connection using it would silently drop every release and stock push.
        RuleFor(x => x.Provider)
            .Must(provider => !isProduction || provider != WarehouseProvider.Dummy)
            .WithMessage(localizer.WarehouseConnections_Create_Provider_DummyNotAllowedInProduction);

        // The address is typed in by the caller and called by this server. The outbound connection checks the addresses the
        // host resolves to again (WarehouseConnectGuard), so this is the early, readable half of the same policy.
        RuleFor(x => x.BaseUrl)
            .Cascade(CascadeMode.Stop)
            .NotNull()
            .WithMessage(localizer.WarehouseConnections_Create_BaseUrl_NotEmpty)
            .Must(url => url!.OriginalString.Length <= Constants.BaseUrlMaxLength)
            .WithMessage(string.Format(CultureInfo.CurrentCulture,
                localizer.WarehouseConnections_Create_BaseUrl_MaximumLength, Constants.BaseUrlMaxLength))
            .Must(url => url!.IsAbsoluteUri && url.Scheme == Uri.UriSchemeHttps)
            .WithMessage(localizer.WarehouseConnections_Create_BaseUrl_MustBeHttps)
            .Must(url => url!.UserInfo.Length == 0 && url.Fragment.Length == 0)
            .WithMessage(localizer.WarehouseConnections_Create_BaseUrl_NoUserInfoOrFragment)
            .Must(url => allowPrivateAddresses || WarehouseAddressPolicy.IsPublicHost(url!))
            .WithMessage(localizer.WarehouseConnections_Create_BaseUrl_AddressNotAllowed)
            .When(x => x.Provider != WarehouseProvider.Dummy);

        RuleFor(x => x.ApiCredential)
            .NotEmpty()
            .WithMessage(localizer.WarehouseConnections_Create_ApiCredential_NotEmpty)
            .MaximumLength(Constants.ApiCredentialMaxLength)
            .WithMessage(string.Format(CultureInfo.CurrentCulture,
                localizer.WarehouseConnections_Create_ApiCredential_MaximumLength, Constants.ApiCredentialMaxLength))
            .When(x => x.Provider != WarehouseProvider.Dummy);
    }
}
