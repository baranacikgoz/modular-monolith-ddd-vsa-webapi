using Common.Domain.StronglyTypedIds;
using Common.Endpoints;
using Common.InterModuleRequests.Contracts;
using Common.InterModuleRequests.IAM;
using MassTransit;
using Microsoft.Extensions.Logging;
using NSubstitute;
using Xunit;

#pragma warning disable CA1515, CA1707

namespace Common.Tests;

public sealed class ActorNamesTests
{
    private readonly FakeGetUsersClient _client = new();
    private readonly CapturingLogger _logger = new();

    [Fact]
    public async Task ResolveAsync_NoIds_DoesNotAskIam()
    {
        var names = await ActorNames.ResolveAsync(_client, [], _logger, CancellationToken.None);

        Assert.Empty(names);
        Assert.Empty(_client.Requests);
    }

    [Fact]
    public async Task ResolveAsync_OnlyNullIds_DoesNotAskIam()
    {
        var names = await ActorNames.ResolveAsync(_client, [null, null], _logger, CancellationToken.None);

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

        await ActorNames.ResolveAsync(_client, [first, null, second, first], _logger, CancellationToken.None);

        var request = Assert.Single(_client.Requests);
        Assert.Equal([first, second], request.UserIds);
    }

    [Fact]
    public async Task ResolveAsync_KnownUser_MapsIdToFullName()
    {
        var id = NewId();
        _client.Users[id] = new UserSummary(id, "ada@example.com", "Ada", "Lovelace");

        var names = await ActorNames.ResolveAsync(_client, [id], _logger, CancellationToken.None);

        Assert.Equal("Ada Lovelace", names[id]);
    }

    [Fact]
    public async Task ResolveAsync_UserUnknownToIam_IsSkipped()
    {
        var known = NewId();
        var unknown = NewId();
        _client.Users[known] = new UserSummary(known, "ada@example.com", "Ada", "Lovelace");

        var names = await ActorNames.ResolveAsync(_client, [known, unknown], _logger, CancellationToken.None);

        Assert.Equal([known], names.Keys);
    }

    [Theory]
    [InlineData(null, null)]
    [InlineData("", " ")]
    public async Task ResolveAsync_UserWithoutAName_IsSkipped(string? firstName, string? lastName)
    {
        var id = NewId();
        _client.Users[id] = new UserSummary(id, "nameless@example.com", firstName, lastName);

        var names = await ActorNames.ResolveAsync(_client, [id], _logger, CancellationToken.None);

        Assert.Empty(names);
    }

    [Fact]
    public async Task ResolveAsync_UserWithOnlyOneNamePart_TrimsTheMissingPart()
    {
        var id = NewId();
        _client.Users[id] = new UserSummary(id, "ada@example.com", "Ada", null);

        var names = await ActorNames.ResolveAsync(_client, [id], _logger, CancellationToken.None);

        Assert.Equal("Ada", names[id]);
    }

    [Fact]
    public async Task ResolveAsync_IamTimesOut_ReturnsNoNamesAndLogsAWarning()
    {
        _client.Failure = new RequestTimeoutException("get-users");

        var names = await ActorNames.ResolveAsync(_client, [NewId()], _logger, CancellationToken.None);

        Assert.Empty(names);
        var entry = Assert.Single(_logger.Entries);
        Assert.Equal(LogLevel.Warning, entry.Level);
        Assert.Same(_client.Failure, entry.Exception);
    }

    [Fact]
    public async Task ResolveAsync_IamHandlerFaulted_ReturnsNoNamesAndLogsAWarning()
    {
        _client.Failure = new RequestFaultException(nameof(GetUsersRequest), Substitute.For<Fault>());

        var names = await ActorNames.ResolveAsync(_client, [NewId()], _logger, CancellationToken.None);

        Assert.Empty(names);
        var entry = Assert.Single(_logger.Entries);
        Assert.Equal(LogLevel.Warning, entry.Level);
        Assert.Same(_client.Failure, entry.Exception);
    }

    [Fact]
    public async Task ResolveAsync_IamUnreachable_ReturnsNoNamesAndLogsAWarning()
    {
        _client.Failure = new TransportUnavailableException("broker is down");

        var names = await ActorNames.ResolveAsync(_client, [NewId()], _logger, CancellationToken.None);

        Assert.Empty(names);
        Assert.Single(_logger.Entries);
    }

    [Fact]
    public async Task ResolveAsync_NoFailure_LogsNothing()
    {
        var id = NewId();
        _client.Users[id] = new UserSummary(id, "ada@example.com", "Ada", "Lovelace");

        await ActorNames.ResolveAsync(_client, [id], _logger, CancellationToken.None);

        Assert.Empty(_logger.Entries);
    }

    [Fact]
    public async Task ResolveAsync_RequestCancelled_Propagates()
    {
        _client.Failure = new OperationCanceledException();

        await Assert.ThrowsAsync<OperationCanceledException>(
            () => ActorNames.ResolveAsync(_client, [NewId()], _logger, CancellationToken.None));
        Assert.Empty(_logger.Entries);
    }

    [Fact]
    public async Task ResolveAsync_FailureWhileTheCallerCancelled_Propagates()
    {
        using var cancellation = new CancellationTokenSource();
        await cancellation.CancelAsync();
        _client.Failure = new RequestTimeoutException("get-users");

        await Assert.ThrowsAsync<RequestTimeoutException>(
            () => ActorNames.ResolveAsync(_client, [NewId()], _logger, cancellation.Token));
    }

    [Fact]
    public async Task ResolveAsync_UnrelatedFailure_Propagates()
    {
        _client.Failure = new InvalidOperationException("bug");

        await Assert.ThrowsAsync<InvalidOperationException>(
            () => ActorNames.ResolveAsync(_client, [NewId()], _logger, CancellationToken.None));
    }

    private static ApplicationUserId NewId() => new(Guid.NewGuid());

    private sealed class CapturingLogger : ILogger
    {
        public List<(LogLevel Level, Exception? Exception)> Entries { get; } = [];

        public IDisposable? BeginScope<TState>(TState state) where TState : notnull
        {
            return null;
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return true;
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception,
            Func<TState, Exception?, string> formatter)
        {
            Entries.Add((logLevel, exception));
        }
    }

    private sealed class FakeGetUsersClient : IInterModuleRequestClient<GetUsersRequest, GetUsersResponse>
    {
        public Dictionary<ApplicationUserId, UserSummary> Users { get; } = [];

        public List<GetUsersRequest> Requests { get; } = [];

        public Exception? Failure { get; set; }

        public Task<GetUsersResponse> SendAsync(GetUsersRequest request, CancellationToken cancellationToken)
        {
            Requests.Add(request);

            if (Failure is not null)
            {
                return Task.FromException<GetUsersResponse>(Failure);
            }

            ICollection<UserSummary> found = request.UserIds
                .Where(Users.ContainsKey)
                .Select(id => Users[id])
                .ToList();

            return Task.FromResult(new GetUsersResponse(found));
        }
    }
}
