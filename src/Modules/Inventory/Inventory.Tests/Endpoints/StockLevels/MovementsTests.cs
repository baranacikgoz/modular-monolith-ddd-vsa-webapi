using System.Net;
using System.Net.Http.Json;
using Common.Application.Pagination;
using Common.Domain.StronglyTypedIds;
using Common.InterModuleRequests.IAM;
using Common.Tests;
using Inventory.Application.Persistence;
using Inventory.Domain.StockLevels;
using Inventory.Tests.Support;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using Xunit;
using Response = Inventory.Endpoints.StockLevels.v1.Movements.Response;

namespace Inventory.Tests.Endpoints.StockLevels;

[Collection("IntegrationTestCollection")]
public class MovementsTests(IntegrationTestWebAppFactory factory) : BaseIntegrationTest(factory)
{
    private static readonly ApplicationUserId Actor = new(TestAuthHandler.DefaultUserId);

    public override async ValueTask InitializeAsync()
    {
        await base.InitializeAsync();
        factory.UsersClient.ClearReceivedCalls();
        UsersLookupReturns();
    }

    private void UsersLookupReturns(params UserSummary[] users)
    {
        factory.UsersClient
            .SendAsync(Arg.Any<GetUsersRequest>(), Arg.Any<CancellationToken>())
            .Returns(new GetUsersResponse(users));
    }

    private static Uri ReceiveRouteOf(StockLevelId id) => new($"/v1/stock-levels/{id}/receive", UriKind.Relative);

    private static Uri MovementsRouteOf(StockLevelId id, string query) =>
        new($"/v1/stock-levels/{id}/movements?{query}", UriKind.Relative);

    private async Task<PaginationResponse<Response>> GetPageAsync(HttpClient client, StockLevelId id, string query)
    {
        var response = await client.GetAsync(MovementsRouteOf(id, query));
        Assert.True(response.IsSuccessStatusCode, await response.Content.ReadAsStringAsync());
        var page = await response.Content.ReadFromJsonAsync<PaginationResponse<Response>>(JsonSerializerOptions);
        return page!;
    }

    // Three receives made through the endpoint, so every ledger row carries the acting user.
    private async Task<StockLevel> SeedLedgerAsync(HttpClient client)
    {
        var stockLevel = await StockLevelTestData.SeedStockLevelAsync(Factory.Services, quantityOnHand: 0);
        foreach (var quantity in new[] { 1, 2, 3 })
        {
            var response = await client.PostAsJsonAsync(ReceiveRouteOf(stockLevel.Id), new { quantity, reason = $"Delivery {quantity}" });
            Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
        }

        return stockLevel;
    }

    [Fact]
    public async Task GetMovements_ReturnsTheLedgerNewestFirstWithTheActingUser()
    {
        var client = StockLevelTestData.AuthedClient(Factory);
        var stockLevel = await SeedLedgerAsync(client);

        var page = await GetPageAsync(client, stockLevel.Id, "pageNumber=1&pageSize=10");

        Assert.Equal([3, 2, 1], page.Data.Select(m => m.Quantity).ToArray());
        Assert.Equal([6, 3, 1], page.Data.Select(m => m.QuantityOnHandAfter).ToArray());
        Assert.All(page.Data, m => Assert.Equal(StockMovementType.Received, m.Type));
        Assert.All(page.Data, m => Assert.Equal(TestAuthHandler.DefaultUserId, m.ActingUserId?.Value));
        Assert.Equal("Delivery 3", page.Data.First().Reason);
        Assert.Null(page.NextCursor);
    }

