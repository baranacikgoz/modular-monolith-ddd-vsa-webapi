using Common.Application.Storage;
using Common.Tests;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Products.Tests.Support;

namespace Products.Tests;

public class IntegrationTestWebAppFactory : IntegrationTestFactory
{
    // IAM is not activated: store seeding falls back gracefully when GetSeedUserIds has no handler
    // (Seeder.Stores.cs), and activating IAM here would require a Keycloak container per test run.
    protected override string[] GetActiveModules() => ["Products", "Outbox"];

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        base.ConfigureWebHost(builder);

        // The S3 stores are replaced by in-memory ones: no storage engine runs in the tests.
        builder.ConfigureTestServices(services =>
        {
            services.RemoveAll<IPublicObjectStore>();
            services.RemoveAll<IPrivateObjectStore>();
            services.AddSingleton<IPublicObjectStore>(new InMemoryObjectStore());
            services.AddSingleton<IPrivateObjectStore>(new InMemoryObjectStore());
        });
    }
}
