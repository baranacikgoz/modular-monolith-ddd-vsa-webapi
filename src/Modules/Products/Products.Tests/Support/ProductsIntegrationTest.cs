using System.Net.Http.Headers;
using Common.Application.Storage;
using Common.Domain.StronglyTypedIds;
using Common.Infrastructure.Persistence.Outbox;
using Common.IntegrationEvents;
using Common.Tests;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Products.Application.Persistence;
using Xunit;

namespace Products.Tests.Support;

/// <summary>Shared plumbing of the Products slice tests: the in-memory object stores (emptied before every test, they are
/// not part of the database Respawn resets), an authorized client and the outbox read.</summary>
[Collection("IntegrationTestCollection")]
public abstract class ProductsIntegrationTest(IntegrationTestWebAppFactory factory) : BaseIntegrationTest(factory)
{
    protected static ApplicationUserId DefaultUser { get; } = new(TestAuthHandler.DefaultUserId);

    protected InMemoryObjectStore PublicStore => (InMemoryObjectStore)Scope.ServiceProvider.GetRequiredService<IPublicObjectStore>();

    protected InMemoryObjectStore PrivateStore => (InMemoryObjectStore)Scope.ServiceProvider.GetRequiredService<IPrivateObjectStore>();

    protected IProductsDbContext Db => Scope.ServiceProvider.GetRequiredService<IProductsDbContext>();

    public override async ValueTask InitializeAsync()
    {
        await base.InitializeAsync();
        PublicStore.Clear();
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