    [Fact]
    public async Task GetMovements_FollowingTheCursor_WalksEveryRowOnce()
    {
        var client = StockLevelTestData.AuthedClient(Factory);
        var stockLevel = await SeedLedgerAsync(client);

        var first = await GetPageAsync(client, stockLevel.Id, "pageNumber=1&pageSize=2");
        var second = await GetPageAsync(client, stockLevel.Id, $"pageNumber=1&pageSize=2&after={first.NextCursor}");

        Assert.Equal([3, 2], first.Data.Select(m => m.Quantity).ToArray());
        Assert.NotNull(first.NextCursor);
        Assert.Equal([1], second.Data.Select(m => m.Quantity).ToArray());
        Assert.Null(second.NextCursor);
    }

    // The rows of one save share a timestamp and their ids are random inside a millisecond, so only the sequence puts them in
    // order. Several stock levels make a wrong order or a repeated or skipped row at a page boundary show up on every run.
    [Fact]
    public async Task GetMovements_RowsOfOneSave_AreListedNewestFirstAndCursorPagesNeitherRepeatNorSkip()
    {
        var client = StockLevelTestData.AuthedClient(Factory);
        for (var attempt = 0; attempt < 10; attempt++)
        {
            var stockLevel = await StockLevelTestData.SeedStockLevelAsync(Factory.Services, quantityOnHand: 0);
            await using (var scope = Factory.Services.CreateAsyncScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<IInventoryDbContext>();
                var tracked = await db.StockLevels.SingleAsync(s => s.Id == stockLevel.Id);
                foreach (var quantity in new[] { 1, 2, 3, 4, 5 })
                {
                    Assert.False(tracked.Receive(quantity, $"Delivery {quantity}").IsFailure);
                }

                await db.SaveChangesAsync();
            }

            var first = await GetPageAsync(client, stockLevel.Id, "pageNumber=1&pageSize=2");
            var second = await GetPageAsync(client, stockLevel.Id, $"pageNumber=1&pageSize=2&after={first.NextCursor}");
            var third = await GetPageAsync(client, stockLevel.Id, $"pageNumber=1&pageSize=2&after={second.NextCursor}");

            Assert.Equal([5L, 4L], first.Data.Select(m => m.Sequence).ToArray());
            Assert.Equal([3L, 2L], second.Data.Select(m => m.Sequence).ToArray());
            Assert.Equal([1L], third.Data.Select(m => m.Sequence).ToArray());
            Assert.Equal([5, 4, 3, 2, 1], first.Data.Concat(second.Data).Concat(third.Data).Select(m => m.Quantity).ToArray());
        }
    }

