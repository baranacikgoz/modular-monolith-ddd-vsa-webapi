using System.Globalization;
using System.Net;
using System.Reflection;
using Common.Application.Localization.Resources;
using Common.Domain.ResultMonad;
using Common.Tests;
using Inventory.Application.Gateway;
using Inventory.Domain.WarehouseConnections.Errors;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;
using Xunit;

namespace Inventory.Tests.Gateway;

/// <summary>
///     An error key without a resource text reaches the client as the bare key. Every error a gateway call or a warehouse
///     connection can answer with needs a text in both languages, and so does the parameter name it is about.
/// </summary>
[Collection("IntegrationTestCollection")]
public class WarehouseErrorLocalizationTests(IntegrationTestWebAppFactory factory) : BaseIntegrationTest(factory)
{
    public static TheoryData<string> Cultures => new() { "tr", "en" };

    private static IEnumerable<Error> Errors()
    {
        var declared = new[] { typeof(WarehouseGatewayErrors), typeof(WarehouseConnectionErrors) }
            .SelectMany(type => type.GetFields(BindingFlags.Public | BindingFlags.Static))
            .Where(field => field.FieldType == typeof(Error))
            .Select(field => (Error)field.GetValue(null)!);

        return declared.Concat([WarehouseGatewayErrors.CallFailed(HttpStatusCode.InternalServerError), WarehouseGatewayErrors.Throttled(1)]);
    }

    [Theory]
    [MemberData(nameof(Cultures))]
    public void EveryWarehouseError_HasATextInBothLanguages(string culture)
    {
        var localizer = Scope.ServiceProvider.GetRequiredService<IStringLocalizer<ResxLocalizer>>();
        var previous = CultureInfo.CurrentUICulture;
        CultureInfo.CurrentUICulture = CultureInfo.GetCultureInfo(culture);
        try
        {
            var errors = Errors().ToList();
            Assert.NotEmpty(errors);
            Assert.True(localizer["NoSuchResourceKey"].ResourceNotFound, "The localizer must report a missing text, or this test proves nothing.");

            var missing = errors
                .SelectMany(error => new[] { error.Key, error.ParameterName })
                .OfType<string>()
                .Where(name => localizer[name].ResourceNotFound)
                .Distinct()
                .ToList();

            Assert.True(missing.Count == 0, $"No '{culture}' text for: {string.Join(", ", missing)}.");
        }
        finally
        {
            CultureInfo.CurrentUICulture = previous;
        }
    }
}
