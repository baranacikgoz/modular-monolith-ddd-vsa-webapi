using Common.InterModuleRequests.Contracts;
using Common.InterModuleRequests.IAM;
using Common.Application.Storage;
using Common.InterModuleRequests.Products;
using Common.Tests;
using Inventory.Application.Gateway;
using Inventory.Tests.Support;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using NSubstitute;

namespace Inventory.Tests;

public class IntegrationTestWebAppFactory : IntegrationTestFactory
{
    // Products is active alongside Inventory so the real cross-module paths run end to end: the
    // ProductCreatedIntegrationEvent outbox message (publish side) and both InterModuleRequest
    // directions (GetProductRequest, GetStockLevelRequest). The OutboxProcessor itself stays off
    // (OutboxOptions:IsProcessor=false, set globally in IntegrationTestFactory) - slice tests only
    // assert the outbox message was written, matching the convention used everywhere else in this
    // repo's test suite (see IntegrationTestFactory's own comment on that setting).
    protected override string[] GetActiveModules() => ["Inventory", "Products", "Outbox"];

    /// <summary>
    ///     Stands in for Products' free-text product search, which the stock level list calls for its product name
    ///     filter. A test sets what it returns; the search itself is covered where it lives.
    /// </summary>
    public IInterModuleRequestClient<SearchProductsByTermRequest, SearchProductsByTermResponse> ProductSearchClient { get; } =
        Substitute.For<IInterModuleRequestClient<SearchProductsByTermRequest, SearchProductsByTermResponse>>();

    /// <summary>
    ///     Stands in for IAM's bulk user lookup, which the stock movement list calls to name the acting users. IAM is not
    ///     active in these tests; a test sets what it returns. It answers with no users until then.
    /// </summary>
    public IInterModuleRequestClient<GetUsersRequest, GetUsersResponse> UsersClient { get; } =
        Substitute.For<IInterModuleRequestClient<GetUsersRequest, GetUsersResponse>>();

    /// <summary>
    ///     The warehouse system every connection talks to in these tests: installed as the primary handler of the gateway
    ///     factory's HTTP client, so the real gateways, pipelines and encryption run and only the network is replaced. A test
    ///     calls <see cref="FakeWarehouseHandler.Reset" /> first and sets what it answers.
    /// </summary>
    public FakeWarehouseHandler Warehouse { get; } = new();

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        base.ConfigureWebHost(builder);

        UsersClient
            .SendAsync(Arg.Any<GetUsersRequest>(), Arg.Any<CancellationToken>())
            .Returns(new GetUsersResponse([]));

        builder.ConfigureTestServices(services =>
        {
            services.AddSingleton(ProductSearchClient);
            services.AddSingleton(UsersClient);

            // A typed client is named after its service type.
            services.AddHttpClient(nameof(IWarehouseGatewayFactory)).ConfigurePrimaryHttpMessageHandler(() => Warehouse);

            // The S3 stores are replaced by in-memory ones: no storage engine runs in the tests.
            services.RemoveAll<IPublicObjectStore>();
            services.RemoveAll<IPrivateObjectStore>();
            services.AddSingleton<IPublicObjectStore>(new InMemoryObjectStore());
            services.AddSingleton<IPrivateObjectStore>(new InMemoryObjectStore());
        });
    }
}
