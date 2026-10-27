using Common.Application.Options;
using Common.Domain.StronglyTypedIds;
using Common.InterModuleRequests.Contracts;
using Common.InterModuleRequests.Products;
using Common.Tests;
using Inventory.Application.Persistence;
using Inventory.Application.StoreAccesses;
using Inventory.Infrastructure.StoreAccesses;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Options;
using NSubstitute;
using Xunit;

namespace Inventory.Tests.StoreAccesses;

/// <summary>
///     Nightly heal of the store access copy from Products, run directly with a substituted Products pager: the job body is
///     what matters, Hangfire only fires it.
/// </summary>
[Collection("IntegrationTestCollection")]
public class StoreAccessReconciliationJobTests(IntegrationTestWebAppFactory factory) : BaseIntegrationTest(factory)
{
    private static readonly ApplicationUserId Owner = new(DefaultIdType.CreateVersion7());

    private IInventoryDbContext Db => Scope.ServiceProvider.GetRequiredService<IInventoryDbContext>();

    private StoreAccessReconciliationJob Job(IInterModuleRequestClient<GetStoresPageRequest, GetStoresPageResponse> pager)
    {
        return new StoreAccessReconciliationJob(
            Db,
            pager,
            Options.Create(new ProjectionReconciliationOptions { PageSize = 2, MaxPages = 10 }),
            NullLogger<StoreAccessReconciliationJob>.Instance);
    }

    private static StorePageItem Source(bool isActive, long version)
    {
        return new StorePageItem(DefaultIdType.CreateVersion7(), Owner, isActive, version);
    }

    /// <summary>A pager that answers the given pages one after the other, whatever cursor it is sent.</summary>
    private static IInterModuleRequestClient<GetStoresPageRequest, GetStoresPageResponse> PagerReturning(
        params GetStoresPageResponse[] pages)
    {
        var pager = Substitute.For<IInterModuleRequestClient<GetStoresPageRequest, GetStoresPageResponse>>();
        pager.SendAsync(Arg.Any<GetStoresPageRequest>(), Arg.Any<CancellationToken>()).Returns(pages[0], pages[1..]);
        return pager;
    }

    private async Task SeedRowAsync(StorePageItem source, bool isActive, long version)
    {
        var row = StoreAccess.Create(source.StoreId);
        row.ApplyIfNewer(source.OwnerId, isActive, version);
        Db.StoreAccess.Add(row);
        await Db.SaveChangesAsync();
        Db.ChangeTracker.Clear();
    }

    private async Task<StoreAccess> RowAsync(DefaultIdType storeId)
    {
        return await Db.StoreAccess.AsNoTracking().SingleAsync(a => a.StoreId == storeId);
    }

    [Fact]
    public async Task RunAsync_StoreWithoutRow_CreatesIt()
    {
        var source = Source(isActive: true, version: 4);
        var pager = PagerReturning(new GetStoresPageResponse([source], null));

        await Job(pager).RunAsync();

        var row = await RowAsync(source.StoreId);
        Assert.Equal(Owner, row.OwnerId);
        Assert.True(row.IsActive);
        Assert.Equal(4, row.SourceVersion);
    }

    /// <summary>The event that closed the store was lost: the nightly run closes it.</summary>
    [Fact]
    public async Task RunAsync_SourceNewerThanRow_CorrectsIsActive()
    {
        var source = Source(isActive: false, version: 5);
        await SeedRowAsync(source, isActive: true, version: 3);
        var pager = PagerReturning(new GetStoresPageResponse([source], null));

        await Job(pager).RunAsync();

        var row = await RowAsync(source.StoreId);
        Assert.False(row.IsActive);
        Assert.Equal(5, row.SourceVersion);
    }

    /// <summary>
    ///     The run reads Products over several requests; an event that landed in between is newer than the page it read, and
    ///     must survive the run.
    /// </summary>
    [Fact]
    public async Task RunAsync_SourceOlderThanRow_LeavesTheRowUntouched()
    {
        var source = Source(isActive: true, version: 2);
        await SeedRowAsync(source, isActive: false, version: 6);
        var pager = PagerReturning(new GetStoresPageResponse([source], null));

        await Job(pager).RunAsync();

        var row = await RowAsync(source.StoreId);
        Assert.False(row.IsActive);
        Assert.Equal(6, row.SourceVersion);
    }

    [Fact]
    public async Task RunAsync_TwoPages_SendsTheFirstPagesLastIdAsTheNextCursor()
    {
        var first = Source(isActive: true, version: 1);
        var second = Source(isActive: true, version: 1);
        var third = Source(isActive: false, version: 1);
        var pager = PagerReturning(
            new GetStoresPageResponse([first, second], second.StoreId),
            new GetStoresPageResponse([third], null));

        await Job(pager).RunAsync();

        await pager.Received(1).SendAsync(
            Arg.Is<GetStoresPageRequest>(r => r.AfterStoreId == second.StoreId), Arg.Any<CancellationToken>());
        Assert.False((await RowAsync(third.StoreId)).IsActive);
        Assert.True((await RowAsync(first.StoreId)).IsActive);
    }
}
