using Common.Application.Storage;
using Common.Domain.StronglyTypedIds;
using Common.InterModuleRequests.Contracts;
using Common.InterModuleRequests.IAM;
using Common.Tests;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using NSubstitute;
using NSubstitute.ClearExtensions;
using Products.Tests.Support;

namespace Products.Tests;

public class IntegrationTestWebAppFactory : IntegrationTestFactory
{
    // IAM is not activated: store seeding falls back gracefully when GetSeedUserIds has no handler
    // (Seeder.Stores.cs), and activating IAM here would require a Keycloak container per test run.
    protected override string[] GetActiveModules() => ["Products", "Outbox"];

    /// <summary>Stands in for IAM's account provisioning, which the store application calls. IAM's own tests cover the
    /// handler; a test sets what the substitute answers after <see cref="ResetIamClients" />.</summary>
    public IInterModuleRequestClient<ProvisionUserRequest, ProvisionUserResponse> ProvisionUserClient { get; } =
        Substitute.For<IInterModuleRequestClient<ProvisionUserRequest, ProvisionUserResponse>>();

    public IInterModuleRequestClient<DeprovisionUserRequest, DeprovisionUserResponse> DeprovisionUserClient { get; } =
        Substitute.For<IInterModuleRequestClient<DeprovisionUserRequest, DeprovisionUserResponse>>();

    /// <summary>Forgets earlier calls and answers every provisioning request with a new user id.</summary>
    public void ResetIamClients()
    {
        ProvisionUserClient.ClearSubstitute(ClearOptions.All);
        DeprovisionUserClient.ClearSubstitute(ClearOptions.All);

        ProvisionUserClient.SendAsync(Arg.Any<ProvisionUserRequest>(), Arg.Any<CancellationToken>())
            .Returns(_ => new ProvisionUserResponse(new ApplicationUserId(DefaultIdType.CreateVersion7()), ProvisionUserFailureReason.None));
        DeprovisionUserClient.SendAsync(Arg.Any<DeprovisionUserRequest>(), Arg.Any<CancellationToken>())
            .Returns(new DeprovisionUserResponse());
    }

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
            services.AddSingleton(ProvisionUserClient);
            services.AddSingleton(DeprovisionUserClient);
        });
    }
}