    [Fact]
    public async Task GetMovements_UnknownStockLevel_ReturnsNotFound()
    {
        var client = StockLevelTestData.AuthedClient(Factory);

        var response = await client.GetAsync(MovementsRouteOf(StockLevelId.New(), "pageNumber=1&pageSize=10"));

        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]
    public async Task GetMovements_MalformedCursor_ReturnsBadRequest()
    {
        var stockLevel = await StockLevelTestData.SeedStockLevelAsync(Factory.Services);
        var client = StockLevelTestData.AuthedClient(Factory);

        var response = await client.GetAsync(MovementsRouteOf(stockLevel.Id, "pageNumber=1&pageSize=10&after=not-a-cursor"));

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task GetMovements_KnownActingUser_CarriesTheDisplayNameFromOneLookupForThePage()
    {
        UsersLookupReturns(new UserSummary(Actor, "ada@example.com", "Ada", "Lovelace", null));
        var client = StockLevelTestData.AuthedClient(Factory);
        var stockLevel = await SeedLedgerAsync(client);

        var page = await GetPageAsync(client, stockLevel.Id, "pageNumber=1&pageSize=10");

        Assert.Equal(3, page.Data.Count);
        Assert.All(page.Data, m => Assert.Equal("Ada Lovelace", m.ActingUserName));
        Assert.All(page.Data, m => Assert.Equal(TestAuthHandler.DefaultUserId, m.ActingUserId?.Value));
        await factory.UsersClient.Received(1).SendAsync(
            Arg.Is<GetUsersRequest>(r => r.UserIds.Count == 1 && r.UserIds.Single() == Actor),
            Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task GetMovements_EveryPageAsksIamOnce()
    {
        UsersLookupReturns(new UserSummary(Actor, "ada@example.com", "Ada", "Lovelace", null));
        var client = StockLevelTestData.AuthedClient(Factory);
        var stockLevel = await SeedLedgerAsync(client);

        var first = await GetPageAsync(client, stockLevel.Id, "pageNumber=1&pageSize=2");
        var second = await GetPageAsync(client, stockLevel.Id, $"pageNumber=1&pageSize=2&after={first.NextCursor}");

        Assert.Equal(2, first.Data.Count);
        Assert.Single(second.Data);
        Assert.All(first.Data.Concat(second.Data), m => Assert.Equal("Ada Lovelace", m.ActingUserName));
        await factory.UsersClient.Received(2).SendAsync(Arg.Any<GetUsersRequest>(), Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task GetMovements_ActingUserUnknownToIam_HasNoNameButKeepsTheId()
    {
        var client = StockLevelTestData.AuthedClient(Factory);
        var stockLevel = await SeedLedgerAsync(client);

        var page = await GetPageAsync(client, stockLevel.Id, "pageNumber=1&pageSize=10");

        Assert.All(page.Data, m => Assert.Null(m.ActingUserName));
        Assert.All(page.Data, m => Assert.Equal(TestAuthHandler.DefaultUserId, m.ActingUserId?.Value));
    }

    [Fact]
    public async Task GetMovements_IamTimesOut_StillReturnsTheLedgerWithIdsOnly()
    {
        var client = StockLevelTestData.AuthedClient(Factory);
        var stockLevel = await SeedLedgerAsync(client);
        factory.UsersClient
            .SendAsync(Arg.Any<GetUsersRequest>(), Arg.Any<CancellationToken>())
            .ThrowsAsync(new RequestTimeoutException("get-users"));

        var page = await GetPageAsync(client, stockLevel.Id, "pageNumber=1&pageSize=10");

        Assert.Equal(3, page.Data.Count);
        Assert.All(page.Data, m => Assert.Null(m.ActingUserName));
        Assert.All(page.Data, m => Assert.Equal(TestAuthHandler.DefaultUserId, m.ActingUserId?.Value));
    }

    [Fact]
    public async Task GetMovements_PageWithoutAnyActingUser_DoesNotAskIam()
    {
        var stockLevel = await StockLevelTestData.SeedStockLevelAsync(Factory.Services, quantityOnHand: 0);
        await using (var scope = Factory.Services.CreateAsyncScope())
        {
            var db = scope.ServiceProvider.GetRequiredService<IInventoryDbContext>();
            var tracked = await db.StockLevels.SingleAsync(s => s.Id == stockLevel.Id);
            Assert.False(tracked.Receive(4, "Made by a consumer").IsFailure);
            await db.SaveChangesAsync();
        }

        var client = StockLevelTestData.AuthedClient(Factory);

        var page = await GetPageAsync(client, stockLevel.Id, "pageNumber=1&pageSize=10");

        var movement = Assert.Single(page.Data);
        Assert.Null(movement.ActingUserId);
        Assert.Null(movement.ActingUserName);
        await factory.UsersClient.DidNotReceive().SendAsync(Arg.Any<GetUsersRequest>(), Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task GetMovements_EmptyLedger_DoesNotAskIam()
    {
        var stockLevel = await StockLevelTestData.SeedStockLevelAsync(Factory.Services, quantityOnHand: 3);
        var client = StockLevelTestData.AuthedClient(Factory);

        var page = await GetPageAsync(client, stockLevel.Id, "pageNumber=1&pageSize=10");

        Assert.Empty(page.Data);
        await factory.UsersClient.DidNotReceive().SendAsync(Arg.Any<GetUsersRequest>(), Arg.Any<CancellationToken>());
    }
}
