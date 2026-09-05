using System.Net;
using System.Net.Http.Json;
using Common.Tests;
using Xunit;

namespace IAM.Tests.Endpoints.Users;

[Collection("IntegrationTestCollection")]
public class SearchTests(IntegrationTestWebAppFactory factory) : BaseIntegrationTest(factory)
{
    private sealed record UserResponse(Guid Id, string Username, string? FirstName, string? LastName);

    private sealed record Page(List<UserResponse> Data, int TotalCount, int PageNumber, int PageSize);

    [Fact]
    public async Task Search_AsStaff_MatchesLastNameSubstring()
    {
        var staff = await IamTestClient.LoginByEmailAsync(Factory, SeedUsers.StaffEmail, SeedUsers.StaffPassword);

        var page = await IamTestClient.Authorized(Factory, staff)
            .GetFromJsonAsync<Page>(new Uri("/users/search?searchTerm=doe&pageNumber=1&pageSize=10", UriKind.Relative));

        Assert.NotNull(page);
        Assert.Contains(page.Data, u => u.Username == SeedUsers.BasicPhone && u.LastName == "Doe");
        Assert.True(page.TotalCount >= 1);
    }

    [Fact]
    public async Task Search_Paginates()
    {
        var staff = await IamTestClient.LoginByEmailAsync(Factory, SeedUsers.StaffEmail, SeedUsers.StaffPassword);

        var page = await IamTestClient.Authorized(Factory, staff)
            .GetFromJsonAsync<Page>(new Uri("/users/search?searchTerm=9011111111&pageNumber=1&pageSize=2", UriKind.Relative));

        Assert.NotNull(page);
        Assert.Equal(2, page.Data.Count);
        Assert.True(page.TotalCount >= 6);
    }

    [Fact]
    public async Task Search_AsBasicUser_Returns403()
    {
        var basic = await IamTestClient.LoginByPhoneAsync(Factory, SeedUsers.BasicPhone);

        using var response = await IamTestClient.Authorized(Factory, basic)
            .GetAsync(new Uri("/users/search?pageNumber=1&pageSize=10", UriKind.Relative));

        Assert.Equal(HttpStatusCode.Forbidden, response.StatusCode);
    }
}
