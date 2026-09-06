using Common.InterModuleRequests.Contracts;
using Common.InterModuleRequests.IAM;
using Common.Tests;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Notifications.Application.Push;
using NSubstitute;
using Xunit;

namespace Notifications.Tests;

public class NotificationsTestFactory : IntegrationTestFactory
{
    // Outbox is required by BaseDbContext's outbox insert; Notifications owns the device registry under test.
    protected override string[] GetActiveModules() => ["Notifications", "Outbox"];

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        base.ConfigureWebHost(builder);

        builder.ConfigureTestServices(services =>
        {
            // FCM is an external API; substitute it so push-dependent tests can assert on delivery
            // without depending on the DummyPushGateway's no-op behavior.
            services.AddSingleton(Substitute.For<IPushGateway>());

            // IAM is not among the active modules, so nothing consumes this request over the in-memory
            // transport. Substitute the client so DeviceRegistryReconciliationServiceTests controls the
            // "live sessions" answer directly.
            services.AddSingleton(
                Substitute.For<IInterModuleRequestClient<GetActiveSessionIdsRequest, GetActiveSessionIdsResponse>>());
        });
    }
}

[CollectionDefinition("IntegrationTestCollection")]
public class IntegrationTestCollection : ICollectionFixture<NotificationsTestFactory>;
