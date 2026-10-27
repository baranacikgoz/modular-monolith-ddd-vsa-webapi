using Common.Application.Localization.Resources;
using Common.Application.Options;
using Common.Tests;
using Inventory.Domain.WarehouseConnections;
using Inventory.Endpoints.WarehouseConnections.v1.Create;
using Inventory.Tests.Support;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Xunit;

namespace Inventory.Tests.Endpoints.WarehouseConnections;

/// <summary>
///     The early half of the address policy: what the create request accepts as a base address. The outbound half, which
///     checks where the host really resolves, is covered by <c>WarehouseConnectGuardTests</c>.
/// </summary>
[Collection("IntegrationTestCollection")]
public class CreateRequestValidatorTests(IntegrationTestWebAppFactory factory) : BaseIntegrationTest(factory)
{
    private IResxLocalizer Localizer => Scope.ServiceProvider.GetRequiredService<IResxLocalizer>();

    private RequestValidator ValidatorOf(bool allowPrivateAddresses = false)
    {
        return new RequestValidator(
            Localizer,
            Options.Create(TestInventoryOptions.Create(configure: o => o.AllowPrivateWarehouseAddresses = allowPrivateAddresses)));
    }

    private static Request RequestOf(string address)
    {
        return new Request
        {
            StoreId = DefaultIdType.CreateVersion7(),
            Provider = WarehouseProvider.JsonApi,
            BaseUrl = new Uri(address, UriKind.RelativeOrAbsolute),
            ApiCredential = WarehouseConnectionTestData.ApiCredential
        };
    }

    [Theory]
    [InlineData("https://warehouse.example-partner.local/api")]
    [InlineData("https://8.8.8.8/api")]
    [InlineData("https://[2606:4700:4700::1111]:8443/api")]
    public void Validate_PublicHttpsAddress_IsValid(string address)
    {
        var result = ValidatorOf().Validate(RequestOf(address));

        Assert.True(result.IsValid, string.Join("; ", result.Errors.Select(e => e.ErrorMessage)));
    }

    [Theory]
    [InlineData("http://warehouse.example-partner.local/api")]
    [InlineData("ftp://warehouse.example-partner.local/api")]
    [InlineData("api/releases")]
    public void Validate_NotAnAbsoluteHttpsAddress_ReportsMustBeHttps(string address)
    {
        var result = ValidatorOf().Validate(RequestOf(address));

        var error = Assert.Single(result.Errors);
        Assert.Equal(Localizer.WarehouseConnections_Create_BaseUrl_MustBeHttps, error.ErrorMessage);
    }

    [Theory]
    [InlineData("https://user:secret@warehouse.example-partner.local/api")]
    [InlineData("https://user@warehouse.example-partner.local/api")]
    [InlineData("https://warehouse.example-partner.local/api#section")]
    public void Validate_UserInfoOrFragment_ReportsNoUserInfoOrFragment(string address)
    {
        var result = ValidatorOf().Validate(RequestOf(address));

        var error = Assert.Single(result.Errors);
        Assert.Equal(Localizer.WarehouseConnections_Create_BaseUrl_NoUserInfoOrFragment, error.ErrorMessage);
    }

    [Theory]
    [InlineData("https://localhost/api")]
    [InlineData("https://LOCALHOST:8443/api")]
    [InlineData("https://stub.localhost/api")]
    [InlineData("https://127.0.0.1/api")]
    [InlineData("https://2130706433/api")]
    [InlineData("https://0.0.0.0/api")]
    [InlineData("https://10.0.0.5/api")]
    [InlineData("https://172.16.4.4/api")]
    [InlineData("https://192.168.1.10/api")]
    [InlineData("https://169.254.169.254/latest/meta-data")]
    [InlineData("https://[::1]/api")]
    [InlineData("https://[fe80::1]/api")]
    [InlineData("https://[fd00::1]/api")]
    [InlineData("https://[::ffff:10.0.0.1]/api")]
    public void Validate_NonPublicHost_ReportsAddressNotAllowed(string address)
    {
        var result = ValidatorOf().Validate(RequestOf(address));

        var error = Assert.Single(result.Errors);
        Assert.Equal(Localizer.WarehouseConnections_Create_BaseUrl_AddressNotAllowed, error.ErrorMessage);
    }

    // A developer's stub lives on the local machine: the switch lifts the address rule only, https and the shape still apply.
    [Fact]
    public void Validate_AllowPrivateAddresses_AcceptsALocalStubButStillRequiresHttps()
    {
        var validator = ValidatorOf(allowPrivateAddresses: true);

        Assert.True(validator.Validate(RequestOf("https://localhost:5001/api")).IsValid);
        Assert.True(validator.Validate(RequestOf("https://10.0.0.5/api")).IsValid);
        Assert.False(validator.Validate(RequestOf("http://localhost:5001/api")).IsValid);
        Assert.False(validator.Validate(RequestOf("https://user:secret@localhost/api")).IsValid);
    }
}
