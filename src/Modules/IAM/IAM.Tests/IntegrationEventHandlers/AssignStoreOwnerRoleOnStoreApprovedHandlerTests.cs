using Common.Application.Auth;
using Common.Application.Options;
using Common.Application.Persistence.Inbox;
using Common.Domain.ResultMonad;
using Common.Domain.StronglyTypedIds;
using Common.IntegrationEvents;
using Common.InterModuleRequests.Contracts;
using Common.InterModuleRequests.IAM;
using Common.Tests;
using IAM.Application.Keycloak;
using IAM.Domain.Errors;
using IAM.Infrastructure.IntegrationEventHandlers;
using IAM.Tests.InterModuleRequestHandlers;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NSubstitute;
using Xunit;
using ZiggyCreatures.Caching.Fusion;

namespace IAM.Tests.IntegrationEventHandlers;

#pragma warning disable CA1707 // Remove the underscores from member name

/// <summary>
///     The StoreApproved consumer against the real Keycloak Testcontainer. The consumer is driven through
///     <see cref="IConsumer{T}.Consume" /> with a substituted <see cref="ConsumeContext{T}" /> so the base class's
///     per-event-id dedupe is part of what runs; the transport itself is not under test.
/// </summary>
[Collection("IntegrationTestCollection")]
public class AssignStoreOwnerRoleOnStoreApprovedHandlerTests(IntegrationTestWebAppFactory factory) : BaseIntegrationTest(factory)
{
    [Fact]
    public async Task Consume_ApprovedStore_AssignsTheStoreOwnerRealmRole()
    {
        var ownerId = await ProvisionUserAsync();

        await ConsumeAsync(new StoreApprovedIntegrationEvent(Guid.NewGuid(), ownerId));

        Assert.Contains(ownerId, await StoreOwnerIdsAsync());
    }

    /// <summary>A second event for the same owner (another store, or an outbox retry): Keycloak accepts an already-held role.</summary>
    [Fact]
    public async Task Consume_TwiceForTheSameOwner_IsHarmless()
    {
        var ownerId = await ProvisionUserAsync();

        await ConsumeAsync(new StoreApprovedIntegrationEvent(Guid.NewGuid(), ownerId));
        await ConsumeAsync(new StoreApprovedIntegrationEvent(Guid.NewGuid(), ownerId));

        Assert.Single(await StoreOwnerIdsAsync(), id => id == ownerId);
    }

    /// <summary>The exact same message delivered twice is dropped by the base class before Keycloak is asked again.</summary>
    [Fact]
    public async Task Consume_SameEventIdTwice_IsDeduplicatedByTheBaseClass()
    {
        var adminClient = Substitute.For<IKeycloakAdminClient>();
        adminClient.AssignRealmRoleAsync(Arg.Any<ApplicationUserId>(), Arg.Any<string>(), Arg.Any<CancellationToken>())
            .Returns(Task.FromResult<Result>(Result.Success));
        var handler = CreateHandler(adminClient);
        var @event = new StoreApprovedIntegrationEvent(Guid.NewGuid(), new ApplicationUserId(Guid.NewGuid()));

        await handler.Consume(ContextFor(@event));
        await handler.Consume(ContextFor(@event));

        await adminClient.Received(1)
            .AssignRealmRoleAsync(@event.OwnerId, KeycloakRoles.StoreOwner, Arg.Any<CancellationToken>());
    }

    /// <summary>An owner Keycloak no longer has (deleted between approval and consumption) is skipped, never retried forever.</summary>
    [Fact]
    public async Task Consume_OwnerUnknownToKeycloak_SkipsWithoutThrowing()
    {
        var unknownOwnerId = new ApplicationUserId(Guid.NewGuid());

        await ConsumeAsync(new StoreApprovedIntegrationEvent(Guid.NewGuid(), unknownOwnerId));

        Assert.DoesNotContain(unknownOwnerId, await StoreOwnerIdsAsync());
    }

    /// <summary>An identity-provider failure must surface as an exception so the bus retries instead of acking the message.</summary>
    [Fact]
    public async Task Consume_IdentityProviderUnavailable_ThrowsSoTheBusRetries()
    {
        var adminClient = Substitute.For<IKeycloakAdminClient>();
        adminClient.AssignRealmRoleAsync(Arg.Any<ApplicationUserId>(), Arg.Any<string>(), Arg.Any<CancellationToken>())
            .Returns(Task.FromResult<Result>(IdentityErrors.IdentityProviderUnavailable));
        var handler = CreateHandler(adminClient);
        var @event = new StoreApprovedIntegrationEvent(Guid.NewGuid(), new ApplicationUserId(Guid.NewGuid()));

        await Assert.ThrowsAsync<InvalidOperationException>(() => handler.Consume(ContextFor(@event)));
    }

    private AssignStoreOwnerRoleOnStoreApprovedHandler CreateHandler(IKeycloakAdminClient adminClient)
    {
        return new AssignStoreOwnerRoleOnStoreApprovedHandler(
            adminClient,
            Scope.ServiceProvider.GetRequiredService<IFusionCache>(),
            Scope.ServiceProvider.GetRequiredService<IOptions<CachingOptions>>(),
            StatelessInboxStore.Instance,
            Scope.ServiceProvider.GetRequiredService<ILogger<AssignStoreOwnerRoleOnStoreApprovedHandler>>());
    }

    private async Task<ApplicationUserId> ProvisionUserAsync()
    {
        var provisionClient = Scope.ServiceProvider
            .GetRequiredService<IInterModuleRequestClient<ProvisionUserRequest, ProvisionUserResponse>>();
        var provisioned = await provisionClient.SendAsync(
            ProvisionUserTestRequests.Create(
                Scope.ServiceProvider, $"{Guid.NewGuid():N}@example.com", IamTestClient.NewPhoneNumber(), "A-Valid-Password-1"),
            CancellationToken.None);

        return provisioned.UserId!.Value;
    }

    private async Task ConsumeAsync(StoreApprovedIntegrationEvent @event)
    {
        var handler = Scope.ServiceProvider.GetRequiredService<AssignStoreOwnerRoleOnStoreApprovedHandler>();
        await handler.Consume(ContextFor(@event));
    }

    private static ConsumeContext<StoreApprovedIntegrationEvent> ContextFor(StoreApprovedIntegrationEvent @event)
    {
        var context = Substitute.For<ConsumeContext<StoreApprovedIntegrationEvent>>();
        context.Message.Returns(@event);
        context.CancellationToken.Returns(CancellationToken.None);
        return context;
    }

    private async Task<IReadOnlyList<ApplicationUserId>> StoreOwnerIdsAsync()
    {
        var adminClient = Scope.ServiceProvider.GetRequiredService<IKeycloakAdminClient>();
        return await adminClient.GetUserIdsInRoleAsync(KeycloakRoles.StoreOwner, 1000, CancellationToken.None);
    }
}

#pragma warning restore CA1707
