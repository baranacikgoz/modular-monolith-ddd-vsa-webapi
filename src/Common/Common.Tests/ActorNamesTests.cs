using Common.Domain.StronglyTypedIds;
using Common.Endpoints;
using Common.InterModuleRequests.Contracts;
using Common.InterModuleRequests.IAM;
using Xunit;

#pragma warning disable CA1515, CA1707

namespace Common.Tests;

public sealed class ActorNamesTests
{
    private readonly FakeGetUsersClient _client = new();

    [Fact]
    public async Task ResolveAsync_NoIds_DoesNotAskIam()
    {
        var names = await ActorNames.ResolveAsync(_client, [], CancellationToken.None);

        Assert.Empty(names);
        Assert.Empty(_client.Requests);
    }

    [Fact]
    public async Task ResolveAsync_OnlyNullIds_DoesNotAskIam()
    {
        var names = await ActorNames.ResolveAsync(_client, [null, null], CancellationToken.None);

        Assert.Empty(names);
        Assert.Empty(_client.Requests);
    }

    [Fact]
    public async Task ResolveAsync_RepeatedAndNullIds_SendsOneRequestWithDistinctIds()
    {
        var first = NewId();
        var second = NewId();
        _client.Users[first] = new UserSummary(first, "ada@example.com", "Ada", "Lovelace");
        _client.Users[second] = new UserSummary(second, "grace@example.com", "Grace", "Hopper");

        await ActorNames.ResolveAsync(_client, [first, null, second, first], CancellationToken.None);

        var request = Assert.Single(_client.Requests);
        Assert.Equal([first, second], request.UserIds);
    }

    [Fact]
    public async Task ResolveAsync_KnownUser_MapsIdToFullName()
    {
        var id = NewId();
        _client.Users[id] = new UserSummary(id, "ada@example.com", "Ada", "Lovelace");

        var names = await ActorNames.ResolveAsync(_client, [id], CancellationToken.None);

        Assert.Equal("Ada Lovelace", names[id]);
    }

    [Fact]
    public async Task ResolveAsync_UserUnknownToIam_IsSkipped()
    {
        var known = NewId();
        var unknown = NewId();
        _client.Users[known] = new UserSummary(known, "ada@example.com", "Ada", "Lovelace");

        var names = await ActorNames.ResolveAsync(_client, [known, unknown], CancellationToken.None);

        Assert.Equal([known], names.Keys);
    }

    [Theory]
    [InlineData(null, null)]
    [InlineData("", " ")]
    public async Task ResolveAsync_UserWithoutAName_IsSkipped(string? firstName, string? lastName)
    {
        var id = NewId();
        _client.Users[id] = new UserSummary(id, "nameless@example.com", firstName, lastName);

        var names = await ActorNames.ResolveAsync(_client, [id], CancellationToken.None);

        Assert.Empty(names);
    }

    [Fact]
    public async Task ResolveAsync_UserWithOnlyOneNamePart_TrimsTheMissingPart()
    {
        var id = NewId();
        _client.Users[id] = new UserSummary(id, "ada@example.com", "Ada", null);

        var names = await ActorNames.ResolveAsync(_client, [id], CancellationToken.None);

        Assert.Equal("Ada", names[id]);
    }

    private static ApplicationUserId NewId() => new(Guid.NewGuid());

    private sealed class FakeGetUsersClient : IInterModuleRequestClient<GetUsersRequest, GetUsersResponse>
    {
        public Dictionary<ApplicationUserId, UserSummary> Users { get; } = [];

        public List<GetUsersRequest> Requests { get; } = [];

        public Task<GetUsersResponse> SendAsync(GetUsersRequest request, CancellationToken cancellationToken)
        {
            Requests.Add(request);

            ICollection<UserSummary> found = request.UserIds
                .Where(Users.ContainsKey)
                .Select(id => Users[id])
                .ToList();

            return Task.FromResult(new GetUsersResponse(found));
        }
    }
}
