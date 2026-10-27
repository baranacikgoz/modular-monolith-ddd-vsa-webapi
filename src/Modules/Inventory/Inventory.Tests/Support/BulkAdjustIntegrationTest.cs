using System.Net.Http.Headers;
using Common.Application.Storage;
using Common.Domain.StronglyTypedIds;
using Common.Infrastructure.Persistence.Outbox;
using Common.IntegrationEvents;
using Common.Tests;
using Inventory.Application.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Inventory.Tests.Support;

/// <summary>Shared plumbing of the bulk adjustment tests: the in-memory object store (emptied before every test, it is not
/// part of the database Respawn resets), an authorized client and the outbox read.</summary>
[Collection("IntegrationTestCollection")]
public abstract class BulkAdjustIntegrationTest(IntegrationTestWebAppFactory factory) : BaseIntegrationTest(factory)
{
    protected static ApplicationUserId DefaultUser { get; } = new(TestAuthHandler.DefaultUserId);

    protected InMemoryObjectStore PrivateStore => (InMemoryObjectStore)Scope.ServiceProvider.GetRequiredService<IPrivateObjectStore>();

    protected IInventoryDbContext Db => Scope.ServiceProvider.GetRequiredService<IInventoryDbContext>();

    public override async ValueTask InitializeAsync()
    {
        await base.InitializeAsync();
        PrivateStore.Clear();
    }

    /// <summary>A client authenticated as the default test user, or as <paramref name="user" />.</summary>
    protected HttpClient AuthorizedClient(ApplicationUserId? user = null)
    {
        var client = Factory.CreateClient();
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("TestScheme");
        if (user is { } id)
        {
            client.DefaultRequestHeaders.Add("X-Test-User-Id", id.Value.ToString());
        }

        return client;
    }

    /// <summary>Every integration event waiting in the outbox (the processor is off in slice tests).</summary>
    protected async Task<List<IntegrationEvent>> OutboxEventsAsync()
    {
        using var scope = Factory.Services.CreateScope();
        var outbox = scope.ServiceProvider.GetRequiredService<IOutboxDbContext>();
        var messages = await outbox.OutboxMessages.AsNoTracking().Where(m => !m.IsProcessed).ToListAsync();
        return messages.Select(m => m.Event).ToList();
    }
}
