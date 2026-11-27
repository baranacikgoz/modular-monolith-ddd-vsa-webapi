using System.Text.Json;

namespace Host.Tests;

// Each test boots its own host with the modules that Configurations/modules.json enables, the set that is deployed.
// The template's "all modules" set is not what runs: a product that adds modules of its own registers routes that
// collide with the template's, and the document of that deployed set is the contract.
// Must run in the "Host" collection to avoid a parallel factory boot (static state corruption).
[Collection("Host")]
public class SwaggerContractTests
{
    private static readonly HashSet<string> OperationMethods =
        ["get", "put", "post", "delete", "options", "head", "patch", "trace"];

    private static readonly Lazy<Task<string>> DocumentBody = new(FetchDocumentBodyAsync);

    [Fact]
    public async Task SwaggerDocument_Request_IsGenerated()
    {
        using var document = await GetDocumentAsync();

        Assert.True(document.RootElement.GetProperty("paths").EnumerateObject().Any());
    }

    [Fact]
    public async Task SwaggerDocument_SchemaIds_ContainNoAssemblyQualifiedNames()
    {
        using var document = await GetDocumentAsync();

        var offenders = document.RootElement.GetProperty("components").GetProperty("schemas").EnumerateObject()
            .Select(schema => schema.Name)
            .Where(id => id.Contains("Culture=", StringComparison.Ordinal))
            .ToList();

        Assert.True(
            offenders.Count == 0,
            $"{offenders.Count} schema ids carry an assembly name, for example: {string.Join(" | ", offenders.Take(3))}");
    }

    [Fact]
    public async Task SwaggerDocument_Enums_AreDescribedAsStrings()
    {
        using var document = await GetDocumentAsync();

        var offenders = document.RootElement.GetProperty("components").GetProperty("schemas").EnumerateObject()
            .Where(schema => schema.Value.TryGetProperty("enum", out _)
                             && !(schema.Value.TryGetProperty("type", out var type) && type.GetString() == "string"))
            .Select(schema => schema.Name)
            .ToList();

        Assert.True(
            offenders.Count == 0,
            $"{offenders.Count} enum schemas are not strings, for example: {string.Join(" | ", offenders.Take(3))}");
    }

    [Fact]
    public async Task SwaggerDocument_Operations_HaveUniqueOperationIds()
    {
        using var document = await GetDocumentAsync();

        var ids = document.RootElement.GetProperty("paths").EnumerateObject()
            .SelectMany(path => path.Value.EnumerateObject().Where(method => OperationMethods.Contains(method.Name)))
            .Select(operation => operation.Value.TryGetProperty("operationId", out var id) ? id.GetString() : null)
            .ToList();

        Assert.DoesNotContain(null, ids);
        Assert.Equal(ids.Count, ids.Distinct(StringComparer.Ordinal).Count());
    }

    private static async Task<JsonDocument> GetDocumentAsync()
    {
        return JsonDocument.Parse(await DocumentBody.Value);
    }

    // The document is the same for every test, so one host serves all of them. Every extra boot is one more
    // shutdown that can lose the race inside MassTransit's receive endpoint stop, which fails the test that owns it.
    private static async Task<string> FetchDocumentBodyAsync()
    {
        // An empty module list leaves TestModuleOverride unset, so the host reads the enabled modules from configuration.
        await using var factory = new HostTestFactory().WithModules(string.Empty);
        await factory.InitializeAsync();
        var client = factory.CreateClient();

        var response = await client.GetAsync(new Uri("/swagger/v1/swagger.json", UriKind.Relative));
        var body = await response.Content.ReadAsStringAsync();
        Assert.True(response.IsSuccessStatusCode, $"{(int)response.StatusCode}: {body[..Math.Min(body.Length, 300)]}");

        return body;
    }
}
