using Common.InterModuleRequests.Contracts;
using Common.InterModuleRequests.Products;
using Common.Tests;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
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

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        base.ConfigureWebHost(builder);

        builder.ConfigureTestServices(services => services.AddSingleton(ProductSearchClient));
    }
}
